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

    public double CalculateAmountLent(Person person) {
        double total = 0;
        List<Transaction> fromTransactions = Transactions.FindAll(transaction => transaction.From.Name == person.Name);
        foreach(Transaction fromTransaction in fromTransactions) total += fromTransaction.Amount;
        total = Math.Round(total, 2);
        return total;
    }

    public double CalculateAmountOwed(Person person) {
        double total = 0;
        return total;
    }

}