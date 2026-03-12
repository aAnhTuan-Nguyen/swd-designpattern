using AdapterPattern.Adaptees;
using AdapterPattern.Target;

namespace AdapterPattern.Adapters;

/// <summary>
/// ADAPTER 2 — Wraps SmsService into INotificationSender.
/// Translates Send(recipient, message) → SendTextMessage(phoneNumber, text).
/// </summary>
public class SmsAdapter : INotificationSender
{
    private readonly SmsService _smsService;

    public SmsAdapter(SmsService smsService)
    {
        _smsService = smsService;
    }

    public void Send(string recipient, string message)
    {
        // Convert: standard interface → SmsService API
        _smsService.SendTextMessage(
            phoneNumber: recipient,
            text: message
        );
    }
}
