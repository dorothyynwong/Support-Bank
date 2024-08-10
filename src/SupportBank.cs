using System;
using System.Globalization;
using System.IO;
using System.Transactions;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace SupportBank;    

public class SupportBank
{
    private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
    private IFileHandler _fileHandler;
    public List<Transaction> Transactions {get; set;}
    public List<Person> People {get; set;}

    public SupportBank(IFileHandler fileHandler)
    {
        _fileHandler = fileHandler;
        Transactions = new List<Transaction>();
        People = new List<Person>();
    }

    public List<LineOfData> ImportData(string fileName, string fileType)
    {
        try
        {
            _fileHandler.ImportFromFile(fileName);
            return _fileHandler.GetData();
        }
        catch (Exception e)
        {
            Logger.Fatal($"File doesn't have a valid extension.");
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public void ImportAndProcessData(string fileName)
    {
        (Transactions, People) =_fileHandler.ProcessData(fileName);
    }
}