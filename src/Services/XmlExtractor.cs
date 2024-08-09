using System.Linq;
using System.Xml.Linq;
using System.Globalization;

namespace SupportBank;

public class XmlExtractor : IExtractor
{
    private CultureInfo _enGB = new CultureInfo("en-GB");
    // public string FileName { get; set; }

    public List<LineOfData> ExtractData(string fileName)
    {
        
    List<LineOfData> parsedDataList = new List<LineOfData>{};

        var xml = XDocument.Load(fileName);

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
    
            LineOfData parsedData = new LineOfData
            {
                Date = date.ToString(_enGB),
                FromAccount = transaction.FromAccount,
                ToAccount = transaction.ToAccount,
                Narrative = transaction.Narrative,
                Amount = transaction.Amount
            };
            parsedDataList.Add(parsedData);
        }

        return parsedDataList;
    }
}