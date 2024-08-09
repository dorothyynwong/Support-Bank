using Newtonsoft.Json;

namespace SupportBank;

public class JsonExtractor : IExtractor
{
    public string FileName { get; set; }
    public List<LineOfData> ExtractData(string fileName)
    { 
        string fileContent = File.ReadAllText(FileName);
        List<LineOfData> parsedDataList = JsonConvert.DeserializeObject<List<LineOfData>>(fileContent);
        return parsedDataList;
    }
}