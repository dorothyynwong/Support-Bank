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
        foreach(Person person in People)
        {
            Console.WriteLine($"Name: {person.Name}");
            double lentValue = CalculateAmountLent(person);
            string amountLent = String.Format("{0:0.00}", Math.Round(lentValue, 2));
            Console.WriteLine($"Total amount lent: {amountLent}");
            double owedValue = CalculateAmountOwed(person);
            string amountOwed = String.Format("{0:0.00}", Math.Round(owedValue, 2));
            Console.WriteLine($"Total amount owed: {amountOwed}");
        }

    }

    public void ListTransactionsByPerson(string personName)
    {   
        List<Transaction> transactions = Transactions.FindAll(transaction => transaction.From.Name == personName);
        Console.WriteLine($"Name: {personName}");

        foreach(Transaction transaction in transactions)
        {
            double amountValue = transaction.Amount;
             string amount = String.Format("{0:0.00}", Math.Round(amountValue, 2));

            Console.Write($"Date: {transaction.Date} | ");
            Console.Write($"Label: {transaction.Label} | ");
            Console.Write($"To: {transaction.To.Name} | ");
            Console.WriteLine($"Amount: {amount}");
        }
        
    }

    public double CalculateAmountLent(Person person) {
        double total = 0;
        List<Transaction> fromTransactions = Transactions.FindAll(transaction => transaction.From.Name == person.Name);
        foreach(Transaction fromTransaction in fromTransactions) total += fromTransaction.Amount;
        return total;
    }

    public double CalculateAmountOwed(Person person) {
        double total = 0;
        List<Transaction> toTransactions = Transactions.FindAll(transaction => transaction.To.Name == person.Name);
        foreach(Transaction toTransaction in toTransactions) total += toTransaction.Amount;
        return total;
    }

}