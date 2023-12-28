using System;
using FizX.Core.ContentLoading;
using FizX.Core.Events;
using FizX.Core.Graphics;
using FizX.Core.Input;
using FizX.Core.Logging;
using FizX.Core.Physics;

namespace FizX.Core;

public class GameBoundaries
{
    public Func<ILogger> LoggerFactory { get; init; }
    public Func<IRenderingEngine> RenderingEngineFactory { get; init; }
    public Func<IInputManager> InputManagerFactory { get; init; }
    public Func<IWorldLoader> WorldLoaderFactory { get; init; }
    public Func<IPhysicsEngine> PhysicsEngineFactory { get; init; }
    public Func<IEventBus> EventBusFactory { get; init; }
}