namespace SupportBank;

public class CSVFileHandler : IFileHandler {
    private List<LineOfData> _dataList = new List<LineOfData>{};

    public void ImportFile(string FilePath)
    {
        string[] lines = File.ReadAllLines(FilePath);

        foreach(string line in lines)
        {
            string[] data = line.Split(",");
            LineOfData dataLine = new LineOfData{
                Date = data[0],
                FromAccount = data[1],
                ToAccount = data[2],
                Narrative = data[3],
                Amount = data[4]
            };

            _dataList.Add(dataLine);
        }
    }

    public List<LineOfData> GetData()
    {
        return _dataList;
    }
}