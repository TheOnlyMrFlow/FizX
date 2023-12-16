using System;
using FizX.Core.Graphics;
using FizX.Core.Worlds;

namespace FizX.Renderer;

public class ConsoleRenderer : IRenderer
{
    public void Render(IWorld world)
    {
        Console.Clear();
        foreach (var worldActor in world.Actors)
        {
            Console.WriteLine(worldActor.ToString());
        }
    }
}
