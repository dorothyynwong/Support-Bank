namespace SupportBank;

public static class DataProcessor
{
    private static int _personCounter = 1;
    private static int _transactionCounter = 1;

    private static List<Person> people = new List<Person> { };
    private static List<Transaction> transactions = new List<Transaction> { };


    private static Person FindPerson(string personName)
    {
        return people.Find(person => person.Name == personName);
    }

    private static Person CreatePerson(string personName)
    {
        Person person = new Person { Id = _personCounter, Name = personName };
        _personCounter++;
        return person;
    }

    public static (List<Person>, List<Transaction>) ProcessData(List<LineOfData> data)
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

            Transaction transaction = new Transaction
            {
                Id = _transactionCounter,
                Date = line.Date,
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