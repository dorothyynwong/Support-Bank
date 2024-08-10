namespace SupportBank;
public interface IValidator {
    bool IsHeaderValid(LineOfData header);
    bool isValidLine(LineOfData line, string dataSource);
    List<LineOfData> ValidateLines(List<LineOfData> data, string dataSource);
}