namespace SupportBank;

public class Program
{
    public static void Main()
    {
        string fileName = "test.csv";
        Extractor extractor = new Extractor { FileName = fileName };
        List<Person> listOfPeople = extractor.ExtractPeople();
        List<Transaction> listOfTransactions = extractor.ExtractTransactions();
        Report report = new Report(listOfPeople, listOfTransactions);
    }
}