using System;

namespace FizX.Core;

public class GameScheduler
{
    public const decimal TickRate = 60;
    public Game Game { get; }
        
    public decimal TargetFrameRate { get; set; } = 60;
    public bool IsRunning { get; set; } = false;

    public GameScheduler(Game game)
    {
        Game = game;
    }

    public void Start()
    {
        IsRunning = true;
    }

    public void Stop()
    {
        IsRunning = false;
    }
}