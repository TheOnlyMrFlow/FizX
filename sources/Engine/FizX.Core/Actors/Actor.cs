using System;
using System.Collections.Generic;
using FizX.Core.Actors.ActorComponents;

namespace FizX.Core.Actors
{
    public class Actor : IActor
    {
        public int Id => throw new NotImplementedException();
        
        private readonly ICollection<IActorComponent> _components = new List<IActorComponent>();

        public IEnumerable<IActorComponent> Components => _components;

        public void AttachComponent(IActorComponent component)
        {
            component.SetActor(this);
            _components.Add(component);
        }

        public void RemoveComponent(IActorComponent component)
        {
            _components.Remove(component);
            component.SetActor(null);
        }

        public void Tick(int deltaMs)
        {

        }
    }
}