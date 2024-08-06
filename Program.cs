namespace SupportBank;

public class Program
{
    public static void Main()
    {
        string fileName = "test.csv";
        Extractor extractor = new Extractor { FileName = fileName };
        (List<Person>, List<Transaction>) data = extractor.ExtractData();
        Report report = new Report(data.Item1, data.Item2);
    }
}