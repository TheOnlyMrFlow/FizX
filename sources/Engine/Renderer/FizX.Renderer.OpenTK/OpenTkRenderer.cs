using FizX.Core.Graphics;
using FizX.Core.Worlds;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace FizX.Renderer.OpenTK;

public class OpenTkRenderer : IRenderer
{
    private readonly ConsoleRenderer _consoleRenderer = new();
    private FizXGameWindow? _window;

    public OpenTkRenderer Init()
    {
        var nativeWindowSettings = new NativeWindowSettings()
        {
            //Size = new Vector2i(800, 600),
            WindowState = WindowState.Maximized,
            WindowBorder = WindowBorder.Hidden,
            Title = "LearnOpenTK - Creating a Window",
            // This is needed to run on macos
            Flags = ContextFlags.ForwardCompatible,
        };
        
        _window = new FizXGameWindow(GameWindowSettings.Default, nativeWindowSettings);
        _window.Run();

        return this;
    }
    
    public void Render(IWorld world)
    {
        _consoleRenderer.Render(world);
        
        if (_window is null || !_window.Exists)
        {
            return;
        }
        
        _window.Title = world.Actors.ElementAt(1).Position.X.ToString();

        _window.UpdateFrequency = 120;
    }
}