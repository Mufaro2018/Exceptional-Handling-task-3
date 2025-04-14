using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Test FormatException");
            Console.WriteLine("2. Test InvalidOperationException");
            Console.WriteLine("3. Test FileNotFoundException");
            Console.WriteLine("4. Test UnauthorizedAccessException");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    TestFormatException();
                    break;
                case "2":
                    TestInvalidOperationException();
                    break;
                case "3":
                    TestFileNotFoundException();
                    break;
                case "4":
                    TestUnauthorizedAccessException();
                    break;
                case "5":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    static void TestFormatException()
    {
        Console.WriteLine("\nTesting FormatException...");
        Console.Write("Enter an integer: ");
        string input = Console.ReadLine();
        try
        {
            int number = int.Parse(input);
            Console.WriteLine($"You entered: {number}");
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"FormatException caught: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("FormatException test completed.");
        }
    }

    static void TestInvalidOperationException()
    {
        Console.WriteLine("\nTesting InvalidOperationException...");
        List<string> names = new List<string> { "Alice", "Bob", "Charlie" };
        Console.WriteLine("Existing names: " + string.Join(", ", names));
        Console.Write("Enter a name to find: ");
        string name = Console.ReadLine();
        try
        {
            string found = names.First(n => n == name);
            Console.WriteLine($"Found: {found}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"InvalidOperationException caught: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("InvalidOperationException test completed.");
        }
    }

    static void TestFileNotFoundException()
    {
        Console.WriteLine("\nTesting FileNotFoundException...");
        Console.Write("Enter a file path to read: ");
        string filePath = Console.ReadLine();
        try
        {
            string content = File.ReadAllText(filePath);
            Console.WriteLine("File content: " + content);
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"FileNotFoundException caught: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("FileNotFoundException test completed.");
        }
    }

    static void TestUnauthorizedAccessException()
    {
        Console.WriteLine("\nTesting UnauthorizedAccessException...");
        string filePath = Path.GetTempFileName();
        Console.WriteLine($"Temporary file created: {filePath}");
        try
        {
            File.SetAttributes(filePath, FileAttributes.ReadOnly);
            Console.WriteLine("Attempting to write to read-only file...");
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("This should throw an exception.");
            }
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"UnauthorizedAccessException caught: {ex.Message}");
        }
        finally
        {
            File.SetAttributes(filePath, FileAttributes.Normal);
            File.Delete(filePath);
            Console.WriteLine("Temporary file deleted.");
            Console.WriteLine("UnauthorizedAccessException test completed.");
        }
    }
}
