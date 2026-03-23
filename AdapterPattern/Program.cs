using AdapterPattern.Adaptees;
using AdapterPattern.Adapters;
using AdapterPattern.Target;

namespace AdapterPattern;

internal class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("╔════════════════════════════════════════════════════╗");
        Console.WriteLine("║      WITHOUT USING ADAPTER PATTERN                 ║");
        Console.WriteLine("╚════════════════════════════════════════════════════╝");
        Console.WriteLine();
        Console.WriteLine("The client must call each service directly using their specific APIs:\n");

        var emailService = new EmailService();
        var smsService = new SmsService();

        Console.WriteLine("--- Sending order notification #1001 ---\n");

        // Send email — client must know the exact method name and parameters of EmailService
        emailService.SendEmail("khach@gmail.com", "System Notification", "Order #1001 has been confirmed.");
        Console.WriteLine();

        // Send SMS — client must know the exact method name and parameters of SmsService
        smsService.SendTextMessage("0901234567", "Order #1001 has been confirmed.");

        Console.WriteLine();
        Console.WriteLine("⚠️  Problems:");
        Console.WriteLine("    - The client is tightly coupled to EmailService and SmsService.");
        Console.WriteLine("    - Cannot process multiple channels generically in a loop or collection.");
        Console.WriteLine("    - Adding a new channel (e.g. Zalo) requires modifying the client code.");

        Console.WriteLine("\n");

        Console.WriteLine("╔════════════════════════════════════════════════════╗");
        Console.WriteLine("║      USING ADAPTER PATTERN                         ║");
        Console.WriteLine("╚════════════════════════════════════════════════════╝");
        Console.WriteLine();
        Console.WriteLine("The client only needs to know about INotificationSender — it does not care about the concrete service type:\n");

        // Adapters wrap legacy services into the standard interface
        INotificationSender emailSender = new EmailAdapter(new EmailService());
        INotificationSender smsSender   = new SmsAdapter(new SmsService());

        // The client works with a list of interfaces — full polymorphism
        var channels = new List<(INotificationSender sender, string recipient)>
        {
            (emailSender, "khach@gmail.com"),
            (smsSender,   "0901234567"),
            (emailSender, "admin@company.com"),
        };

        Console.WriteLine("--- Sending order notification #1002 ---\n");
        foreach (var (sender, recipient) in channels)
        {
            sender.Send(recipient, "Order #1002 has been confirmed.");
            Console.WriteLine();
        }

        Console.WriteLine("✅ Benefits:");
        Console.WriteLine("   - The client only calls Send() — it doesn't need to know the underlying service type.");
        Console.WriteLine("   - Easily handle multiple channels in a single loop.");
        Console.WriteLine("   - Adding a new channel only requires creating a new Adapter; no client code changes.");
    }
}

