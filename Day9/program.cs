using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// ======================== 1. Interface ========================
public interface INotificationChannel
{
    Task<bool> SendMessageAsync(NotificationMessage message);
}

// ======================== 2. Message Model ========================
public class NotificationMessage
{
    public string To { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public Dictionary<string, object>? Metadata { get; set; }
}

// ======================== 3. Email Sender ========================
public class EmailSender : INotificationChannel
{
    private readonly string _smtpServer;
    private readonly int _port;
    private readonly string _fromEmail;

    public EmailSender(string smtpServer, int port, string fromEmail)
    {
        _smtpServer = smtpServer;
        _port = port;
        _fromEmail = fromEmail;
    }

    public async Task<bool> SendMessageAsync(NotificationMessage message)
    {
        try
        {
            // Simulate actual SMTP send – replace with real SmtpClient logic
            Console.WriteLine($"[EMAIL] To: {message.To} | Subject: {message.Subject} | Body: {message.Body}");
            await Task.Delay(50); // mock async work
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Email failed: {ex.Message}");
            return false;
        }
    }
}

// ======================== 4. SMS Sender ========================
public class SmsSender : INotificationChannel
{
    private readonly string _accountSid;
    private readonly string _authToken;
    private readonly string _fromPhone;

    public SmsSender(string accountSid, string authToken, string fromPhone)
    {
        _accountSid = accountSid;
        _authToken = authToken;
        _fromPhone = fromPhone;
    }

    public async Task<bool> SendMessageAsync(NotificationMessage message)
    {
        try
        {
            Console.WriteLine($"[SMS] To: {message.To} | Body: {message.Body}");
            await Task.Delay(50);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"SMS failed: {ex.Message}");
            return false;
        }
    }
}

// ======================== 5. WhatsApp Sender ========================
public class WhatsAppSender : INotificationChannel
{
    private readonly string _accountSid;
    private readonly string _authToken;
    private readonly string _fromWhatsAppNumber;

    public WhatsAppSender(string accountSid, string authToken, string fromWhatsAppNumber)
    {
        _accountSid = accountSid;
        _authToken = authToken;
        _fromWhatsAppNumber = fromWhatsAppNumber;
    }

    public async Task<bool> SendMessageAsync(NotificationMessage message)
    {
        try
        {
            Console.WriteLine($"[WHATSAPP] To: {message.To} | Body: {message.Body}");
            await Task.Delay(50);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"WhatsApp failed: {ex.Message}");
            return false;
        }
    }
}

// ======================== 6. Dispatcher (optional but useful) ========================
public class NotificationDispatcher
{
    private readonly IEnumerable<INotificationChannel> _channels;

    public NotificationDispatcher(IEnumerable<INotificationChannel> channels)
    {
        _channels = channels;
    }

    public async Task<Dictionary<string, bool>> SendToAllAsync(NotificationMessage message)
    {
        var results = new Dictionary<string, bool>();
        foreach (var channel in _channels)
        {
            var channelName = channel.GetType().Name;
            var success = await channel.SendMessageAsync(message);
            results[channelName] = success;
        }
        return results;
    }

    public async Task<bool> SendToSpecificAsync<TChannel>(NotificationMessage message) where TChannel : INotificationChannel
    {
        var channel = _channels.OfType<TChannel>().FirstOrDefault();
        if (channel == null)
            throw new InvalidOperationException($"Channel {typeof(TChannel).Name} not registered.");
        return await channel.SendMessageAsync(message);
    }
}

// ======================== 7. Demo / Main Program ========================
class Program
{
    static async Task Main(string[] args)
    {
        // 1. Create instances of each channel (mock credentials)
        var emailChannel = new EmailSender("smtp.example.com", 587, "noreply@example.com");
        var smsChannel = new SmsSender("ACxxx", "token", "+1234567890");
        var whatsappChannel = new WhatsAppSender("ACxxx", "token", "+1234567890");

        // 2. Build dispatcher with all channels
        var dispatcher = new NotificationDispatcher(new INotificationChannel[] { emailChannel, smsChannel, whatsappChannel });

        // 3. Create a test message
        var message = new NotificationMessage
        {
            To = "user@example.com",      // In real use, SMS/WhatsApp need phone numbers
            Subject = "Hello from C#",
            Body = "This is a multi-channel notification test."
        };

        // 4. Send via all channels
        Console.WriteLine("=== Sending to all channels ===");
        var allResults = await dispatcher.SendToAllAsync(message);
        foreach (var kvp in allResults)
            Console.WriteLine($"{kvp.Key}: {(kvp.Value ? "Success" : "Failed")}");

        // 5. Send via Email only
        Console.WriteLine("\n=== Sending via Email only ===");
        bool emailResult = await dispatcher.SendToSpecificAsync<EmailSender>(message);
        Console.WriteLine($"Email only result: {emailResult}");

        // 6. Direct single‑channel usage (without dispatcher)
        Console.WriteLine("\n=== Direct SMS send ===");
        var directSms = new SmsSender("dummy", "dummy", "+555");
        await directSms.SendMessageAsync(new NotificationMessage { To = "+123456789", Body = "Direct SMS test" });
    }
}
