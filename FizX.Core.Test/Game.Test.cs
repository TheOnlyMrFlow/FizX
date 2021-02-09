using System;
using Xunit;
using Moq;
using FizX.Core.Rendering;
using FizX.Core.Physics;
using FizX.Core.Logging;
using FizX.Core.Input;
using FizX.Core.ContentLoading;
using FluentAssertions;
using System.Threading;
using System.Diagnostics;

namespace FizX.Core.Test
{
    public class GameTest
    {
        private readonly Mock<IRenderer> _rendererMock;
        private readonly Mock<IPhysicsSimulator> _physicsSimulatorMock;
        private readonly Mock<ILogger> _loggerMock;        
        private readonly Mock<IInputManager> _inputManagerMock;
        private readonly Mock<IWorldLoader> _worldLoaderMock;

        private readonly Mock<IGameBoundaries> _gameBoundariesMock;

        private Game _game;

        public GameTest()
        {
            _rendererMock = new Mock<IRenderer>();
            _physicsSimulatorMock = new Mock<IPhysicsSimulator>();
            _loggerMock = new Mock<ILogger>();
            _inputManagerMock = new Mock<IInputManager>();
            _worldLoaderMock = new Mock<IWorldLoader>();

            _gameBoundariesMock = new Mock<IGameBoundaries>();
            _gameBoundariesMock.Setup(gb => gb.Renderer).Returns(_rendererMock.Object);
            _gameBoundariesMock.Setup(gb => gb.PhysicsSimulator).Returns(_physicsSimulatorMock.Object);
            _gameBoundariesMock.Setup(gb => gb.Logger).Returns(_loggerMock.Object);
            _gameBoundariesMock.Setup(gb => gb.InputManager).Returns(_inputManagerMock.Object);
            _gameBoundariesMock.Setup(gb => gb.WorldLoader).Returns(_worldLoaderMock.Object);


            _game = new Game(_gameBoundariesMock.Object);
        }

        [Fact]
        public void Game_ShouldNotBeRunning_BeforeItWasStarted()
        {
            _game.IsRunning.Should().BeFalse();
        }

        [Fact]
        public async void Game_ShouldBeRunning_AfterItWasStarted()
        {
            _game.Start();
            _game.IsRunning.Should().BeTrue();
        }

        [Fact]
        public async void Game_ShouldNotBeRunning_AfterItWasStopped()
        {
            _game.Start();
            await _game.StopAsync();
            _game.IsRunning.Should().BeFalse();
        }

        [Fact]
        public async void WaitForFirstFrame_ShouldResolve_ShortlyAfterStartingTheGame()
        {
            var stopWatch = new Stopwatch();
            _game.Start();
            await _game.WaitForFirstFrameAsync();
            stopWatch.ElapsedMilliseconds.Should().BeLessThan(100);
        }

        [Fact]
        public async void WaitForFrame_ShouldNotResolve_BeforeTheGivenFrameHasElapsed()
        {
            _game.Start();
            await _game.WaitForFrameAsync(10);
            _game.ElapsedFramesSinceStart.Should().BeGreaterOrEqualTo(10);
        }

        [Fact]
        public async void ElapsedFrames_ShouldIncreaseByOne_AfterWaitingForNextFrame()
        {
            _game.Start();
            await _game.WaitForNextFrameAsync();
            var a = _game.ElapsedFramesSinceStart;
            await _game.WaitForNextFrameAsync();
            var b = _game.ElapsedFramesSinceStart;
            b.Should().Equals(a + 1);
        }

        [Fact]
        public void Game_ShouldRunAt60Fps()
        {
            _game.Start();
            while (_game.ElapsedFramesSinceStart < 10) { }
            var actualFps = (_game.ElapsedMillisecondsSinceStart / _game.ElapsedFramesSinceStart);
            actualFps.Should().BeInRange(13, 19);
        }

        [Fact]
        public async void Game_ShouldCallRenderer_AtEveryFrame()
        {
            _game.Start();
            await _game.WaitForFrameAsync(10); // could be any significantly 'large' number
            _rendererMock.Verify(r => r.Render(), Times.Exactly(_game.ElapsedFramesSinceStart));
        }
    }
}
