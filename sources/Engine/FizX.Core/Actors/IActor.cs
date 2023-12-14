using FizX.Core.Actors.ActorComponents;
using FizX.Core.Common;
using FizX.Core.Geometry;

namespace FizX.Core.Actors;

public interface IActor : ITickable
{
    int Id { get; }

    public Transform Transform { get; }
        
    public void AttachComponent(IActorComponent component);
        
    public void RemoveComponent(IActorComponent component);
}