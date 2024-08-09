namespace SupportBank;

public class Transaction
{
    public int Id { get; set; }
    public string Date { get; set; }
    public Person From { get; set; }
    public Person To { get; set; }
    public string Label { get; set; }
    public double Amount { get; set; }
}