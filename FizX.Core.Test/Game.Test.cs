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

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(10)]
        public void ElapsedFrames_ShouldEqualX_AfterRenderingXTimes(int x)
        {
            Enumerable.Range(0, x).ToList().ForEach(i => _game.Render());
            _game.ElapsedFramesSinceStart.Should().Be(x);
        }

        [Fact]
        public  void Game_ShouldCallRenderer_AtEveryFrame()
        {
            _game.Render();
            _rendererMock.Verify(r => r.Render(), Times.Once);
            _game.Render();
            _rendererMock.Verify(r => r.Render(), Times.Exactly(2));
            _game.Render();
            _rendererMock.Verify(r => r.Render(), Times.Exactly(3));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(10)]
        public void ElapsedTicks_ShouldEqualX_AfterTickingXTimes(int x)
        {
            Enumerable.Range(0, x).ToList().ForEach(i => _game.Tick());
            _game.ElaspedTicksSinceStart.Should().Be(x);
        }

        [Fact]
        public void Game_ShouldCallPhysicsSimulator_AtEveryTick()
        {
            _game.Tick();
            _physicsSimulatorMock.Verify(p => p.Tick(), Times.Once);
            _game.Tick();
            _physicsSimulatorMock.Verify(p => p.Tick(), Times.Exactly(2));
            _game.Tick();
            _physicsSimulatorMock.Verify(p => p.Tick(), Times.Exactly(3));
        }
    }
}
