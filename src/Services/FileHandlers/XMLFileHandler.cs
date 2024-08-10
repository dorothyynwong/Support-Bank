using System.Linq;
using System.Xml.Linq;
using System.Globalization;

namespace SupportBank;

public class XMLFileHandler : IFileHandler {
    private List<LineOfData> _dataList = new List<LineOfData>{};
    private CultureInfo _enGB = new CultureInfo("en-GB");

    public IValidator Validator { get; set; }
    public IDataProcessor DataProcessor { get; set; }

    public void ImportFile(string FilePath)
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
}