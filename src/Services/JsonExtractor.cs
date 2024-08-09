using Newtonsoft.Json;

namespace SupportBank;

public class JsonExtractor : IExtractor
{
    public List<LineOfData> ExtractData(string fileName)
    { 
        string fileContent = File.ReadAllText(fileName);
        List<LineOfData> parsedDataList = JsonConvert.DeserializeObject<List<LineOfData>>(fileContent);
        return parsedDataList;
    }
}