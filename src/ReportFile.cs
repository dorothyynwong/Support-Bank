namespace SupportBank;

public class ReportFile : Report
{
    public ReportFile(List<Person> people, List<Transaction> transactions) 
            : base(people, transactions)
    {

    }

    private List<string> PrintAllTransactions ()
    {
        List<string> fileContent = new List<string>{};
        foreach(Person person in People)
        {
            fileContent.Add($"Name: {person.Name}");
            double lentValue = CalculateAmountLent(person);
            string amountLent = String.Format("{0:0.00}", Math.Round(lentValue, 2));
            fileContent.Add($"Total amount lent: {amountLent}");
            double owedValue = CalculateAmountOwed(person);
            string amountOwed = String.Format("{0:0.00}", Math.Round(owedValue, 2));
            fileContent.Add($"Total amount owed: {amountOwed}");
        }
        return fileContent;
    }
    public void ExportFile(string fileName)
    {
        List<string> fileContent = PrintAllTransactions();
        File.WriteAllLines(fileName, fileContent);
    }   
}