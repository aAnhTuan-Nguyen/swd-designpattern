namespace FacadePattern.SubSystems;

public class RoomLights
{
    public void TurnOn() => Console.WriteLine("💡 Đèn phòng: Bật đèn");
    public void TurnOff() => Console.WriteLine("💡 Đèn phòng: Tắt đèn");
    public void Dim(int level) => Console.WriteLine($"💡 Đèn phòng: Giảm ánh sáng xuống {level}%");
}
