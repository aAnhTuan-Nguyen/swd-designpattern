namespace FacadePattern.SubSystems;

public class Television
{
    public void TurnOn() => Console.WriteLine("📺 TV: Đang bật TV...");
    public void TurnOff() => Console.WriteLine("📺 TV: Đang tắt TV...");
    public void SetInput(string source) => Console.WriteLine($"📺 TV: Chuyển sang nguồn [{source}]");
    public void SetBrightness(int level) => Console.WriteLine($"📺 TV: Độ sáng đặt ở mức {level}%");
}
