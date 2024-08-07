namespace SupportBank;

public class Program
{
    private static void GetUserChoiceAndReport(Report report) {
        string? userChoice;
        
        do
        {
            Console.WriteLine("Would you like to 1. List All accounts or 2. List a person's account (1/2)?");
            userChoice = Console.ReadLine();
            if (userChoice == "1")
            {
                report.ListAllTransactions();
                Console.WriteLine("Press any button to go back to menu. Press q to exit.");
                userChoice = Console.ReadLine();
            }
            else if (userChoice == "2")
            {
                Console.WriteLine("Which person's account would you like to see?");
                string? personName = Console.ReadLine();
                report.ListTransactionsByPerson(personName);
                Console.WriteLine("Press any button to go back to menu. Press q to exit.");
                userChoice = Console.ReadLine();
            }
            else
            {
                Console.WriteLine("This is not a valid option, please try again, press any button to go back to menu. Press q to exit.");
                userChoice = Console.ReadLine();
            }
        }
        while (userChoice.ToLower() != "q");

    }
    public static void Main()
    {
        string fileName = "./Files/Transactions2014.csv";
        Extractor extractor = new Extractor { FileName = fileName };
        (List<Person>, List<Transaction>) data = extractor.ExtractData();
        Report report = new Report(data.Item1, data.Item2);
    
        GetUserChoiceAndReport(report);
    }
}