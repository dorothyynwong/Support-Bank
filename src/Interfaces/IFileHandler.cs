namespace SupportBank;
public interface IFileHandler {
    IValidator Validator {get; set;}
    IDataProcessor DataProcessor {get; set;}
    void ImportFile(string filePath);
    List<LineOfData> GetData();
}