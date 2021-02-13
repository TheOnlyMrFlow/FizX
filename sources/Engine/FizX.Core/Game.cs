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
        private readonly IPhysicsSystem _physicsSystem;
        private readonly IRenderer _renderer;
        private readonly IWorldLoader _worldLoader;

        public IWorld World { get; private set; }

        public int ElapsedFramesSinceStart { get; private set; } = 0;
        public int ElaspedTicksSinceStart { get; private set; } = 0;

        public Game(IGameBoundaries boundaries)
        {
            _inputManager = boundaries.InputManager;
            _logger = boundaries.Logger;
            _physicsSystem = boundaries.PhysicsSystem;
            _renderer = boundaries.Renderer;
            _worldLoader = boundaries.WorldLoader;

            World = _worldLoader.LoadWorld();
        }


        public void Tick()
        {
            World.Tick(123);

            _physicsSystem.Tick();

            _renderer.Render();

            ElaspedTicksSinceStart += 1;
        }
    }
}
