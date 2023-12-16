using System.Threading;

namespace FizX.Core;

public interface IGameHost
{
    void HostGame(Game game, CancellationToken cancellationToken);
}