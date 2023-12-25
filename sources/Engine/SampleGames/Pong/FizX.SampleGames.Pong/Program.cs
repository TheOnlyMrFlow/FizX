using FizX.Core;
using FizX.Core.Actors;
using FizX.Core.Geometry.Shapes;
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
world.AddActor(actor1, TimeLineIndex.TimeLine1);

var actor2 = new Actor();
actor2.AttachComponent(new BoxColliderComponent(new RectangleShape(30, 20)));
actor2.AttachComponent(new Actor2Component());
world.AddActor(actor2, TimeLineIndex.TimeLine0);

var wl = new WorldLoader();
wl.SetWorld(world);

#endregion

var openTkGameHost = new OpenTkGameHost();

var gameBoundaries = new GameBoundaries
{
    RenderingEngine = openTkGameHost.RenderingEngine,
    WorldLoader = wl,
    PhysicsEngine = new PhysicsEngine(),
    EventBus = new EventBus()
};

openTkGameHost.HostGame(new Game(gameBoundaries), cancellationTokenSource.Token);