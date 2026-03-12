namespace FacadePattern.SubSystems;

public class RoomLights
{
    public void TurnOn() => Console.WriteLine("💡 Room Lights: Turning lights on");
    public void TurnOff() => Console.WriteLine("💡 Room Lights: Turning lights off");
    public void Dim(int level) => Console.WriteLine($"💡 Room Lights: Dimming lights to {level}%");
}
