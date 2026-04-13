using System;
using System.Runtime.Versioning;
using System.Threading.Tasks;

public class Chatbot
{
    private string userName = string.Empty;

    public void Start()
    {
        AsciiArt.Display();              //  Show ASCII art
        ConsoleUI.DisplayHeader();

        AudioPlayer player = new AudioPlayer();
        PlayGreetingIfSupported(player); //  Play voice greeting (Windows only)

        AskUserName();

        RunChatLoop().Wait();            //  Run async loop
    }

    [SupportedOSPlatform("windows")]
    private void PlayGreeting(AudioPlayer player)
    {
        player.PlayGreeting();
    }

    private void PlayGreetingIfSupported(AudioPlayer player)
    {
        if (OperatingSystem.IsWindows())
        {
            PlayGreeting(player);
        }
    }

    private void AskUserName()
    {
        Console.Write("Enter your name: ");
        userName = (Console.ReadLine() ?? string.Empty).Trim();

        while (!InputValidator.IsValid(userName))
        {
            Console.Write("Invalid input. Enter your name: ");
            userName = (Console.ReadLine() ?? string.Empty).Trim();
        }

        ConsoleUI.PrintSystem($"Welcome, {userName}! 👋");
    }

    private async Task RunChatLoop()
    {
        while (true)
        {
            Console.Write("\nYou: ");
            string input = (Console.ReadLine() ?? string.Empty).Trim();

            if (!InputValidator.IsValid(input))
            {
                ConsoleUI.PrintError("I didn’t quite understand that. Could you rephrase?");
                continue;
            }

            if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                ConsoleUI.PrintSystem("Goodbye! Stay safe online 🔐");
                break;
            }

            ConsoleUI.ShowTypingIndicator();

            string response = await ResponseHandler.GetResponse(input, userName);

            ConsoleUI.TypeEffect($"Bot: {response}");
        }
    }
}