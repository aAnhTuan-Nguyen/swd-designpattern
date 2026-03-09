namespace AdapterPattern.Adaptees;

/// <summary>
/// ADAPTEE 1 — Class cũ xử lý email.
/// Có interface KHÁC với INotificationSender: nhận 3 tham số riêng biệt.
/// Không thể thay đổi class này (thư viện bên ngoài / legacy code).
/// </summary>
public class EmailService
{
    public void SendEmail(string toAddress, string subject, string body)
    {
        Console.WriteLine($"📧 [EmailService] Gửi email đến : {toAddress}");
        Console.WriteLine($"                 Tiêu đề       : {subject}");
        Console.WriteLine($"                 Nội dung       : {body}");
    }
}
