using System;
using System.IO;
using System.Media;
using System.Runtime.Versioning;

public interface IAudioPlayer
{
    void PlayGreeting();
}

public class AudioPlayer : IAudioPlayer
{
    [SupportedOSPlatform("windows")]
    public void PlayGreeting()
    {
        try
        {
            string path = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Voice Message",
                "greeting.wav"
            );

            if (File.Exists(path))
            {
                using var player = new SoundPlayer(path);
                player.PlaySync(); // ✅ plays fully before continuing
            }
            else
            {
                ConsoleUI.PrintError("Voice greeting file not found.");
            }
        }
        catch (Exception ex)
        {
            ConsoleUI.PrintError("Audio error: " + ex.Message);
        }
    }
}