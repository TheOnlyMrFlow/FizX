using FizX.Core.Worlds;

namespace FizX.Core.Graphics;

public interface IRenderer
{
    void Render(IWorld world);
}