using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using FizX.Core.Events;
using FizX.Core.Exceptions;

namespace FizX.Core.Input;

public interface IInputManager
{
    Vector2 GetInputVector(string name);
    void MapInputVector(string name, KeyboardInputKey key, InputAxis axis, float magnitude = 1f);

    void BeforeGameStarts();
    void OnTickEnded();
}

public class InputManager : IInputManager
{
    private readonly Dictionary<string, InputVectorLazy> _inputVectors = new();

    private InputState _inputState = new([]);

    private readonly InputStateBuilder _inputStateBuilder = new();
    
    public void BeforeGameStarts()
    {
        var eventBus = Game.Instance.EventBus;
        eventBus.Subscribe<KeyboardKeyDownEvent>(e => _inputStateBuilder.KeyDown(e.Key));
        eventBus.Subscribe<KeyboardKeyUpEvent>(e => _inputStateBuilder.KeyUp(e.Key));
    }
    
    public void OnTickEnded()
    {
        _inputState = _inputStateBuilder.Build();
    }
    
    public Vector2 GetInputVector(string name)
    {
        return _inputVectors.TryGetValue(name, out var vec) ? vec.GetValue(_inputState) : throw new FizXRuntimeException("Input name does not exist.");
    }
    
    public void MapInputVector(string name, IEnumerable<InputVectorComponent> components)
    {
        if (!_inputVectors.TryGetValue(name, out var vector))
        {
            vector = new InputVectorLazy();
            _inputVectors.Add(name, vector);
        }
        
        foreach (var component in components)
        {
            vector.AddComponent(component);
            
        }
    }
    
    public void MapInputVector(string name, InputVectorComponent component)
    {
        MapInputVector(name, [component]);
    }
    
    public void MapInputVector(string name, KeyboardInputKey key, InputAxis axis, float magnitude = 1f)
    {
        MapInputVector(name, new InputVectorComponent(key, axis, magnitude));
    }
}

public abstract class KeyboardEvent : Event
{
    public KeyboardInputKey Key { get; set; }
    
    public KeyboardEvent(KeyboardInputKey key)
    {
        Key = key;
    }
}

public class KeyboardKeyDownEvent : KeyboardEvent
{
    public KeyboardKeyDownEvent(KeyboardInputKey key) : base(key)
    {
    }
}

public class KeyboardKeyUpEvent : KeyboardEvent
{
    public KeyboardKeyUpEvent(KeyboardInputKey key) : base(key)
    {
    }
}

public enum InputAxis
{
    X,
    Y
}

public class InputVectorLazy
{
    private readonly List<InputVectorComponent> _components = new();
    
    public void AddComponent(KeyboardInputKey key, InputAxis axis, float magnitude)
    {
        _components.Add(new InputVectorComponent(key, axis, magnitude));
    }

    public void AddComponent(InputVectorComponent component)
    {
        _components.Add(component);
    }
    
    public Vector2 GetValue(InputState inputState)
    {
        return _components
            .Where(c => inputState.IsPressed(c.Key))
            .Aggregate(
                Vector2.Zero, 
                (current, component) => current + component.Magnitude * (component.Axis == InputAxis.X ? Vector2.UnitX : Vector2.UnitY)
                );
    }
}

public class InputStateBuilder()
{
    private readonly HashSet<KeyboardInputKey> _pressedKeys = [];

    public void KeyUp(KeyboardInputKey key)
    {
        _pressedKeys.Remove(key);
    }
    
    public void KeyDown(KeyboardInputKey key)
    {
        _pressedKeys.Add(key);
    }

    public InputState Build() => new(_pressedKeys);
}

public class InputState
{
    private readonly HashSet<KeyboardInputKey> _pressedKeys = new ();

    public InputState(HashSet<KeyboardInputKey> pressedKeys)
    {
        _pressedKeys = pressedKeys;
    }
    
    public bool IsPressed(KeyboardInputKey key) => _pressedKeys.Contains(key);
}

public class InputVectorComponent
{
    public KeyboardInputKey Key { get; }
    public InputAxis Axis { get; }
    public float Magnitude { get; }
    
    public InputVectorComponent(KeyboardInputKey key, InputAxis axis, float magnitude)
    {
        Key = key;
        Axis = axis;
        Magnitude = magnitude;
    }
}

public enum KeyboardInputKey
{
    Unknown,
    Space,
    Apostrophe,
    Comma,
    Minus,
    Period,
    Slash,
    D0,
    D1,
    D2,
    D3,
    D4,
    D5,
    D6, 
    D7, 
    D8, 
    D9, 
    Semicolon, 
    Equal, 
    A, 
    B, 
    C, 
    D, 
    E, 
    F, 
    G,
    H,
    I,
    J,
    K,
    L,
    M,
    N,
    O,
    P,
    Q,
    R,
    S,
    T,
    U,
    V,
    W,
    X,
    Y,
    Z,
    LeftBracket,
    Backslash,
    RightBracket,
    GraveAccent,
    Escape,
    Enter,
    Tab,
    Backspace,
    Insert,
    Delete,
    Right,
    Left,
    Down,
    Up,
    PageUp,
    PageDown,
    Home,
    End,
    CapsLock,
    ScrollLock,
    NumLock,
    PrintScreen,
    Pause,
    F1,
    F2,
    F3,
    F4,
    F5,
    F6,
    F7,
    F8,
    F9,
    F10,
    F11,
    F12,
    F13,
    F14,
    F15,
    F16,
    F17,
    F18,
    F19,
    F20,
    F21,
    F22,
    F23,
    F24,
    F25,
    KeyPad0,
    KeyPad1,
    KeyPad2,
    KeyPad3,
    KeyPad4,
    KeyPad5,
    KeyPad6,
    KeyPad7,
    KeyPad8,
    KeyPad9,
    KeyPadDecimal,
    KeyPadDivide,
    KeyPadMultiply,
    KeyPadSubtract,
    KeyPadAdd,
    KeyPadEnter,
    KeyPadEqual,
    LeftShift,
    LeftControl,
    LeftAlt,
    LeftSuper,
    RightShift,
    RightControl,
    RightAlt,
    RightSuper,
    LastKey,
    Menu,
}