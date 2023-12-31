using System.Threading;
using FizX.Core.Common;
using FizX.Core.Timing;

namespace FizX.Core.Actors.ActorComponents;

public abstract class ActorComponent : ITickable
{
    private static int _lastRandomId = -1;
    
    public static int NextRandomId() => Interlocked.Increment(ref _lastRandomId);
    
    public readonly int Id = NextRandomId();
    
    public Actor? Actor { get; internal set; }

    protected Game Game => Game.Instance;

    protected TimeLine TimeLine => Actor!.TimeLine;
        
    public virtual void Tick(FrameInfo frame) { }

    public virtual void RewindTick() { }
    
    public override string ToString()
    {
        return $"[Type: {GetType()}]";
    }

    public virtual void OnStartRewinding() { }
    
    public virtual void OnStopRewinding() { }

}