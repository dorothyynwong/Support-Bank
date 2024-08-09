using System.IO;

namespace SupportBank;

public static class FileSelector
{
    public static string GetUserFileChoice(string currentDirectory)
    {
        string path = @$"{currentDirectory}\Files";

        string[] files = Directory.GetFiles(path);

        Console.WriteLine("Which file do you want to import?");
        for (int i = 0; i < files.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {files[i]}");
        }

        string userChoice = Console.ReadLine();
        // TryParse returns Boolean - throw exception if false - user has not entered a number
        int.TryParse(userChoice, out int userNumber);

        return files[userNumber - 1];
    }
    
     
}