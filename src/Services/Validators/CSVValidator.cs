namespace SupportBank;

public class CSVValidator: IValidator
{
    private bool IsHeaderValid(string header)
    {
        return header == "Date,From,To,Narrative,Amount";
    }

    public bool isValidTransaction(ParsedData parsedData)
    {
        return true;
    }

    public bool isValidLine(LineOfData line, string dataSource)
    {
        return true;
    }
    
    public List<LineOfData> ValidateData(List<LineOfData> data, string dataSource)
    {
        return data;
    }
}