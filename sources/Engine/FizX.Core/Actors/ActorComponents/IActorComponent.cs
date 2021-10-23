namespace FizX.Core.Actors.ActorComponents
{
    public interface IActorComponent
    {
        IActor? GetActor();

        internal void SetActor(IActor? actor);
        void Tick(int deltaMs);
    }
}