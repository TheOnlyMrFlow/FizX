using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace FizX.Engine.Physics.Collisions
{
    public class ColliderRegistry
    {
        private Dictionary<int, HashSet<Collider>> _layers = new Dictionary<int, HashSet<Collider>>();

        public ImmutableDictionary<int, HashSet<Collider>> Layers => _layers.ToImmutableDictionary();
        
        internal void RegisterCollider(Collider collider)
        {
            if (!_layers.TryGetValue(collider.Layer, out var layer))
            {
                layer = new HashSet<Collider>();
                _layers.Add(collider.Layer, layer);
            }

            layer.Add(collider);
        }

        internal void UnRegisterCollider(Collider collider)
        {
            _layers[collider.Layer].Remove(collider);
        }

        
        
    }
}
