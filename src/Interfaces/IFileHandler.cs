namespace SupportBank;
public interface IFileHandler {
    IValidator Validator {get; set;}
    IDataProcessor DataProcessor {get; set;}
    void ImportFromFile(string filePath);

    void ImportFromStream(string filePath);
    (List<Transaction>, List<Person>) ProcessData(string filePath);
    List<LineOfData> GetData();
}