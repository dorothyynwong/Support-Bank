using System.Globalization;

namespace SupportBank;

public class DataProcessor: IDataProcessor
{
    private int _personCounter = 1;
    private int _transactionCounter = 1;

    public List<Transaction> transactions {get; set;}
    public List<Person> people{get; set;}

    public DataProcessor() 
    {
        transactions = new List<Transaction>();
        people = new List<Person>();
    }

    public void CreateTransaction(ParsedData parsedData)
    {
        Person fromPerson = FindPerson(parsedData.FromAccount);
            if (fromPerson == null)
            {
                fromPerson = CreatePerson(parsedData.FromAccount);
                people.Add(fromPerson);
            }

            Person toPerson = FindPerson(parsedData.ToAccount);
            if (toPerson == null)
            {
                toPerson = CreatePerson(parsedData.ToAccount);
                people.Add(toPerson);
            }

            Transaction transaction = new Transaction
            {
                Id = _transactionCounter,
                Date = parsedData.Date,
                From = fromPerson,
                To = toPerson,
                Label = parsedData.Narrative,
                Amount = parsedData.Amount
            };

            transactions.Add(transaction);

            _transactionCounter++;
    }

    public (List<Person>, List<Transaction>) ProcessData()
    {
        return (people, transactions);
    }

    private  Person FindPerson(string personName)
    {
        return people.Find(person => person.Name == personName);
    }

    private  Person CreatePerson(string personName)
    {
        Person person = new Person { Id = _personCounter, Name = personName };
        _personCounter++;
        return person;
    }

    public (List<Person>, List<Transaction>) ProcessData(List<LineOfData> data)
    {
        if (data == null) return (null, null);

        foreach (LineOfData line in data)
        {
            Person fromPerson = FindPerson(line.FromAccount);
            if (fromPerson == null)
            {
                fromPerson = CreatePerson(line.FromAccount);
                people.Add(fromPerson);
            }

            Person toPerson = FindPerson(line.ToAccount);
            if (toPerson == null)
            {
                toPerson = CreatePerson(line.ToAccount);
                people.Add(toPerson);
            }
            DateTime date;
            DateTime.TryParseExact(line.Date, "dd/MM/yyyy" , new CultureInfo("en-GB") , DateTimeStyles.None, out date);
            Transaction transaction = new Transaction
            {
                Id = _transactionCounter,
                Date = date,
                From = fromPerson,
                To = toPerson,
                Label = line.Narrative,
                Amount = double.Parse(line.Amount)
            };

            transactions.Add(transaction);

            _transactionCounter++;
        }
        return (people, transactions);
    }
}