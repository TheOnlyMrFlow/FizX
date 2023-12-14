using FizX.Core.Actors;
using FizX.Core.ContentLoading;
using FizX.Core.Geometry.Shapes;
using FizX.Core.Physics.Collisions.ColliderComponents;
using FizX.Core.Worlds;

namespace FizX.WorldLoader;

public class WorldLoader : IWorldLoader
{
    private World _world;
    
    public IWorld LoadWorld()
    {
        return _world;
    }

    public void SetWorld(World world)
    {
        _world = world;
    }
}