namespace FizX.Core.Actors.ActorComponents
{
    public interface IActorComponent : ITickable
    {
        IActor? GetActor();

        internal void SetActor(IActor? actor);
    }
}