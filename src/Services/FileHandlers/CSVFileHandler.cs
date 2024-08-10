using System.Globalization;

namespace SupportBank;

public class CSVFileHandler : IFileHandler {
    private List<LineOfData> _dataList = new List<LineOfData>{};
    public IValidator Validator { get; set; }
    public IDataProcessor DataProcessor { get; set; }

    public CSVFileHandler()
    {
        Validator = new CSVValidator();
        DataProcessor  = new DataProcessor();
    }

    public void ImportFromFile(string FilePath)
    {
        string[] lines = File.ReadAllLines(FilePath);

        foreach(string line in lines)
        {
            string[] data = line.Split(",");
            LineOfData dataLine = new LineOfData{
                Date = data[0],
                FromAccount = data[1],
                ToAccount = data[2],
                Narrative = data[3],
                Amount = data[4]
            };

            _dataList.Add(dataLine);
        }
    }

    public void ImportFromStream(string FilePath)
    {
        const string HEADER = "Date,From,To,Narrative,Amount";
        try
        {
            using (StreamReader sr = new StreamReader(FilePath))
            {
                string line = sr.ReadLine();

                if (line == HEADER)
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] data = line.Split(",");
                        double amount;
                        DateTime date;
                        if (double.TryParse(data[4], out amount) && 
                            DateTime.TryParseExact(data[0], "dd/MM/yyyy" , new CultureInfo("en-GB") , DateTimeStyles.None, out date))
                        {
                            ParsedData parsedData = new ParsedData
                            {
                                Date = date,
                                FromAccount = data[1],
                                ToAccount = data[2],
                                Narrative = data[3],
                                Amount = amount
                            };
                            if (Validator.isValidTransaction(parsedData))
                            {
                                DataProcessor.CreateTransaction(parsedData);
                            }
                            else
                            {
                                throw new Exception("Invalid Line");
                            }
                        }
                        else
                        {
                            throw new Exception("Invalid Amount");
                        }
                    }
                }
                else
                {
                    throw new Exception("Invalid file format.");
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }

    }

    public (List<Transaction>, List<Person>) ProcessData(string filePath)
    {
        ImportFromStream(filePath);
        return (DataProcessor.transactions, DataProcessor.people);
    }

    public List<LineOfData> GetData()
    {
        return _dataList;
    }
}


