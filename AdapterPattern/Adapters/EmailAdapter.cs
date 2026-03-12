using AdapterPattern.Adaptees;
using AdapterPattern.Target;

namespace AdapterPattern.Adapters;

/// <summary>
/// ADAPTER 1 — Wraps EmailService into INotificationSender.
/// Translates Send(recipient, message) → SendEmail(toAddress, subject, body).
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
        // Convert: standard interface → EmailService API
        _emailService.SendEmail(
            toAddress: recipient,
            subject: "System Notification",
            body: message
        );
    }
}
