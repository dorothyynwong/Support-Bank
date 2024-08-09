namespace SupportBank;

public interface IExtractor
{
    List<LineOfData> ExtractData(string fileName);
}
