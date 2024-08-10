namespace SupportBank;

public class JSONValidator: IValidator
{
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