using FizX.Core.Common;
using FizX.Core.Timing;

namespace FizX.Core.Actors.ActorComponents;

public abstract class ActorComponent : ITickable
{
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