using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace FizX.Core;

public class BasicGameHost : IGameHost
{
    private const decimal DefaultMaxTickRate = 240;
    private const decimal DefaultMaxFrameRate = 120;

    private readonly Stopwatch _stopwatch = new();
    
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

    public BasicGameHost()
    {
        SetMaxTickRate(DefaultMaxTickRate);
        SetMaxFrameRate(DefaultMaxFrameRate);
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

    public void HostGame(Game game, CancellationToken cancellationToken = default)
    {
        IsRunning = true;
        _stopwatch.Start();

        var nextRenderStartsNotBefore = 0f;
        var nextTickStartsNotBefore = 0f;
        var lasTickStartedAt = 0f;
        ulong frameIndex = 0;

        game.BeforeStart();
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
                game.Tick(new FrameInfo
                {
                    Index = frameIndex,
                    ElapsedMs = _stopwatch.ElapsedMilliseconds,
                    DeltaTimeMs = deltaMs,
                });
                lasTickStartedAt = currentTickIsStartingAt;
                frameIndex++;
            }

            nextRenderStartsNotBefore = _stopwatch.ElapsedMilliseconds + MinMillisPerFrame;
            Console.WriteLine("Rendering");
            game.Render();
        }
    }

    public void Stop()
    {
        IsRunning = false;
    }
}