using NLog;
using NLog.Config;
using NLog.Targets;

namespace SupportBank;

public class Program
{
    private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
    private static void GetUserChoiceAndReport(Report report)
    {
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

    private static List<LineOfData> ImportCsvFile(string fileName)
    {
        Logger.Info($"Importing file {fileName}");
        Extractor extractor = new Extractor { FileName = fileName };
        List<LineOfData> data = extractor.ExtractCsvData();
        foreach (var line in data)
        {
            Console.WriteLine(line.FromAccount);
        }
        return data;
    }

    private static List<LineOfData> ImportJsonFile(string fileName)
    {
        Extractor extractor = new Extractor { FileName = fileName };
        List<LineOfData> data = extractor.ExtractJsonData();
        foreach (var line in data)
        {
            Console.WriteLine(line.FromAccount);
        }
        return data;
    }
    
    public static void Main()
    {
        string currentDirectory = System.IO.Directory.GetCurrentDirectory();

        var config = new LoggingConfiguration();
        var target = new FileTarget { FileName = @$"{currentDirectory}\Logs\SupportBank.log", Layout = @"${longdate} ${level} - ${logger}: ${message}" };
        config.AddTarget("File Logger", target);
        config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, target));
        LogManager.Configuration = config;

        // (List<Person>, List<Transaction>) data = ImportCsvFile("./Files/DodgyTransactions2015.csv");
        
        // if(data != (null, null)) 
        // {
        //     Report report = new Report(data.Item1, data.Item2);
        //     GetUserChoiceAndReport(report);
        // }

        // ImportCsvFile("./Files/DodgyTransactions2015.csv");
        ImportJsonFile("./Files/Transactions2013.json");
    }
}