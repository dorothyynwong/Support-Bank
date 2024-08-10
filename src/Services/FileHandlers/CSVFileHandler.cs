namespace SupportBank;

public class CSVFileHandler : IFileHandler {
    private List<LineOfData> _dataList = new List<LineOfData>{};
    public IValidator Validator { get; set; }
    public IDataProcessor DataProcessor { get; set; }

    public CSVFileHandler()
    {
        Validator = new Validator();
        DataProcessor  = new DataProcessor();
    }

    public CSVFileHandler(IValidator validator, IDataProcessor dataProcessor) 
    {
        Validator = validator;
        DataProcessor = dataProcessor;
    }

    public void ImportFile(string FilePath)
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

    public void ImportAsStream(string FilePath)
    {
        try
        {
            using (StreamReader sr = new StreamReader(FilePath))
            {
                string line;
                
                line = sr.ReadLine(); //skip the header line
                while ((line = sr.ReadLine()) != null)
                {
                    // lines.Add(line);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }

    }

    public List<LineOfData> GetData()
    {
        return _dataList;
    }
}


