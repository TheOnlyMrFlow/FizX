using System;
using System.Diagnostics;
using FizX.Core.Graphics;
using FizX.Core.Worlds;

namespace FizX.Renderer;

public class ConsoleRenderingEngine : IRenderingEngine
{
    private readonly Stopwatch _stopwatch = new Stopwatch();
    private TimeSpan _lastEllapsed = TimeSpan.Zero;
    
    public void RenderWorld(World world)
    {
        if (!_stopwatch.IsRunning)
        {
            _stopwatch.Start();
        }
        
        Console.Clear();
        var elapsed = _stopwatch.Elapsed;
        var delta = elapsed - _lastEllapsed;
        _lastEllapsed = elapsed;
        Console.WriteLine($"FPS: {(int) (1000000 / delta.TotalMicroseconds)}");
        foreach (var worldActor in world.Actors)
        {
            Console.WriteLine(worldActor.ToString());
        }
    }
}
