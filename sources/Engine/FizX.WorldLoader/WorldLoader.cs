using FizX.Core.ContentLoading;
using FizX.Core.Worlds;

namespace FizX.WorldLoader;

public class WorldLoader : IWorldLoader
{
    private World _world;
    
    public World LoadWorld()
    {
        return _world;
    }

    public void SetWorld(World world)
    {
        _world = world;
    }
}