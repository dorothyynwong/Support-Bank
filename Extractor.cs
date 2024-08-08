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
    // private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
    // private CultureInfo _enGB = new CultureInfo("en-GB");

    private int _personCounter = 1;
    private int _transactionCounter = 1;
    public string FileName { get; set; }
    private string[] GetDataFromCsvFile()
    {
        string[] fileContent = File.ReadAllLines(FileName);
        return fileContent;
    }


    // private List<string>? GetDataFromFile()
    // {
    //     List<string> lines = new List<string>();
    //     try
    //     {   
    //         using (StreamReader sr = new StreamReader(FileName))
    //         {
    //             string line;
    //             line = sr.ReadLine(); //skip the header line

    //             if (line != "Date,From,To,Narrative,Amount")
    //             {
    //                 throw new Exception("Invalid File");
    //                 Logger.Fatal($"The header is in an incorrect format.\nCurrent header: {line}");
    //             }

    //             while ((line = sr.ReadLine()) != null)
    //             {
    //                 lines.Add(line);
    //             }
    //         }
    //         return lines;
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine("The file could not be read:");
    //         Console.WriteLine(e.Message);
    //         return null;
    //     }
    // }

    // private Boolean isValidLine(string[] data)
    // {
    //     return 
    //     (
    //         double.TryParse(data[4], out double amount) 
    //         && DateTime.TryParseExact(data[0], "dd/MM/yyyy", _enGB , DateTimeStyles.None, out DateTime _)
    //     );
    // }

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

    // public (List<Person>, List<Transaction>) ExtractJsonData()
    public List<LineOfData> ExtractJsonData()
    {
        string fileContent = File.ReadAllText(FileName);
        List<LineOfData> linesOfDataList = JsonConvert.DeserializeObject<List<LineOfData>>(fileContent);
        return linesOfDataList;
    }

    public List<LineOfData> ExtractCsvData()
    {
        // List<Person> people = new List<Person> { };
        // List<Transaction> transactions = new List<Transaction> { };

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

        // foreach (string line in lines)
        // {
        //     try
        //     {
        //         string[] data = line.Split(",");

        //         if (isValidLine(data))
        //         {
        //             string date = data[0];
        //             string fromName = data[1];
        //             string toName = data[2];
        //             string label = data[3];
        //             double.TryParse(data[4], out double amount);

        //             Person fromPerson = FindPerson(fromName, people);
        //             if (fromPerson == null)
        //             {
        //                 fromPerson = CreatePerson(fromName);
        //                 people.Add(fromPerson);
        //             }

        //             Person toPerson = FindPerson(toName, people);
        //             if (toPerson == null)
        //             {
        //                 toPerson = CreatePerson(toName);
        //                 people.Add(toPerson);
        //             }
        //             Transaction transaction = new Transaction
        //             {
        //                 Id = _transactionCounter,
        //                 Date = date,
        //                 From = fromPerson,
        //                 To = toPerson,
        //                 Label = label,
        //                 Amount = amount
        //             };

        //             transactions.Add(transaction);

        //             _transactionCounter++;
        //         }
        //         else
        //         {
        //             throw new Exception("Invalid line");
        //         }
        //     }
        //     catch (Exception e)
        //     {
        //         Logger.Error($"The line is in an incorrect format.\nIncorrect line: {line}");
        //         Console.WriteLine($"Invalid data: {e.Message}");
        //         Console.WriteLine("The line could not be read:");
        //         Console.WriteLine(line);
        //     }
        // }
        // return (people, transactions);
    }
}