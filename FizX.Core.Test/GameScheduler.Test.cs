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
using System.Linq;

namespace FizX.Core.Test
{
    public class GameSchedulerTest
    {
        private readonly Mock<IRenderer> _rendererMock;
        private readonly Mock<IPhysicsSimulator> _physicsSimulatorMock;
        private readonly Mock<ILogger> _loggerMock;        
        private readonly Mock<IInputManager> _inputManagerMock;
        private readonly Mock<IWorldLoader> _worldLoaderMock;

        private readonly Mock<IGameBoundaries> _gameBoundariesMock;

        private Game _game;
        private GameScheduler _gameScheduler;

        public GameSchedulerTest()
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
            _gameScheduler = new GameScheduler(_game);
        }

        [Fact]
        public void DefaultTargetTickRate_ShouldBe60()
        {
            _gameScheduler.TargetTickRate.Should().Be(60);
        }

        [Fact]
        public void DefaultTargetFrameRate_ShouldBe120()
        {
            _gameScheduler.TargetFrameRate.Should().Be(60);
        }

        [Fact]
        public void It_ShouldNotBeRunning_BeforeItWasStarted()
        {
            _gameScheduler.IsRunning.Should().BeFalse();
        }

        [Fact]
        public void It_ShouldBeRunning_AfterItWasStarted()
        {
            _gameScheduler.Start();
            _gameScheduler.IsRunning.Should().BeTrue();
        }

        [Fact]
        public void It_ShouldNotBeRunning_AfterItWasStopped()
        {
            _gameScheduler.Start();
            _gameScheduler.Stop();
            _gameScheduler.IsRunning.Should().BeFalse();
        }
    }
}
