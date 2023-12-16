using FizX.Core.ContentLoading;
using FizX.Core.Events;
using FizX.Core.Input;
using FizX.Core.Logging;
using FizX.Core.Physics;
using FizX.Core.Graphics;
using FizX.Core.Worlds;

namespace FizX.Core;

public class Game
{
    private readonly IInputManager _inputManager;
    private readonly ILogger _logger;
    private readonly IPhysicsEngine _physicsEngine;
    private readonly IRenderer _renderer;
    private readonly IWorldLoader _worldLoader;
    private readonly IEventBus _eventBus;

    public World World { get; private set; }

    public int ElapsedFramesSinceStart { get; private set; } = 0;
    public int ElapsedTicksSinceStart { get; private set; } = 0;

    public Game(GameBoundaries boundaries)
    {
        _inputManager = boundaries.InputManager;
        _logger = boundaries.Logger;
        _physicsEngine = boundaries.PhysicsEngine;
        _renderer = boundaries.Renderer;
        _worldLoader = boundaries.WorldLoader;
        _eventBus = boundaries.EventBus;

        World = _worldLoader.LoadWorld();
    }


    public void Tick(int deltaMs)
    {
        World.Tick(deltaMs);

        _physicsEngine.Tick(deltaMs);
            
        ElapsedTicksSinceStart += 1;
    }
        
    public void Render()
    {
        _renderer.Render(World);
    }
}