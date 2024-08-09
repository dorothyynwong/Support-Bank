namespace SupportBank;

public class CsvExtractor : IExtractor
{
    public string FileName { get; set; }
    public List<LineOfData> ExtractData(string fileName)
    {
        string[] lines = File.ReadAllLines(FileName);
        if (lines == null) return null;

        List<LineOfData> dataList = new List<LineOfData>{};

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

            dataList.Add(dataLine);
        }

        return dataList;
    }
}