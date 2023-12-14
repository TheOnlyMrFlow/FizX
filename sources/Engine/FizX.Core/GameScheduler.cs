using System;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace FizX.Core;

public class GameScheduler
{
    private const decimal _defaultTargetTickRate = 120;
    private const decimal _defaultTargetFrameRate = 1;

    private readonly Stopwatch _stopwatch = new();
    
    public Game Game { get; }
        
    public decimal MaxFrameRate { get; private set; }
    public int MinMillisPerFrame { get; private set; }
    
    public decimal MaxTickRate { get; private set; }
    public int MinMillisPerTick { get; private set; }
    
    public bool IsRunning { get; set; } = false;

    public GameScheduler(Game game)
    {
        SetMaxTickRate(_defaultTargetTickRate);
        SetMaxFrameRate(_defaultTargetFrameRate);
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

    public async Task Start(CancellationToken cancellationToken = default)
    {
        IsRunning = true;
        _stopwatch.Restart();

        var anyTaskFailedCts = new CancellationTokenSource();
        var cToken = CancellationTokenSource.CreateLinkedTokenSource(anyTaskFailedCts.Token, cancellationToken).Token;

        async Task TickForeverSafe()
        {
            try
            {
                await TickForever(cToken).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await anyTaskFailedCts.CancelAsync();
                throw;
            }
        }
        
        async Task RenderForeverSafe()
        {
            try
            {
                await RenderForever(cToken).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await anyTaskFailedCts.CancelAsync();
                throw;
            }
        }
        
        await Task.WhenAll(TickForeverSafe(), RenderForeverSafe()).ConfigureAwait(false);
    }
    
    protected async Task TickForever(CancellationToken cancellationToken = default)
    {
        var lastTickDurationMs = 0;
        while (!cancellationToken.IsCancellationRequested)
        {
            var elapsedBeforeTick = _stopwatch.ElapsedMilliseconds;
            
            Game.Tick(lastTickDurationMs);
            
            lastTickDurationMs = (int) (_stopwatch.ElapsedMilliseconds - elapsedBeforeTick);
            
            var delayMs = MinMillisPerTick - lastTickDurationMs;
            if (delayMs > 0 && !cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(delayMs, cancellationToken);
            }
        }
    }
    
    protected async Task RenderForever(CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var elapsedBeforeRender = _stopwatch.ElapsedMilliseconds;
            
            Game.Render();
            
            var lastRenderDurationMs = (int) (_stopwatch.ElapsedMilliseconds - elapsedBeforeRender);
            
            var delayMs = MinMillisPerFrame - lastRenderDurationMs;
            if (delayMs > 0 && !cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(delayMs, cancellationToken);
            }
        }
    }

    public void Stop()
    {
        IsRunning = false;
    }
}