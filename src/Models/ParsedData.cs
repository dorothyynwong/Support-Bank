namespace SupportBank;

public class ParsedData {
    public DateTime Date { get; set; }

    public string FromAccount { get; set; }

    public string ToAccount { get; set; }

    public string Narrative { get; set; }

    public double Amount { get; set; }
}