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
    public static Game Instance { get; private set; }

    public IInputManager InputManager { get; }
    public IEventBus EventBus { get; }

    private readonly ILogger _logger;
    private readonly IPhysicsEngine _physicsEngine;
    private readonly IRenderingEngine _renderingEngine;
    private readonly IWorldLoader _worldLoader;

    public World World { get; private set; }

    public int ElapsedFramesSinceStart { get; private set; } = 0;
    public int ElapsedTicksSinceStart { get; private set; } = 0;

    public Game(GameBoundaries boundaries)
    {
        InputManager = boundaries.InputManagerFactory();
        _logger = boundaries.LoggerFactory();
        _physicsEngine = boundaries.PhysicsEngineFactory();
        _renderingEngine = boundaries.RenderingEngineFactory();
        _worldLoader = boundaries.WorldLoaderFactory();
        EventBus = boundaries.EventBusFactory();

        World = _worldLoader.LoadWorld();

        Instance = this;
    }

    public void BeforeStart()
    {
        InputManager.BeforeGameStarts();
    }
    
    public void Tick(FrameInfo frame)
    {
        World.Tick(frame);

        _physicsEngine.Tick(frame);
            
        ElapsedTicksSinceStart += 1;
        
        InputManager.OnTickEnded();
    }
        
    public void Render()
    {
        _renderingEngine.RenderWorld(World);
    }

}