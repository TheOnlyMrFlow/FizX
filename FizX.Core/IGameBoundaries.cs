using FizX.Core.ContentLoading;
using FizX.Core.Input;
using FizX.Core.Logging;
using FizX.Core.Physics;
using FizX.Core.Rendering;

namespace FizX.Core
{
     public interface IGameBoundaries
    {
        ILogger Logger { get; }
        IRenderer Renderer { get; }
        IInputManager InputManager { get; }
        IWorldLoader WorldLoader { get; }
        IPhysicsSimulator PhysicsSimulator { get; }
    }
}
