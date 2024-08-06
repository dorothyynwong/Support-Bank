using System;
using System.IO;

namespace SupportBank;

public class Extractor {
    public string FileName {get; set;}

    private void GetDataFromFile() {
        try
        {
            // Create an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader.
            using (StreamReader sr = new StreamReader(FileName))
            {
                string line;
                // Read and display lines from the file until the end of
                // the file is reached.
                line = sr.ReadLine(); //skip the header line
                int transactionCounter = 1;
                int personCounter = 1;

                Dictionary<int, string> people = new Dictionary<int, string>{};
                

                while ((line = sr.ReadLine()) != null)
                {
                    // Console.WriteLine(line);
                    string[] data = line.Split(",");

                    Person fromPerson = new Person {
                        Id = personCounter,
                        Name = line[1]
                    }

                    if(!people.Contains(fromPerson)) {
                        people.add(fromPerson);
                        personCounter++;
                    }

                    Person toPerson = new Person {
                        Id = personCounter,
                        Name = line[2]
                    }

                    if(!people.Contains(toPerson)) {
                        people.add(fromPerson);
                        personCounter++;
                    }

                    Transaction transaction = new Transaction 
                    {
                        Id = transactionCounter, 
                        Date = line[0], 
                        Label = line[3], 
                        Amount = line[4] 
                    }

                    count++;
                }
            }
        }
        catch (Exception e)
        {
            // Let the user know what went wrong.
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
    }

    public List<Person> ExtractPeople () {
        List<Person> people = new List<Person>{};
        return people;
    }

    public List<Transaction> ExtractTransactions () {
        List<Transaction> transactions = new List<Transaction>{}; 
        return transactions;
    }
}