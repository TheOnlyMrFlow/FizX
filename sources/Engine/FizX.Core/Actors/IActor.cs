using FizX.Core.Actors.ActorComponents;

namespace FizX.Core.Actors
{
    public interface IActor : ITickable
    {
        int Id { get; }
        
        public void AttachComponent(IActorComponent component);
        
        public void RemoveComponent(IActorComponent component);
    }
}