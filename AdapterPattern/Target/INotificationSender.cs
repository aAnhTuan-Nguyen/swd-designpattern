namespace AdapterPattern.Target;

/// <summary>
/// TARGET INTERFACE — Giao diện chuẩn mà client sử dụng.
/// Client chỉ cần biết interface này, không cần biết cách gửi thực tế.
/// </summary>
public interface INotificationSender
{
    void Send(string recipient, string message);
}
