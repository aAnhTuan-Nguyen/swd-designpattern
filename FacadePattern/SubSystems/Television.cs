namespace FacadePattern.SubSystems;

public class Television
{
    public void TurnOn() => Console.WriteLine("📺 TV: Turning on...");
    public void TurnOff() => Console.WriteLine("📺 TV: Turning off...");
    public void SetInput(string source) => Console.WriteLine($"📺 TV: Switching input to [{source}]");
    public void SetBrightness(int level) => Console.WriteLine($"📺 TV: Brightness set to {level}%");
}
