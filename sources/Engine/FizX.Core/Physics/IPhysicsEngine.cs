using FizX.Core.Physics.Collisions.ColliderComponents;

namespace FizX.Core.Physics;

public interface IPhysicsEngine
{
    void Tick(decimal deltaMs);

    void RegisterCollider(ColliderComponent colliderComponent, string layer);
}