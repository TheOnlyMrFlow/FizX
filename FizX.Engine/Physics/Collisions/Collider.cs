using System.Numerics;

namespace FizX.Engine.Physics.Collisions
{
    public abstract class Collider
    {
        public Vector2 Offset { get; set; } = Vector2.Zero;
        public GameObject GameObject { get; set; }

        private int _layer;

        public int Layer
        {
            get => _layer;
            set
            {
                if (value != _layer)
                    CollisionSystem.UnRegisterCollider(this);

                _layer = value;

                CollisionSystem.RegisterCollider(this);
            }
        }

        public override int GetHashCode()
        {
            return GameObject.GetHashCode();
        }

        public enum EType
        {  
            Static,
            Dynamic
        }
    }
}