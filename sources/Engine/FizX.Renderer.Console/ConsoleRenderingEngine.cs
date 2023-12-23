using System;
using FizX.Core.Graphics;
using FizX.Core.Worlds;

namespace FizX.Renderer;

public class ConsoleRenderingEngine : IRenderingEngine
{
    public void RenderWorld(World world)
    {
        Console.Clear();
        foreach (var worldActor in world.Actors)
        {
            Console.WriteLine(worldActor.ToString());
        }
    }
}
