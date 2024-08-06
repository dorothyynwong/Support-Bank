namespace SupportBank;

public class Report
{
    public List<Person> People;
    public List<Transaction> Transactions;
    
    public Report(List<Person> people, List<Transaction> transactions) {
        this.People = people;
        this.Transactions = transactions;
    }

    public void ListAllTransactions ()
    {
        Console.WriteLine("list of transactions");
    }

    public void ListTransactionsByPerson(Person person)
    {
        Console.WriteLine("list of people and their transactions");
    }
}