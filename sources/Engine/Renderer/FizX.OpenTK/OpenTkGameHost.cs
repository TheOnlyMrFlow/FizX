using System.Diagnostics;
using FizX.Core;
using FizX.Core.Input;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FizX.OpenTK;

public class OpenTkGameHost : IGameHost
{
    private GameWindow _window;
    private ulong _frameIndex = 0;
    private Stopwatch _stopWatch = new ();
    
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
        _window.Load += () =>
        {
            _stopWatch.Restart();
            RenderingEngine.Load();
            game.BeforeStart();
        };

        _window.Resize += RenderingEngine.OnResize;
        
        _window.UpdateFrame += e =>
        {
            Console.WriteLine(e.Time);
            RenderingEngine.OnUpdateFrame(e);

            var frameInfo = new FrameInfo
            {
                Index = _frameIndex,
                ElapsedMs = _stopWatch.ElapsedMilliseconds,
                DeltaTimeMs = (float) e.Time * 1000f,
            };
            game.Tick(frameInfo);

            _frameIndex++;
        };

        _window.RenderFrame += e =>
        {
            game.Render();
        };

        _window.Unload += RenderingEngine.OnUnload;

        _window.KeyDown += e => Game.Instance.EventBus.BroadcastEvent(new KeyboardKeyDownEvent(MapKey(e.Key)));
        _window.KeyUp += e => Game.Instance.EventBus.BroadcastEvent(new KeyboardKeyUpEvent(MapKey(e.Key)));
            
