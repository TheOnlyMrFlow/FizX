namespace FizX.Core.Actors.ActorComponents;

public abstract class ActorComponent : IActorComponent
{
    private IActor? _actor;

    public IActor? GetActor()
        => _actor;

    void IActorComponent.SetActor(IActor? actor)
        => _actor = actor;

    public virtual void Tick(int deltaMs) { }
}