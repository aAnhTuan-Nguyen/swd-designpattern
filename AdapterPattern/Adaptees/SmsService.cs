namespace AdapterPattern.Adaptees;

/// <summary>
/// ADAPTEE 2 — Class cũ xử lý SMS.
/// Có interface KHÁC với INotificationSender: tên phương thức và tham số khác hoàn toàn.
/// Không thể thay đổi class này (thư viện bên ngoài / legacy code).
/// </summary>
public class SmsService
{
    public void SendTextMessage(string phoneNumber, string text)
    {
        Console.WriteLine($"📱 [SmsService]   Gửi SMS đến   : {phoneNumber}");
        Console.WriteLine($"                 Tin nhắn       : {text}");
    }
}
