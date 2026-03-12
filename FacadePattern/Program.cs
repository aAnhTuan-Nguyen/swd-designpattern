using FacadePattern.Facade;
using FacadePattern.SubSystems;

namespace FacadePattern;

internal class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Initialize subsystems
        var tv = new Television();
        var sound = new SoundSystem();
        var player = new StreamingPlayer();
        var lights = new RoomLights();

        // ╔════════════════════════════════════════════════════╗
        // ║       PART 1: WITHOUT USING FACADE PATTERN         ║
        // ╚════════════════════════════════════════════════════╝
        Console.WriteLine("╔════════════════════════════════════════════════════╗");
        Console.WriteLine("║       WITHOUT USING FACADE PATTERN                 ║");
        Console.WriteLine("╚════════════════════════════════════════════════════╝");
        Console.WriteLine();
        Console.WriteLine("The client must manually call each device:");

        // Start movie — the client must know every detailed step
        Console.WriteLine("\n--- Start watching movie (manual) ---\n");
        lights.Dim(10);
        tv.TurnOn();
        tv.SetInput("HDMI 1");
        tv.SetBrightness(80);
        sound.TurnOn();
        sound.SetSurroundMode();
        sound.SetVolume(60);
        player.TurnOn();
        player.Play("Avengers: Endgame");

        // End movie — the client must also turn off each device manually
        Console.WriteLine("\n--- End watching movie (manual) ---\n");
        player.Stop();
        player.TurnOff();
        sound.TurnOff();
        tv.TurnOff();
        lights.TurnOn();

        Console.WriteLine("\n");

        // ╔════════════════════════════════════════════════════╗
        // ║       PART 2: USING FACADE PATTERN                 ║
        // ╚════════════════════════════════════════════════════╝
        Console.WriteLine("╔════════════════════════════════════════════════════╗");
        Console.WriteLine("║       USING FACADE PATTERN                         ║");
        Console.WriteLine("╚════════════════════════════════════════════════════╝");
        Console.WriteLine();
        Console.WriteLine("The client only needs to call a single method:");

        var homeTheater = new HomeTheaterFacade(tv, sound, player, lights);
        homeTheater.WatchMovie("Avengers: Endgame");
        homeTheater.EndMovie();
    }
}
