using OpenAI.Chat;
using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

public static class ResponseHandler
{
    private static readonly string apiKey =
        Environment.GetEnvironmentVariable("OPENAI_API_KEY") ?? string.Empty;

    public static async Task<string> GetResponse(string input, string name)
    {
        input = input.ToLower();

        // 👋 Greeting
        if (input.Contains("hello") || input.Contains("hi"))
            return $"Hey {name}! 👋 I'm here to help you stay safe online. What would you like to learn today?";

        // 😊 Small talk
        if (input.Contains("how are you"))
            return $"I'm doing great, {name}! Ready to help you stay safe online 🔐. What’s on your mind?";

        // 🎯 Purpose
        if (input.Contains("purpose") || input.Contains("what do you do"))
            return "My goal is to help you understand how to stay safe online—things like avoiding scams, creating strong passwords, and protecting your personal information. What topic interests you most?";

        // 🔑 Passwords
        if (input.Contains("password"))
            return "Great question! 🔑 A strong password should be long, unique, and hard to guess. Try using a mix of uppercase, lowercase, numbers, and symbols—or even a passphrase like 'BlueSky!Tiger92'.\n\nAlso, avoid reusing passwords across sites. Would you like tips on password managers?";

        // 🎣 Phishing
        if (input.Contains("phishing") || input.Contains("scam"))
            return "Phishing is one of the most common online threats 🎣. Attackers try to trick you into giving personal info through fake emails or messages.\n\nAlways check:\n- The sender’s email address\n- Suspicious links\n- Urgent or threatening language\n\nIf something feels off, it probably is. Want me to show you a real example?";

        // 🌐 Safe browsing
        if (input.Contains("browsing") || input.Contains("website"))
            return "When browsing the internet 🌐, always look for HTTPS 🔒 in the URL—it means the connection is secure.\n\nAlso avoid clicking unknown links, especially from emails or pop-ups. If a site looks suspicious, it's safer to leave it. Do you want tips on spotting fake websites?";

        // 🦠 Malware
        if (input.Contains("malware") || input.Contains("virus"))
            return "Malware is harmful software designed to damage or steal your data 🦠.\n\nYou can avoid it by:\n- Not downloading files from unknown sources\n- Keeping your software updated\n- Using antivirus protection\n\nHave you ever seen a suspicious download before?";

        // 🔐 Privacy
        if (input.Contains("privacy") || input.Contains("data"))
            return "Protecting your personal data is very important 🔐. Try to limit what you share online and review app permissions regularly.\n\nIf a free app asks for too much access, that’s a red flag. Want help checking what apps should or shouldn't access?";

        // 📱 Social media safety
        if (input.Contains("social media"))
            return "On social media 📱, be careful about what you share publicly. Personal details like your location, school, or routine can be misused.\n\nAlso watch out for fake profiles and suspicious messages. Do you usually keep your accounts private?";

        // ❓ Help menu
        if (input.Contains("what can i ask") || input.Contains("help"))
            return "You can ask me about:\n\n- Password safety 🔑\n- Phishing and scams 🎣\n- Safe browsing 🌐\n- Malware 🦠\n- Privacy 🔐\n\nOr even ask general cybersecurity questions. What would you like to explore?";

        // 🙋 Unknown → AI fallback
        return await GetAIResponse(input, name);
    }

    // 🤖 AI FALLBACK
    private static async Task<string> GetAIResponse(string input, string name)
    {
        try
        {
            var client = new ChatClient(
                model: "gpt-4.1-mini",
                apiKey: apiKey
            );

            var result = await client.CompleteChatAsync(
                ChatMessage.CreateSystemMessage(
                    "You are a friendly cybersecurity assistant. Keep answers beginner-friendly, practical, and focused on online safety. Ask follow-up questions when helpful."
                ),
                ChatMessage.CreateUserMessage($"{name} says: {input}")
            );

            var content = result.Value.Content;

            var text = string.Concat(
                content
                    .OfType<ChatMessageContentPart>()
                    .Where(part => part.Kind == ChatMessageContentPartKind.Text && part.Text != null)
                    .Select(part => part.Text)
            );

            return text;
        }
        catch (Exception)
        {
            return "Hmm… something went wrong while I was thinking 🤔. Please try again in a moment.";
        }
    }
}