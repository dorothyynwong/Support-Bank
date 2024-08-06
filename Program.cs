namespace SupportBank;

public class Program
{
    public static void Main()
    {
        string fileName = "./Files/Transactions2014.csv";
        Extractor extractor = new Extractor { FileName = fileName };
        (List<Person>, List<Transaction>) data = extractor.ExtractData();
        // foreach(var person in data.Item1) Console.WriteLine(person.Name);
        // foreach(var transaction in data.Item2) Console.WriteLine(transaction.Amount);
        Report report = new Report(data.Item1, data.Item2);
        // Person testPerson = new Person {Id = 1, Name = "Jon A"};
        // Console.WriteLine(report.CalculateAmountLent(testPerson));
        report.ListAllTransactions();
    }
}