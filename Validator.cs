using System;
using System.Globalization;
using System.IO;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace SupportBank;

public class Validator
{
    private CultureInfo _enGB = new CultureInfo("en-GB");
    private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

    private Boolean IsHeaderValid(LineOfData header) 
    {
        return header.Date == "Date" && 
               header.FromAccount == "From" &&
               header.ToAccount == "To" &&
               header.Narrative == "Narrative" &&
               header.Amount == "Amount";
    }

    private Boolean isValidLine(LineOfData line)
    {
        return 
        (
            double.TryParse(line.Amount, out double amount) 
            && DateTime.TryParseExact(line.Date, "dd/MM/yyyy", _enGB , DateTimeStyles.None, out DateTime _)
        );
    }

    public List<LineOfData> ValidateLines(List<LineOfData> data, string dataSource) 
    {
        List<LineOfData> validLines = new List<LineOfData>{};
        if (dataSource == "csv") 
        {
            try 
            {
                if (!IsHeaderValid(data[0]))
                {
                    string header = $"{data[0].Date}, {data[0].FromAccount}, {data[0].ToAccount}, {data[0].Narrative}, {data[0].Amount}";
                    string expectedHeader = "Date,From,To,Narrative,Amount";
                    throw new Exception("Invalid File");
                    Logger.Fatal($"The header is in an incorrect format.\nExpected header: {expectedHeader} \nCurrent header: {header}.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return null;
            }
        }

        foreach(LineOfData line in data) 
        {
            try
            {
                if (isValidLine(line))
                {
                    validLines.Add(line);
                }
                else
                {
                    throw new Exception("Invalid line");
                }
            }
            catch (Exception e)
            {
                string problemLine = $"{line.Date}, {line.FromAccount}, {line.ToAccount}, {line.Narrative}, {line.Amount}";
                Logger.Error($"The line is in an incorrect format.\nIncorrect line: {problemLine}");
                Console.WriteLine($"Invalid data: {e.Message}");
                Console.WriteLine("The line could not be read:");
                Console.WriteLine(problemLine);
            }
        }

        return validLines.Count>0 ? validLines : null;
    }

}