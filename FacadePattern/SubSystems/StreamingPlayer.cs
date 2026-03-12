namespace FacadePattern.SubSystems;

public class StreamingPlayer
{
    public void TurnOn() => Console.WriteLine("🎬 Player: Starting the streaming player...");
    public void TurnOff() => Console.WriteLine("🎬 Player: Shutting down the streaming player...");
    public void Play(string movie) => Console.WriteLine($"🎬 Player: Playing movie \"{movie}\"...");
    public void Pause() => Console.WriteLine("🎬 Player: Pausing movie");
    public void Stop() => Console.WriteLine("🎬 Player: Stopping movie");
}
