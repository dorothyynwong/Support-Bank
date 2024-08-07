﻿using NLog;
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

    private static (List<Person>, List<Transaction>) ImportCsvFile(string fileName)
    {
        Logger.Info($"Importing file {fileName}");
        Extractor extractor = new Extractor { FileName = fileName };
        // (List<Person>, List<Transaction>) data = extractor.ExtractData();
        // return data;
        List<string[]> dataList = extractor.ExtractCsvData();
        Validator validator = new Validator(dataList);
        if (dataList.Count>0 && validator.isHeaderValid())
        {      
            List<string[]> validLines = validator.GetValidLines();
            foreach(string[] validLine in validLines) Console.WriteLine(validLine[4]);
        }
  
        return (null, null);
    }

    private static void ImportJsonFile(string fileName)
    {
        Extractor extractor = new Extractor { FileName = fileName };
        extractor.ExtractJsonData();
    }
    
    public static void Main()
    {
        // string currentDirectory = System.IO.Directory.GetCurrentDirectory();

        // var config = new LoggingConfiguration();
        // var target = new FileTarget { FileName = @$"{currentDirectory}\Logs\SupportBank.log", Layout = @"${longdate} ${level} - ${logger}: ${message}" };
        // config.AddTarget("File Logger", target);
        // config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, target));
        // LogManager.Configuration = config;

        (List<Person>, List<Transaction>) data = ImportCsvFile("./Files/EmptyFile.csv");
        
        if(data != (null, null)) 
        {
            Report report = new Report(data.Item1, data.Item2);
            GetUserChoiceAndReport(report);
        }

        // ImportJsonFile("./Files/Transactions2013.json");
    }
}