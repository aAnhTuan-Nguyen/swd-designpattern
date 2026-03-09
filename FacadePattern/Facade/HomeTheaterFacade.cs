using FacadePattern.SubSystems;

namespace FacadePattern.Facade;

public class HomeTheaterFacade
{
    private readonly Television _tv;
    private readonly SoundSystem _sound;
    private readonly StreamingPlayer _player;
    private readonly RoomLights _lights;

    public HomeTheaterFacade(
        Television tv,
        SoundSystem sound,
        StreamingPlayer player,
        RoomLights lights)
    {
        _tv = tv;
        _sound = sound;
        _player = player;
        _lights = lights;
    }

    /// <summary>
    /// Một lệnh duy nhất để bắt đầu xem phim.
    /// Bên trong, Facade điều phối tất cả các hệ thống con.
    /// </summary>
    public void WatchMovie(string movie)
    {
        Console.WriteLine($"\n🎥 === BẮT ĐẦU XEM PHIM: \"{movie}\" ===\n");

        _lights.Dim(10);
        _tv.TurnOn();
        _tv.SetInput("HDMI 1");
        _tv.SetBrightness(80);
        _sound.TurnOn();
        _sound.SetSurroundMode();
        _sound.SetVolume(60);
        _player.TurnOn();
        _player.Play(movie);

        Console.WriteLine("\n🍿 Chúc bạn xem phim vui vẻ!\n");
    }

    /// <summary>
    /// Một lệnh duy nhất để kết thúc xem phim.
    /// </summary>
    public void EndMovie()
    {
        Console.WriteLine("\n🎥 === KẾT THÚC XEM PHIM ===\n");

        _player.Stop();
        _player.TurnOff();
        _sound.TurnOff();
        _tv.TurnOff();
        _lights.TurnOn();

        Console.WriteLine("\n✅ Tất cả thiết bị đã được tắt. Đèn phòng đã bật lại.\n");
    }
}
