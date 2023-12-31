using FizX.Core;
using FizX.Core.Actors;
using FizX.Core.Geometry.Shapes;
using FizX.Core.Graphics;
using FizX.Core.Input;
using FizX.Core.Physics.Collisions.ColliderComponents;
using FizX.Core.Timing;
using FizX.Core.Worlds;
using FizX.Events;
using FizX.OpenTK;
using FizX.Physics;
using FizX.Renderer;
using FizX.SampleGames.Pong;
using FizX.WorldLoader;

var cancellationTokenSource = new CancellationTokenSource();

// Register a callback for the Ctrl+C event
Console.CancelKeyPress += (sender, eventArgs) => {
    // Cancel the CancellationTokenSource when Ctrl+C is pressed
    cancellationTokenSource.Cancel();
    // Prevent the default Ctrl+C handling (termination) to allow for cleanup
    eventArgs.Cancel = true;
};

#region SetWorld
var world = new World();
var actor1 = new Actor();
actor1.AttachComponent(new Actor1Component());
actor1.AttachComponent(new SpriteRendererComponent()
{
    TextureFilePath = "Resources/cat.png"
});
world.AddActor(actor1, TimeLineIndex.TimeLine1);

var actor2 = new Actor();
actor2.AttachComponent(new BoxColliderComponent(new RectangleShape(30, 20)));
actor2.AttachComponent(new Actor2Component());
actor2.AttachComponent(new SpriteRendererComponent()
{
    TextureFilePath = "Resources/dog.png"
});
//world.AddActor(actor2, TimeLineIndex.TimeLine0);

var wl = new WorldLoader();
wl.SetWorld(world);

#endregion

var openTkGameHost = new OpenTkGameHost();

var gameBoundaries = new GameBoundaries
{
    RenderingEngineFactory = () => openTkGameHost.RenderingEngine,
    WorldLoaderFactory = () => wl,
    PhysicsEngineFactory = () => new PhysicsEngine(),
    EventBusFactory = () => new EventBus(),
    InputManagerFactory = () =>
    {
        var inputManager = new InputManager();
        inputManager.MapInputVector("Move", [
            new InputVectorComponent(KeyboardInputKey.W, InputAxis.Y, 1f),
            new InputVectorComponent(KeyboardInputKey.S, InputAxis.Y, -1f),
            new InputVectorComponent(KeyboardInputKey.D, InputAxis.X, 1f),
            new InputVectorComponent(KeyboardInputKey.A, InputAxis.X, -1f)
        ]);
        
        inputManager.MapInputVector("Rewind", KeyboardInputKey.Space, InputAxis.Y);

        return inputManager;
    },
    LoggerFactory = () => null!
};

openTkGameHost.HostGame(new Game(gameBoundaries), cancellationTokenSource.Token);