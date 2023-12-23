using FizX.Core;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace FizX.OpenTK;

public class OpenTkGameHost : IGameHost
{
    private GameWindow _window;
    
    public OpenTkRenderingEngine RenderingEngine { get; protected set; }

    public OpenTkGameHost()
    {
        var nativeWindowSettings = new NativeWindowSettings()
        {
            ClientSize = new Vector2i(960, 540),
            WindowState = WindowState.Normal,
            WindowBorder = WindowBorder.Fixed,
            Title = "LearnOpenTK - Creating a Window",
            // This is needed to run on macos
            Flags = ContextFlags.ForwardCompatible,
        };
        
        _window = new GameWindow(GameWindowSettings.Default, nativeWindowSettings);
        _window.UpdateFrequency = 120;

        RenderingEngine = new OpenTkRenderingEngine(_window);
    }

    public void HostGame(Game game, CancellationToken cancellationToken)
    {
        _window.Load += RenderingEngine.Load;

        _window.Resize += RenderingEngine.OnResize;
        
        _window.UpdateFrame += e =>
        {
            RenderingEngine.OnUpdateFrame(e);
            
            game.Tick((int) (e.Time * 1000f));
        };

        _window.RenderFrame += e =>
        {
            game.Render();
        };

        _window.Unload += RenderingEngine.OnUnload;
            
        _window.Run();
    }
}