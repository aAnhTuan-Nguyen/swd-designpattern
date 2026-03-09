using AdapterPattern.Adaptees;
using AdapterPattern.Target;

namespace AdapterPattern.Adapters;

/// <summary>
/// ADAPTER 1 — Bọc EmailService vào INotificationSender.
/// Dịch lệnh Send(recipient, message) → SendEmail(toAddress, subject, body).
/// </summary>
public class EmailAdapter : INotificationSender
{
    private readonly EmailService _emailService;

    public EmailAdapter(EmailService emailService)
    {
        _emailService = emailService;
    }

    public void Send(string recipient, string message)
    {
        // Chuyển đổi: interface chuẩn → API của EmailService
        _emailService.SendEmail(
            toAddress: recipient,
            subject: "Thông báo hệ thống",
            body: message
        );
    }
}
