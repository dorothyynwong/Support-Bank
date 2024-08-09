namespace SupportBank;

public static class FileHelper 
{
    public static string GetFileExtension(string fileName)
    {
        string extension = Path.GetExtension(fileName).Replace(".", ""); 
        return extension;
    }
}