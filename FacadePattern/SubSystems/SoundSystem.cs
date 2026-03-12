namespace FacadePattern.SubSystems;

public class SoundSystem
{
    public void TurnOn() => Console.WriteLine("🔊 Sound: Turning on the sound system...");
    public void TurnOff() => Console.WriteLine("🔊 Sound: Turning off the sound system...");
    public void SetVolume(int level) => Console.WriteLine($"🔊 Sound: Volume set to {level}");
    public void SetSurroundMode() => Console.WriteLine("🔊 Sound: Surround 7.1 mode enabled");
    public void SetStereoMode() => Console.WriteLine("🔊 Sound: Stereo mode enabled");
}
