using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using FizX.Engine.Physics;
using FizX.Engine.Physics.Collisions;
using SFML.Graphics;
using SFML.System;
using Transform = FizX.Engine.Maths.Transform;

namespace FizX.Engine
{
    public class GameObject
    {
        public bool IsDestroyed { get; private set; }
        public Transform Transform { get; private set; } = new Transform();
        public Vector2f Velocity { get; set; } = new Vector2f(0, 0);
        public ICollection<Component> Components = new List<Component>();
        public Shape Sprite { get; set; }

        private Collider _collider;
        public Collider Collider
        {
            get => _collider;
            set
            {
                if (_collider != null)
                    CollisionSystem.UnRegisterCollider(_collider);

                _collider = value;
                _collider.GameObject = this;

                CollisionSystem.RegisterCollider(_collider);
            }

        }
        public float Elapsed { get; private set; }

        public float Scale = 1f;

        public Guid Id { get; } = Guid.NewGuid();

        public void AddComponent(Component component)
        {
            Components.Add(component);
            component.SetGameObject(this);
        }

        public IEnumerable<T> GetComponents<T>() where T : Component
        {
            return (IEnumerable<T>) Components.Where(comp => (comp as T) != null).Select(comp => (T) comp);
        }

        public void Update(int elapsed)
        {
            Elapsed = Elapsed;
            Parallel.ForEach(Components, component => { component.OnUpdate(); });
        }

        public void Start()
        {
            Parallel.ForEach(Components, component => { component.OnStart(); });
        }

        public void Destroy()
        {
            Parallel.ForEach(Components, component => { component.OnDestroy(); });
            IsDestroyed = true;
        }

        public void OnCollisionEnter(Collision collision)
        {
            string methodName = "OnCollisionEnter";

            Parallel.ForEach(Components, component =>
            {
                component
                    .GetType()
                    .GetMethod(methodName)?
                    .Invoke(component, new[] { collision });
            });
        }

        public void OnCollisionStay(Collision collision)
        {
            string methodName = "OnCollisionStay";

            Parallel.ForEach(Components, component =>
            {
                component
                    .GetType()
                    .GetMethod(methodName)?
                    .Invoke(component, new[] { collision });
            });
        }

        public void OnCollisionExit(Collision collision)
        {
            string methodName = "OnCollisionExit";

            Parallel.ForEach(Components, component =>
            {
                component
                    .GetType()
                    .GetMethod(methodName)?
                    .Invoke(component, new[] { collision });
            });
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        
    }
}