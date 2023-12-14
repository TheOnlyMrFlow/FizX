using FizX.Core.ContentLoading;
using FizX.Core.Events;
using FizX.Core.Graphics;
using FizX.Core.Input;
using FizX.Core.Logging;
using FizX.Core.Physics;

namespace FizX.Core;

public class GameBoundaries
{
    public ILogger Logger { get; init; }
    public IRenderer Renderer { get; init; }
    public IInputManager InputManager { get; init; }
    public IWorldLoader WorldLoader { get; init; }
    public IPhysicsEngine PhysicsEngine { get; init; }
    public IEventBus EventBus { get; init; }
}