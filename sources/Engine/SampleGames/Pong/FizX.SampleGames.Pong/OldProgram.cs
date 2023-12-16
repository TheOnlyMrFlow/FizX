// using FizX.Core;
// using FizX.Core.Actors;
// using FizX.Core.Geometry.Shapes;
// using FizX.Core.Physics.Collisions.ColliderComponents;
// using FizX.Core.Worlds;
// using FizX.Events;
// using FizX.OpenTK;
// using FizX.Physics;
// using FizX.Renderer;
// using FizX.SampleGames.Pong;
// using FizX.WorldLoader;
//
// var cancellationTokenSource = new CancellationTokenSource();
//
// // Register a callback for the Ctrl+C event
// Console.CancelKeyPress += (sender, eventArgs) => {
//     // Cancel the CancellationTokenSource when Ctrl+C is pressed
//     cancellationTokenSource.Cancel();
//     // Prevent the default Ctrl+C handling (termination) to allow for cleanup
//     eventArgs.Cancel = true;
// };
//
// #region SetWorld
// var world = new World();
// world.AddActor(new Actor());
//
// var actor2 = new Actor();
// actor2.AttachComponent(new BoxColliderComponent(new RectangleShape(30, 20)));
// actor2.AttachComponent(new MyCustomComponent());
// world.AddActor(actor2);
//
// var wl = new WorldLoader();
// wl.SetWorld(world);
//
// #endregion
//
// var openTkRenderer = new OpenTkRenderer();
//
// var gameBoundaries = new GameBoundaries
// {
//     Renderer = openTkRenderer,
//     WorldLoader = wl,
//     PhysicsEngine = new PhysicsEngine(),
//     EventBus = new EventBus()
// };
//
// var game = new Game(gameBoundaries);
//
// var gameHost = new BasicGameHost(game);
//
// gameHost.StartGame(cancellationTokenSource.Token);
//
// openTkRenderer.Init();