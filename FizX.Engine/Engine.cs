using System;
using System.Diagnostics;
using System.Threading;
using FizX.Engine.Debug;
using FizX.Engine.Input;
using FizX.Engine.Physics;
using FizX.Graphics;

namespace FizX.Engine
{
    public class Engine
    {
        private readonly ILogger _logger;
        private readonly RenderingSystem _renderingSystem;
        private readonly InputSystem _inputSystem;

        public World World { get; private set; } = new World();

        public int MaxFPS { get; set; } = 60; 
        public Engine(ILogger logger)
        {
            _renderingSystem = new RenderingSystem(800, 400, "Engine");
            _inputSystem = new InputSystem(_renderingSystem.Window);
            _logger = logger;
            Debug.Debug.SetLogger(logger);
        }

        public void Run()
        {

            int minLoopDuration = (int) (1f / MaxFPS * 1000);

            var watch = new Stopwatch();
            World.Start();

            long workedFor;
            int sleepTime;
            
            int elapsed = 0;

            while (true)
            {
                watch.Restart();
                Input.Input.SetSnapshot(_inputSystem.GetInput());
                PhysicsSystem.Run(World, minLoopDuration);
                World.Update(minLoopDuration);
                _renderingSystem.Render(World);
                workedFor = watch.ElapsedMilliseconds;
                sleepTime = (int) Math.Max(minLoopDuration - workedFor, 0);
                Thread.Sleep(sleepTime);
            }
        }

    }
}
