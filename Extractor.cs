using System;
using System.Globalization;
using System.IO;
using NLog;
using NLog.Config;
using NLog.Targets;
using Newtonsoft.Json;
using System.Linq;
using System.Xml.Linq;

namespace SupportBank;

public class Extractor
{
    private CultureInfo _enGB = new CultureInfo("en-GB");
    private int _personCounter = 1;
    private int _transactionCounter = 1;
    public string FileName { get; set; }
   
    public List<LineOfData> ExtractXmlData() 
    {
        List<LineOfData> linesOfDataList = new List<LineOfData>{};

        var xml = XDocument.Load(FileName);

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
            linesOfDataList.Add(line);
        }

        return linesOfDataList;
    }

    public List<LineOfData> ExtractJsonData()
    {
        string fileContent = File.ReadAllText(FileName);
        List<LineOfData> linesOfDataList = JsonConvert.DeserializeObject<List<LineOfData>>(fileContent);
        return linesOfDataList;
    }

    public List<LineOfData> ExtractCsvData()
    {
        string[] lines = File.ReadAllLines(FileName);
        if (lines == null) return null;

        List<LineOfData> dataList = new List<LineOfData>{};

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

            dataList.Add(dataLine);
        }

        return dataList;
    }
}