using System;
using System.Collections.Generic;
using System.Text;

namespace FizX.Core
{
    public interface IWorld
    {
        IEnumerable<IActor> Actors { get; }
        void Tick(int deltaMs);
    }
}
