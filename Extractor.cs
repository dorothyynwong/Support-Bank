using System;
using System.Globalization;
using System.IO;
using NLog;
using NLog.Config;
using NLog.Targets;
using Newtonsoft.Json;

namespace SupportBank;

public class Extractor
{
    private int _personCounter = 1;
    private int _transactionCounter = 1;
    public string FileName { get; set; }
    private string[] GetDataFromCsvFile()
    {
        string[] fileContent = File.ReadAllLines(FileName);
        return fileContent;
    }

    private Person FindPerson(string personName, List<Person> people)
    {
        return people.Find(person => person.Name == personName);
    }

    private Person CreatePerson(string personName) 
    {
        Person person = new Person {Id = _personCounter, Name = personName };
        _personCounter++;
        return person;
    }

    public List<LineOfData> ExtractJsonData()
    {
        string fileContent = File.ReadAllText(FileName);
        List<LineOfData> linesOfDataList = JsonConvert.DeserializeObject<List<LineOfData>>(fileContent);
        return linesOfDataList;
    }

    public List<LineOfData> ExtractCsvData()
    {
        string[] lines = GetDataFromCsvFile();
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