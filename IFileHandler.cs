namespace SupportBank;
public interface IFileHandler {
    void ImportFile(string filePath);
    List<LineOfData> GetData();
}