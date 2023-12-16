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

    private readonly Mock<GameBoundaries> _gameBoundariesMock = new();

    private readonly Game _game;
    private readonly BasicGameHost _basicGameHost;

    public GameScheduler_Test()
    {
        _gameBoundariesMock.Setup(gb => gb.Renderer).Returns(_rendererMock.Object);
        _gameBoundariesMock.Setup(gb => gb.PhysicsEngine).Returns(_physicsSimulatorMock.Object);
        _gameBoundariesMock.Setup(gb => gb.Logger).Returns(_loggerMock.Object);
        _gameBoundariesMock.Setup(gb => gb.InputManager).Returns(_inputManagerMock.Object);
        _gameBoundariesMock.Setup(gb => gb.WorldLoader).Returns(_worldLoaderMock.Object);


        _game = new Game(_gameBoundariesMock.Object);
        _basicGameHost = new BasicGameHost(_game);
    }

    [Fact]
    public void DefaultTargetFrameRate_ShouldBe60()
    {
        _basicGameHost.MaxFrameRate.Should().Be(60);
    }
        
    [Fact]
    public void TickRate_ShouldBeAConstWorth60()
    {
        BasicGameHost.TickRate.Should().Be(60);
    }

    [Fact]
    public void It_ShouldNotBeRunning_BeforeItWasStarted()
    {
        _basicGameHost.IsRunning.Should().BeFalse();
    }

    [Fact]
    public void It_ShouldBeRunning_AfterItWasStarted()
    {
        _basicGameHost.StartGame();
        _basicGameHost.IsRunning.Should().BeTrue();
    }

    [Fact]
    public void It_ShouldNotBeRunning_AfterItWasStopped()
    {
        _basicGameHost.StartGame();
        _basicGameHost.Stop();
        _basicGameHost.IsRunning.Should().BeFalse();
    }
}