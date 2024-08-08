namespace SupportBank;

public class ReportFile : Report
{
    // private string[] FormatReportContent()
    // {
    //     foreach (var transaction in transactions)
    //     {
    //         string formattedLine = $
    //     }
    // }

    public ReportFile(List<Person> people, List<Transaction> transactions)
        : base(people, transactions) // Call to base class constructor
    {

    }

    private List<string> PrintAllTransactions ()
    {
        List<string> fileContent = new List<string>{};
        foreach(Person person in People)
        {
            List<Transaction> transactionsOfPerson = Transactions.FindAll(transaction => transaction.From.Name == person.Name);
            transactionsOfPerson.Concat(Transactions.FindAll(transaction => transaction.To.Name == person.Name));
            fileContent.Add($"Name: {person.Name}");
            foreach (Transaction transaction in transactionsOfPerson)
            {
                double amountValue = transaction.Amount;
                string amount = String.Format("{0:0.00}", Math.Round(amountValue, 2));

                fileContent.Add($"Date: {transaction.Date} | " +
                                $"Label: {transaction.Label} | " +
                                $"To: {transaction.To.Name} | " +
                                $"Amount: {amount}");
            }
            // fileContent.Add($"Name: {person.Name}");
            // double lentValue = CalculateAmountLent(person);
            // string amountLent = String.Format("{0:0.00}", Math.Round(lentValue, 2));
            // fileContent.Add($"Total amount lent: {amountLent}");
            // double owedValue = CalculateAmountOwed(person);
            // string amountOwed = String.Format("{0:0.00}", Math.Round(owedValue, 2));
            // fileContent.Add($"Total amount owed: {amountOwed}");
        }
        return fileContent;
    }
    public void ExportFile(string fileName)
    {
        List<string> fileContent = PrintAllTransactions();
        File.WriteAllLines(fileName, fileContent);
    }   
}