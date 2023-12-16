using FizX.Core;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace FizX.OpenTK;

public class OpenTkGameHost : IGameHost
{
    private GameWindow _window;
    
    public OpenTkRenderer Renderer { get; protected set; }

    public OpenTkGameHost()
    {
        var nativeWindowSettings = new NativeWindowSettings()
        {
            ClientSize = new Vector2i(800, 600),
            WindowState = WindowState.Normal,
            WindowBorder = WindowBorder.Fixed,
            Title = "LearnOpenTK - Creating a Window",
            // This is needed to run on macos
            Flags = ContextFlags.ForwardCompatible,
        };
        
        _window = new GameWindow(GameWindowSettings.Default, nativeWindowSettings);
        _window.UpdateFrequency = 120;

        Renderer = new OpenTkRenderer(_window);
    }

    public void HostGame(Game game, CancellationToken cancellationToken)
    {
        _window.Load += Renderer.Load;

        _window.Resize += Renderer.OnResize;
        
        _window.UpdateFrame += e =>
        {
            Renderer.OnUpdateFrame(e);
            
            game.Tick((int) (e.Time * 1000f));
        };

        _window.RenderFrame += e =>
        {
            game.Render();
        };

        _window.Unload += Renderer.OnUnload;
            
        _window.Run();
    }
}