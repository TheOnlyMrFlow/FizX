using System;
using Xunit;
using Moq;
using FizX.Core.Physics;
using FizX.Core.Logging;
using FizX.Core.Input;
using FizX.Core.ContentLoading;
using FluentAssertions;
using System.Threading;
using System.Diagnostics;
using System.Linq;
using FizX.Core.Graphics;

namespace FizX.Core.Test;

public class GameScheduler_Test
{
    private readonly Mock<IRenderer> _rendererMock = new();
    private readonly Mock<IPhysicsEngine> _physicsSimulatorMock = new();
    private readonly Mock<ILogger> _loggerMock = new();        
    private readonly Mock<IInputManager> _inputManagerMock = new();
    private readonly Mock<IWorldLoader> _worldLoaderMock = new();

    private readonly Mock<IGameBoundaries> _gameBoundariesMock = new();

    private readonly Game _game;
    private readonly GameScheduler _gameScheduler;

    public GameScheduler_Test()
    {
        _gameBoundariesMock.Setup(gb => gb.Renderer).Returns(_rendererMock.Object);
        _gameBoundariesMock.Setup(gb => gb.PhysicsEngine).Returns(_physicsSimulatorMock.Object);
        _gameBoundariesMock.Setup(gb => gb.Logger).Returns(_loggerMock.Object);
        _gameBoundariesMock.Setup(gb => gb.InputManager).Returns(_inputManagerMock.Object);
        _gameBoundariesMock.Setup(gb => gb.WorldLoader).Returns(_worldLoaderMock.Object);


        _game = new Game(_gameBoundariesMock.Object);
        _gameScheduler = new GameScheduler(_game);
    }

    [Fact]
    public void DefaultTargetFrameRate_ShouldBe60()
    {
        _gameScheduler.TargetFrameRate.Should().Be(60);
    }
        
    [Fact]
    public void TickRate_ShouldBeAConstWorth60()
    {
        GameScheduler.TickRate.Should().Be(60);
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