        _window.Run();
    }

    private KeyboardInputKey MapKey(Keys key)
    {
        return key switch
        {
            Keys.Unknown => KeyboardInputKey.Unknown,
            Keys.Space => KeyboardInputKey.Space,
            Keys.Apostrophe => KeyboardInputKey.Apostrophe,
            Keys.Comma => KeyboardInputKey.Comma,
            Keys.Minus => KeyboardInputKey.Minus,
            Keys.Period => KeyboardInputKey.Period,
            Keys.Slash => KeyboardInputKey.Slash,
            Keys.D0 => KeyboardInputKey.D0,
            Keys.D1 => KeyboardInputKey.D1,
            Keys.D2 => KeyboardInputKey.D2,
            Keys.D3 => KeyboardInputKey.D3,
            Keys.D4 => KeyboardInputKey.D4,
            Keys.D5 => KeyboardInputKey.D5,
            Keys.D6 => KeyboardInputKey.D6,
            Keys.D7 => KeyboardInputKey.D7,
            Keys.D8 => KeyboardInputKey.D8,
            Keys.D9 => KeyboardInputKey.D9,
            Keys.Semicolon => KeyboardInputKey.Semicolon,
            Keys.Equal => KeyboardInputKey.Equal,
            Keys.A => KeyboardInputKey.A,
            Keys.B => KeyboardInputKey.B,
            Keys.C => KeyboardInputKey.C,
            Keys.D => KeyboardInputKey.D,
            Keys.E => KeyboardInputKey.E,
            Keys.F => KeyboardInputKey.F,
            Keys.G => KeyboardInputKey.G,
            Keys.H => KeyboardInputKey.H,
            Keys.I => KeyboardInputKey.I,
            Keys.J => KeyboardInputKey.J,
            Keys.K => KeyboardInputKey.K,
            Keys.L => KeyboardInputKey.L,
            Keys.M => KeyboardInputKey.M,
            Keys.N => KeyboardInputKey.N,
            Keys.O => KeyboardInputKey.O,
            Keys.P => KeyboardInputKey.P,
            Keys.Q => KeyboardInputKey.Q,
            Keys.R => KeyboardInputKey.R,
            Keys.S => KeyboardInputKey.S,
            Keys.T => KeyboardInputKey.T,
            Keys.U => KeyboardInputKey.U,
            Keys.V => KeyboardInputKey.V,
            Keys.W => KeyboardInputKey.W,
            Keys.X => KeyboardInputKey.X,
            Keys.Y => KeyboardInputKey.Y,
            Keys.Z => KeyboardInputKey.Z,
            Keys.LeftBracket => KeyboardInputKey.LeftBracket,
            Keys.Backslash => KeyboardInputKey.Backslash,
            Keys.RightBracket => KeyboardInputKey.RightBracket,
            Keys.GraveAccent => KeyboardInputKey.GraveAccent,
            Keys.Escape => KeyboardInputKey.Escape,
            Keys.Enter => KeyboardInputKey.Enter,
            Keys.Tab => KeyboardInputKey.Tab,
            Keys.Backspace => KeyboardInputKey.Backspace,
            Keys.Insert => KeyboardInputKey.Insert,
            Keys.Delete => KeyboardInputKey.Delete,
            Keys.Right => KeyboardInputKey.Right,
            Keys.Left => KeyboardInputKey.Left,
            Keys.Down => KeyboardInputKey.Down,
            Keys.Up => KeyboardInputKey.Up,
            Keys.PageUp => KeyboardInputKey.PageUp,
            Keys.PageDown => KeyboardInputKey.PageDown,
            Keys.Home => KeyboardInputKey.Home,
            Keys.End => KeyboardInputKey.End,
            Keys.CapsLock => KeyboardInputKey.CapsLock,
            Keys.ScrollLock => KeyboardInputKey.ScrollLock,
            Keys.NumLock => KeyboardInputKey.NumLock,
            Keys.PrintScreen => KeyboardInputKey.PrintScreen,
            Keys.Pause => KeyboardInputKey.Pause,
            Keys.F1 => KeyboardInputKey.F1,
            Keys.F2 => KeyboardInputKey.F2,
            Keys.F3 => KeyboardInputKey.F3,
            Keys.F4 => KeyboardInputKey.F4,
            Keys.F5 => KeyboardInputKey.F5,
            Keys.F6 => KeyboardInputKey.F6,
            Keys.F7 => KeyboardInputKey.F7,
            Keys.F8 => KeyboardInputKey.F8,
            Keys.F9 => KeyboardInputKey.F9,
            Keys.F10 => KeyboardInputKey.F10,
            Keys.F11 => KeyboardInputKey.F11,
            Keys.F12 => KeyboardInputKey.F12,
            Keys.F13 => KeyboardInputKey.F13,
            Keys.F14 => KeyboardInputKey.F14,
            Keys.F15 => KeyboardInputKey.F15,
            Keys.F16 => KeyboardInputKey.F16,
            Keys.F17 => KeyboardInputKey.F17,
            Keys.F18 => KeyboardInputKey.F18,
            Keys.F19 => KeyboardInputKey.F19,
            Keys.F20 => KeyboardInputKey.F20,
            Keys.F21 => KeyboardInputKey.F21,
            Keys.F22 => KeyboardInputKey.F22,
            Keys.F23 => KeyboardInputKey.F23,
            Keys.F24 => KeyboardInputKey.F24,
            Keys.F25 => KeyboardInputKey.F25,
            Keys.KeyPad0 => KeyboardInputKey.KeyPad0,
            Keys.KeyPad1 => KeyboardInputKey.KeyPad1,
            Keys.KeyPad2 => KeyboardInputKey.KeyPad2,
            Keys.KeyPad3 => KeyboardInputKey.KeyPad3,
            Keys.KeyPad4 => KeyboardInputKey.KeyPad4,
            Keys.KeyPad5 => KeyboardInputKey.KeyPad5,
            Keys.KeyPad6 => KeyboardInputKey.KeyPad6,
            Keys.KeyPad7 => KeyboardInputKey.KeyPad7,
            Keys.KeyPad8 => KeyboardInputKey.KeyPad8,
            Keys.KeyPad9 => KeyboardInputKey.KeyPad9,
            Keys.KeyPadDecimal => KeyboardInputKey.KeyPadDecimal,
            Keys.KeyPadDivide => KeyboardInputKey.KeyPadDivide,
            Keys.KeyPadMultiply => KeyboardInputKey.KeyPadMultiply,
            Keys.KeyPadSubtract => KeyboardInputKey.KeyPadSubtract,
            Keys.KeyPadAdd => KeyboardInputKey.KeyPadAdd,
            Keys.KeyPadEnter => KeyboardInputKey.KeyPadEnter,
            Keys.KeyPadEqual => KeyboardInputKey.KeyPadEqual,
            Keys.LeftShift => KeyboardInputKey.LeftShift,
            Keys.LeftControl => KeyboardInputKey.LeftControl,
            Keys.LeftAlt => KeyboardInputKey.LeftAlt,
            Keys.LeftSuper => KeyboardInputKey.LeftSuper,
            Keys.RightShift => KeyboardInputKey.RightShift,
            Keys.RightControl => KeyboardInputKey.RightControl,
            Keys.RightAlt => KeyboardInputKey.RightAlt,
            Keys.RightSuper => KeyboardInputKey.RightSuper,
            Keys.Menu => KeyboardInputKey.Menu,
            _ => throw new ArgumentOutOfRangeException(nameof(key), key, null)
        };
    }
}