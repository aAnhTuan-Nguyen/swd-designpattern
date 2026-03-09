using FacadePattern.Facade;
using FacadePattern.SubSystems;

namespace FacadePattern;

internal class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Khởi tạo các hệ thống con (subsystems)
        var tv = new Television();
        var sound = new SoundSystem();
        var player = new StreamingPlayer();
        var lights = new RoomLights();

        // ╔════════════════════════════════════════════════════╗
        // ║       PHẦN 1: KHÔNG SỬ DỤNG FACADE PATTERN       ║
        // ╚════════════════════════════════════════════════════╝
        Console.WriteLine("╔════════════════════════════════════════════════════╗");
        Console.WriteLine("║       KHÔNG SỬ DỤNG FACADE PATTERN               ║");
        Console.WriteLine("╚════════════════════════════════════════════════════╝");
        Console.WriteLine();
        Console.WriteLine("Client phải tự gọi từng thiết bị một cách thủ công:");

        // Bật phim — client phải biết từng bước chi tiết
        Console.WriteLine("\n--- Bắt đầu xem phim (thủ công) ---\n");
        lights.Dim(10);
        tv.TurnOn();
        tv.SetInput("HDMI 1");
        tv.SetBrightness(80);
        sound.TurnOn();
        sound.SetSurroundMode();
        sound.SetVolume(60);
        player.TurnOn();
        player.Play("Avengers: Endgame");

        // Tắt phim — client cũng phải tự tắt từng cái
        Console.WriteLine("\n--- Kết thúc xem phim (thủ công) ---\n");
        player.Stop();
        player.TurnOff();
        sound.TurnOff();
        tv.TurnOff();
        lights.TurnOn();

        Console.WriteLine("\n");

        // ╔════════════════════════════════════════════════════╗
        // ║       PHẦN 2: SỬ DỤNG FACADE PATTERN             ║
        // ╚════════════════════════════════════════════════════╝
        Console.WriteLine("╔════════════════════════════════════════════════════╗");
        Console.WriteLine("║       SỬ DỤNG FACADE PATTERN                     ║");
        Console.WriteLine("╚════════════════════════════════════════════════════╝");
        Console.WriteLine();
        Console.WriteLine("Client chỉ cần gọi 1 phương thức duy nhất:");

        var homeTheater = new HomeTheaterFacade(tv, sound, player, lights);
        homeTheater.WatchMovie("Avengers: Endgame");
        homeTheater.EndMovie();
    }
}
