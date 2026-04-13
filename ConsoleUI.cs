using System;
using System.Threading;

public static class ConsoleUI
{
    public static void DisplayHeader()
    {
        Console.WriteLine(new string('=', 50));
    }

    public static void PrintSystem(string message)
    {
        Console.WriteLine($"[System]: {message}");
    }

    public static void PrintError(string message)
    {
        Console.WriteLine($"[Error]: {message}");
    }

    public static void TypeEffect(string message, int delay = 20)
    {
        foreach (char c in message)
        {
            Console.Write(c);
            Thread.Sleep(delay);
        }
        Console.WriteLine();
    }

    public static void ShowTypingIndicator()
    {
        Console.Write("Bot is typing...");
        Thread.Sleep(800);
        Console.WriteLine();
    }
}