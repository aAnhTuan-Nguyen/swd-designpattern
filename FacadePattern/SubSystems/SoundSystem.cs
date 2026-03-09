namespace FacadePattern.SubSystems;

public class SoundSystem
{
    public void TurnOn() => Console.WriteLine("🔊 Âm thanh: Đang bật hệ thống âm thanh...");
    public void TurnOff() => Console.WriteLine("🔊 Âm thanh: Đang tắt hệ thống âm thanh...");
    public void SetVolume(int level) => Console.WriteLine($"🔊 Âm thanh: Âm lượng đặt ở mức {level}");
    public void SetSurroundMode() => Console.WriteLine("🔊 Âm thanh: Chế độ Surround 7.1 đã bật");
    public void SetStereoMode() => Console.WriteLine("🔊 Âm thanh: Chế độ Stereo đã bật");
}
