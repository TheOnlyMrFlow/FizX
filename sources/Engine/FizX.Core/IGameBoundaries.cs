using FizX.Core.ContentLoading;
using FizX.Core.Graphics;
using FizX.Core.Input;
using FizX.Core.Logging;
using FizX.Core.Physics;

namespace FizX.Core
{
     public interface IGameBoundaries
    {
        ILogger Logger { get; }
        IRenderer Renderer { get; }
        IInputManager InputManager { get; }
        IWorldLoader WorldLoader { get; }
        IPhysicsSystem PhysicsSystem { get; }
    }
}
