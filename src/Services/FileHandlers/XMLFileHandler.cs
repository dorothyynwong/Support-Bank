using System.Linq;
using System.Xml.Linq;
using System.Globalization;

namespace SupportBank;

public class XMLFileHandler : IFileHandler {
    private List<LineOfData> _dataList = new List<LineOfData>{};
    private CultureInfo _enGB = new CultureInfo("en-GB");

    public IValidator Validator { get; set; }
    public IDataProcessor DataProcessor { get; set; }

    
    public XMLFileHandler()
    {
        Validator = new XMLValidator();
        DataProcessor  = new DataProcessor();
    }

    public void ImportFromStream(string FilePath)
    {
        using FileStream fs = File.OpenRead(FilePath);
        XDocument xml = XDocument.Load(fs);
        
        foreach(var t in xml.Descendants("SupportTransaction")) 
        {
            DateTime date;
            DateTime.TryParseExact((string)t.Attribute("Date"), "dd/MM/yyyy HH:mm:ss" , new CultureInfo("en-GB") , DateTimeStyles.None, out date);
            ParsedData parsedData = new ParsedData 
            {
                            Date = date,
                            Amount = double.Parse(t.Element("Value").Value),
                            Narrative = t.Element("Description").Value,
                            FromAccount = (string)t.Element("Parties").Element("From"),
                            ToAccount = (string)t.Element("Parties").Element("To")
            };
            
            if (parsedData != null)
            {
                if (Validator.isValidTransaction(parsedData))
                {
                    DataProcessor.CreateTransaction(parsedData);
                }
                else
                {
                    throw new Exception("Invalid Transaction");
                }
            }
        }
     
    }
    

    public void ImportFromFile(string FilePath)
    {

        var xml = XDocument.Load(FilePath);

        var transactions = from t in xml.Descendants("SupportTransaction")
                    select new {
                            Date = (double)t.Attribute("Date"),
                            Amount = (string)t.Element("Value").Value,
                            Narrative = (string)t.Element("Description").Value,
                            FromAccount = (string)t.Element("Parties").Element("From"),
                            ToAccount = (string)t.Element("Parties").Element("To")
                        };

        foreach (var transaction in transactions)
        {
            DateTime date = DateTime.FromOADate(transaction.Date);
    
            LineOfData line = new LineOfData
            {
                Date = date.ToString(_enGB),
                FromAccount = transaction.FromAccount,
                ToAccount = transaction.ToAccount,
                Narrative = transaction.Narrative,
                Amount = transaction.Amount
            };
           _dataList.Add(line);
        }
    }

    public List<LineOfData> GetData()
    {
        return _dataList;
    }

    public (List<Transaction>, List<Person>) ProcessData(string filePath)
    {
        ImportFromStream(filePath);
        return (DataProcessor.transactions, DataProcessor.people);
    }
}