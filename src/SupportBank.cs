using System;
using System.Globalization;
using System.IO;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace SupportBank;    

public class SupportBank
{
    private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
    private IFileHandler _fileHandler;

    public SupportBank(IFileHandler fileHandler)
    {
        _fileHandler = fileHandler;
    }

    public List<LineOfData> ImportData(string fileName, string fileType)
    {
        try
        {
            _fileHandler.ImportFile(fileName);
            return _fileHandler.GetData();
        }
        catch (Exception e)
        {
            Logger.Fatal($"File doesn't have a valid extension.");
            Console.WriteLine(e.Message);
            return null;
        }
    }
}