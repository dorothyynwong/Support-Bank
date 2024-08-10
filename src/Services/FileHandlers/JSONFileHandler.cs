using Newtonsoft.Json;

namespace SupportBank;

public class JSONFileHandler : IFileHandler {
    private List<LineOfData> _dataList = new List<LineOfData>{};

    public IValidator Validator { get; set; }
    public IDataProcessor DataProcessor { get; set; }

    public void ImportFile(string FilePath)
    {
        string fileContent = File.ReadAllText(FilePath);
        _dataList = JsonConvert.DeserializeObject<List<LineOfData>>(fileContent);
    }

    public List<LineOfData> GetData()
    {
        return _dataList;
    }
}