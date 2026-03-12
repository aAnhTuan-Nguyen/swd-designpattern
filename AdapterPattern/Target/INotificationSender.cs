namespace AdapterPattern.Target;

/// <summary>
/// TARGET INTERFACE — The standard interface the client uses.
/// The client only needs to know this interface and not how sending is implemented.
/// </summary>
public interface INotificationSender
{
    void Send(string recipient, string message);
}
