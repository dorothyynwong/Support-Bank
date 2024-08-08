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
    {
        this.People = people;
        this.Transactions = transactions;
    }

    private string[] PrintAllTransactions ()
    {
        string[] fileContent;
        foreach(Person person in People)
        {
            fileContent += $"Name: {person.Name} \n";
            double lentValue = CalculateAmountLent(person);
            string amountLent = String.Format("{0:0.00}", Math.Round(lentValue, 2));
            fileContent += $"Total amount lent: {amountLent} \n";
            double owedValue = CalculateAmountOwed(person);
            string amountOwed = String.Format("{0:0.00}", Math.Round(owedValue, 2));
            fileContent += $"Total amount owed: {amountOwed}";
        }
        return fileContent;
    }
    public void ExportFile(string fileName)
    {
        string[] fileContent = PrintAllTransactions();
        File.WriteAllLines(fileName, fileContent);
    }   
}