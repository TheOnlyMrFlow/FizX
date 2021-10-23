namespace FizX.Core.Actors.ActorComponents
{
    public class ActorComponent : IActorComponent
    {
        private IActor? _actor;

        public IActor? GetActor()
            => _actor;

        void IActorComponent.SetActor(IActor? actor)
            => _actor = actor;
    }
}