namespace FacadePattern.SubSystems;

public class StreamingPlayer
{
    public void TurnOn() => Console.WriteLine("🎬 Player: Đang khởi động trình phát...");
    public void TurnOff() => Console.WriteLine("🎬 Player: Đang tắt trình phát...");
    public void Play(string movie) => Console.WriteLine($"🎬 Player: Đang phát phim \"{movie}\"...");
    public void Pause() => Console.WriteLine("🎬 Player: Tạm dừng phim");
    public void Stop() => Console.WriteLine("🎬 Player: Dừng phát phim");
}
