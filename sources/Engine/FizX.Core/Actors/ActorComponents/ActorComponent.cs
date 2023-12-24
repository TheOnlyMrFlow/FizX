using FizX.Core.Common;

namespace FizX.Core.Actors.ActorComponents;

public abstract class ActorComponent : ITickable
{
    public Actor? Actor { get; internal set; }

    public virtual void Tick(float deltaMs) { }

    public override string ToString()
    {
        return $"[Type: {GetType()}]";
    }
}