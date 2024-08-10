using Newtonsoft.Json;

namespace SupportBank;

public class JSONFileHandler : IFileHandler {
    private List<LineOfData> _dataList = new List<LineOfData>{};

    public IValidator Validator { get; set; }
    public IDataProcessor DataProcessor { get; set; }

    public JSONFileHandler()
    {
        Validator = new JSONValidator();
        DataProcessor  = new DataProcessor();
    }

    public void ImportFromFile(string FilePath)
    {
        string fileContent = File.ReadAllText(FilePath);
        _dataList = JsonConvert.DeserializeObject<List<LineOfData>>(fileContent);
    }

    public void ImportFromStream(string FilePath)
    {
        try
        {
            using StreamReader sr = new StreamReader(FilePath);
            using JsonTextReader reader = new JsonTextReader(sr);

            JsonSerializer serializer = new JsonSerializer();
            // List<ParsedData> parsedDataList = serializer.Deserialize<List<ParsedData>>(reader);
            
            reader.Read();
            if (reader.TokenType != JsonToken.StartArray)
                throw new JsonReaderException("Expected StartArray token");

            while (reader.Read() && reader.TokenType != JsonToken.EndArray)
            {
                ParsedData parsedData = serializer.Deserialize<ParsedData>(reader);
                if (parsedData != null)
                {
                    if (Validator.isValidTransaction(parsedData))
                    {
                        DataProcessor.CreateTransaction(parsedData);
                    }
                    else
                    {
                        throw new Exception("Invalid Transaction");
                    }

                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
    }

    public List<LineOfData> GetData()
    {
        return _dataList;
    }

    public (List<Transaction>, List<Person>) ProcessData(string filePath)
    {
        ImportFromStream(filePath);
        return (DataProcessor.transactions, DataProcessor.people);
    }
}