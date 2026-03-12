namespace AdapterPattern.Adaptees;

/// <summary>
/// ADAPTEE 1 — Legacy class that handles email.
/// It has a different interface than INotificationSender: accepts 3 separate parameters.
/// Assume this class cannot be changed (external library / legacy code).
/// </summary>
public class EmailService
{
    public void SendEmail(string toAddress, string subject, string body)
    {
        Console.WriteLine($"📧 [EmailService] Sending email to : {toAddress}");
        Console.WriteLine($"                 Subject        : {subject}");
        Console.WriteLine($"                 Body           : {body}");
    }
}
