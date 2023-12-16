using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace FizX.Core;

public class GameScheduler
{
    private const decimal DefaultMaxTickRate = 240;
    private const decimal DefaultMaxFrameRate = 120;

    private readonly Stopwatch _stopwatch = new();
    
    public Game Game { get; }
        
    public decimal MaxFrameRate { get; private set; }
    public int MinMillisPerFrame { get; private set; }
    
    public decimal MaxTickRate { get; private set; }
    public int MinMillisPerTick { get; private set; }
    
    public bool IsRunning { get; set; } = false;

    private float _lastTickStartedAt = 0f;
    private float _lastTickFinishedAt = 0f;
    private float _lastRenderStartedAt = 0f;
    private float _lastRenderFinishedAt = 0f;
    
    private float _nextTickStartsAt = 0f;
    private float _nextRenderStartsAt = 0f;

    public GameScheduler(Game game)
    {
        SetMaxTickRate(DefaultMaxTickRate);
        SetMaxFrameRate(DefaultMaxFrameRate);
        Game = game;
    }
    
    public void SetMaxTickRate(decimal maxTickRate)
    {
        MaxTickRate = maxTickRate;
        MinMillisPerTick = (int) (1_000 / maxTickRate);
    }
    
    public void SetMaxFrameRate(decimal maxFrameRate)
    {
        MaxFrameRate = maxFrameRate;
        MinMillisPerFrame = (int) (1_000 / maxFrameRate);
    }

    public void Start(CancellationToken cancellationToken = default)
    {
        IsRunning = true;
        _stopwatch.Start();

        var anyTaskFailedCts = new CancellationTokenSource();
        var cToken = CancellationTokenSource.CreateLinkedTokenSource(anyTaskFailedCts.Token, cancellationToken).Token;

        var nextRenderStartsNotBefore = 0f;
        var nextTickStartsNotBefore = 0f;
        var lasTickStartedAt = 0f;
        while (!cancellationToken.IsCancellationRequested)
        {
            while (_stopwatch.ElapsedMilliseconds < nextRenderStartsNotBefore)
            {
                if (_stopwatch.ElapsedMilliseconds < nextTickStartsNotBefore)
                {
                    continue;
                }
                
                nextTickStartsNotBefore = _stopwatch.ElapsedMilliseconds + MinMillisPerTick;
                
                var currentTickIsStartingAt = _stopwatch.ElapsedMilliseconds;
                var deltaMs = currentTickIsStartingAt - lasTickStartedAt;
                Console.WriteLine("Ticking " + deltaMs);
                Game.Tick((int) deltaMs);
                lasTickStartedAt = currentTickIsStartingAt;
            }

            nextRenderStartsNotBefore = _stopwatch.ElapsedMilliseconds + MinMillisPerFrame;
            Console.WriteLine("Rendering");
            Game.Render();
        }
    }

    public void Stop()
    {
        IsRunning = false;
    }
}