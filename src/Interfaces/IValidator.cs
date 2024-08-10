namespace SupportBank;
public interface IValidator {
    // bool IsHeaderValid(LineOfData header);
    bool isValidLine(LineOfData line, string dataSource);
    List<LineOfData> ValidateData(List<LineOfData> data, string dataSource);
    bool isValidTransaction(ParsedData parsedData);
}