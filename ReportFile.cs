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
            fileContent.Add($"Name: {person.Name} \n");
            double lentValue = CalculateAmountLent(person);
            string amountLent = String.Format("{0:0.00}", Math.Round(lentValue, 2));
            fileContent.Add($"Total amount lent: {amountLent} \n");
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