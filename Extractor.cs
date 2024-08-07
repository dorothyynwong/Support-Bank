using System;
using System.IO;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace SupportBank;

public class Extractor {
    private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
    public string FileName {get; set;}
    
    private List<string>? GetDataFromFile() {
        List<string> lines = new List<string>();
        try
        {
            using (StreamReader sr = new StreamReader(FileName))
            {
                string line;
                
                line = sr.ReadLine(); //skip the header line

                if (line != "Date,From,To,Narrative,Amount")
                {
                    Logger.Fatal($"The header is in an incorrect format.\nCurrent header: {line}");
                    throw new Exception("Invalid File");
                }

                while ((line = sr.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            return lines;
        }
        catch (Exception e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public (List<Person>, List<Transaction>) ExtractData () {
        List<Person> people = new List<Person> {};
        List<Transaction> transactions = new List<Transaction> {};
        
        List<string> lines = GetDataFromFile();
        if (lines == null) return (null, null);

        int transactionCounter = 1;
        int personCounter = 1;

        foreach (string line in lines) {
            string[] data = line.Split(",");

            string date = data[0];
            string fromName = data[1];
            string toName = data[2];
            string label = data[3];
            double.TryParse(data[4], out double amount);
           
            Person? fromPerson = people.Find(person => person.Name == fromName);
            if (fromPerson == null) {
                fromPerson = new Person{
                    Id = personCounter,
                    Name = fromName
                };
                people.Add(fromPerson);
                personCounter++;
            }

            Person? toPerson = people.Find(person => person.Name == toName);
            if (toPerson == null) {
                toPerson = new Person{
                    Id = personCounter,
                    Name = toName
                };
                people.Add(toPerson);
                personCounter++;
            }

            Transaction transaction = new Transaction 
            {
                Id = transactionCounter, 
                Date = date, 
                From = fromPerson,
                To = toPerson,
                Label = label, 
                Amount = amount 
            };

            transactions.Add(transaction);

            transactionCounter++;
        }

        return (people, transactions);
    }
}