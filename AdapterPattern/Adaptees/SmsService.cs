namespace AdapterPattern.Adaptees;

/// <summary>
/// ADAPTEE 2 — Legacy class that handles SMS.
/// It has a different interface than INotificationSender: different method name and parameters.
/// Assume this class cannot be changed (external library / legacy code).
/// </summary>
public class SmsService
{
    public void SendTextMessage(string phoneNumber, string text)
    {
        Console.WriteLine($"📱 [SmsService]   Sending SMS to : {phoneNumber}");
        Console.WriteLine($"                 Message         : {text}");
    }
}
