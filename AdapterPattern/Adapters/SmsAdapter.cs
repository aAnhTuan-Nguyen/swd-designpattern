using AdapterPattern.Adaptees;
using AdapterPattern.Target;

namespace AdapterPattern.Adapters;

/// <summary>
/// ADAPTER 2 — Bọc SmsService vào INotificationSender.
/// Dịch lệnh Send(recipient, message) → SendTextMessage(phoneNumber, text).
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
        // Chuyển đổi: interface chuẩn → API của SmsService
        _smsService.SendTextMessage(
            phoneNumber: recipient,
            text: message
        );
    }
}
