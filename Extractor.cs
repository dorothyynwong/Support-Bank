using System;
using System.IO;

namespace SupportBank;

public class Extractor {
    public string FileName {get; set;}

    private List<string> GetDataFromFile() {
        List<string> lines = new List<string>();

        try
        {
            using (StreamReader sr = new StreamReader(FileName))
            {
                string line;
                
                line = sr.ReadLine(); //skip the header line

                while ((line = sr.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }

        return lines;
    }

    public (List<Person>, List<Transaction>) ExtractData () {
        List<Person> people = new List<Person> {};
        List<Transaction> transactions = new List<Transaction> {};
        
        List<string> lines = GetDataFromFile();

        int transactionCounter = 1;
        int personCounter = 1;

        Dictionary<string, int> peopleNames = new Dictionary<string, int>{};

        foreach (string line in lines) {
            string[] data = line.Split(",");

            string date = data[0];
            string fromName = data[1];
            string toName = data[2];
            string label = data[3];
            float.TryParse(data[4], out float amount);
           
            Person fromPerson = new Person{
                Id = peopleNames.ContainsKey(fromName) ? peopleNames[fromName] : personCounter,
                Name = fromName
            };

            if (!peopleNames.ContainsKey(fromName))
            {
                peopleNames.Add(fromName, personCounter);

                people.Add(fromPerson);    

                personCounter++;
            }

             Person toPerson = new Person{
                Id = peopleNames.ContainsKey(toName) ? peopleNames[toName] : personCounter,
                Name = toName
            };

            if (!peopleNames.ContainsKey(toName))
            {
                peopleNames.Add(toName, personCounter);

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