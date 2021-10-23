using System;
using System.Collections.Generic;
using System.Text;

namespace FizX.Core
{
    public class World : IWorld
    {
        private readonly List<IActor> _actors = new List<IActor>();
        public IEnumerable<IActor> Actors => _actors;

        public void Tick(int deltaTime)
        {
            foreach (var actor in _actors)
                actor.Tick(deltaTime);
        }

        public void AddActor(IActor actor)
        {
            _actors.Add(actor);
        }
    }
}
