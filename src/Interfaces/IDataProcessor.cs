namespace SupportBank;
public interface IDataProcessor {
    List<Transaction> transactions {get; set;}
    List<Person> people{get; set;}

    (List<Person>, List<Transaction>) ProcessData(List<LineOfData> data);

}