using System;
using System.Globalization;
using System.IO;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace SupportBank;    

public class Program
{
    private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

    private static string currentDirectory = System.IO.Directory.GetCurrentDirectory();
    public Program()
    {
        // string currentDirectory = System.IO.Directory.GetCurrentDirectory();

        var config = new LoggingConfiguration();
        var target = new FileTarget { FileName = @$"{currentDirectory}\Logs\SupportBank.log", Layout = @"${longdate} ${level} - ${logger}: ${message}" };
        config.AddTarget("File Logger", target);
        config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, target));
        LogManager.Configuration = config;
    }

    private static string GetFileExtension(string fileName)
    {
        string extension = Path.GetExtension(fileName).Replace(".", ""); 
        return extension;
    }

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

    private static List<LineOfData> ImportData(string fileName, string fileType)
    {
        IExtractor extractor;
        try
        {
            switch (fileType)
            {
                case "csv":
                    extractor = new CsvExtractor();
                    break;
                case "json":
                    extractor = new JsonExtractor();
                    break;
                case "xml":
                    extractor = new XmlExtractor();
                    break;
                default:
                    throw new Exception("Invalid file extension");
                    break;
            }
            return extractor.ExtractData(fileName);
        }
        catch (Exception e)
        {
            Logger.Fatal($"File doesn't have a valid extension.");
            Console.WriteLine(e.Message);
            return null;
        }
    }
    
    public static void Main()
    {
        string fileName = FileSelector.GetUserFileChoice(currentDirectory);

        string fileType = GetFileExtension(fileName);

        List<LineOfData> lines = ImportData(fileName, fileType);

        if (lines != null)
        {
            List<LineOfData> validLines = Validator.ValidateLines(lines, fileType);

            (List<Person>, List<Transaction>) processedData = DataProcessor.ProcessData(validLines);


            Report report = new Report(processedData.Item1, processedData.Item2);
            GetUserChoiceAndReport(report);

            ReportFile reportFile = new ReportFile(processedData.Item1, processedData.Item2);
            reportFile.ExportFile("./Files/Output/Transactions2012_xml.txt");
        }
        else Console.WriteLine("File cannot be imported");
    }
}