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

        public bool IsRunning => 
            _mainTask != null 
            && ( _mainTask.Status == TaskStatus.Running || _mainTask.Status == TaskStatus.WaitingToRun);

        public int ElapsedFramesSinceStart { get; set; }
        public decimal ElapsedMillisecondsSinceStart { get; set; }

        public void Start()
        {
            _mainTask = new Task(Run, TaskCreationOptions.LongRunning);
            _mainTask.Start();
        }

        private void Run()
        {
            var stopWatch = new Stopwatch();
            while (! _mainTaskCancelationToken.IsCancellationRequested)
            {
                Thread.Sleep(16);
                _renderer.Render();
                ElapsedMillisecondsSinceStart += 16;
                ElapsedFramesSinceStart++;
            }
        }

        public Task StopAsync()
        {
            var task = new Task(() =>
            {
                RequestToStop();

                while (IsRunning)
                    Thread.Sleep(1);
            });
            task.Start();

            return task;
        }

        public void RequestToStop()
        {
            _mainTaskCancelationToken = new CancellationToken(true);
        }

        public Task WaitForNextFrameAsync()
        {
            var frameCount = ElapsedFramesSinceStart;
            var task = new Task(() =>
            {
                while (ElapsedFramesSinceStart == frameCount)
                    Thread.Sleep(1);
            });
            task.Start();

            return task;
        }

        public Task WaitForFrameAsync(int frame)
        {
            var task = new Task(() =>
            {
                while (ElapsedFramesSinceStart < frame)
                    Thread.Sleep(1);
            });
            task.Start();

            return task;
        }

        public Task WaitForFirstFrameAsync()
        {
            var task = new Task(() =>
            {
                while (ElapsedFramesSinceStart == 0)
                    Thread.Sleep(1);
            });
            task.Start();

            return task;
        }
    }
}
