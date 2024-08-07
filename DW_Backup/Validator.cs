using System;
using System.Globalization;
using System.IO;
using NLog;
using NLog.Config;
using NLog.Targets;
using Newtonsoft.Json;


namespace SupportBank;

public class Validator
{
    private  List<string[]> _dataList;
    private CultureInfo _enGB = new CultureInfo("en-GB");
    private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
    public Validator(List<string[]> dataList) 
    {
        this._dataList = dataList;
    }

    public List<string[]> GetValidLines()
    {
        List<string[]> validLines = new List<string[]>();
        foreach(string[] data in _dataList)
        if
        (
            double.TryParse(data[4], out double amount) 
            && DateTime.TryParseExact(data[0], "dd/MM/yyyy", _enGB , DateTimeStyles.None, out DateTime _)
        )
        {
            validLines.Add(data);
        }
        return validLines;
    }

    public Boolean isHeaderValid() 
    {
        string[] header = _dataList[0];
        
        // if (header != "Date,From,To,Narrative,Amount")
        // {
        //     throw new Exception("Invalid File");
        //     Logger.Fatal($"The header is in an incorrect format.\nCurrent header: {header}");
        //     return false;
        // }
        if (header[0] != "Date" || header[1] != "From" || header[2] != "To" || header[3] != "Narrative" || header[4] != "Amount") return false;

        else return true;
    }

    // public Boolean ValidateFile() 
    // {
    //             string currentDirectory = System.IO.Directory.GetCurrentDirectory();

    //     var config = new LoggingConfiguration();
    //     var target = new FileTarget { FileName = @$"{currentDirectory}\Logs\SupportBank.log", Layout = @"${longdate} ${level} - ${logger}: ${message}" };
    //     config.AddTarget("File Logger", target);
    //     config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, target));
    //     LogManager.Configuration = config;
    //     if (lines != null && isHeaderValid(lines[0])) {
    //         foreach(string line in lines) {
    //             string[] data = line.Split(",");
    //             isValidLine(data);
    //         }
    //         return true;
    //     } else return false;
    // }
}