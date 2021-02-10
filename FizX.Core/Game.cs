using FizX.Core.ContentLoading;
using FizX.Core.Input;
using FizX.Core.Logging;
using FizX.Core.Physics;
using FizX.Core.Rendering;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace FizX.Core
{
    public class Game
    {
        private readonly IInputManager _inputManager;
        private readonly ILogger _logger;
        private readonly IPhysicsSimulator _physicsSimulator;
        private readonly IRenderer _renderer;
        private readonly IWorldLoader _worldLoader;

        private Task _mainTask;
        private CancellationToken _mainTaskCancelationToken = new CancellationToken(false);

        public Game(IGameBoundaries boundaries)
        {
            _inputManager = boundaries.InputManager;
            _logger = boundaries.Logger;
            _physicsSimulator = boundaries.PhysicsSimulator;
            _renderer = boundaries.Renderer;
            _worldLoader = boundaries.WorldLoader;
        }

        public int ElapsedFramesSinceStart { get; private set; } = 0;
        public int ElaspedTicksSinceStart { get; private set; } = 0;

        public void Render()
        {
            ElapsedFramesSinceStart += 1;
            _renderer.Render();
        }

        public void Tick()
        {
            ElaspedTicksSinceStart += 1;
            _physicsSimulator.Tick();
        }
    }
}
