using SFML.System;

namespace FizX.Engine.Physics.Collisions
{
    public class Collision
    {
        public Collision(Collider collider1, Collider collider2)
        {
            Collider1 = collider1;
            Collider2 = collider2;
        }
        public Collider Collider1 { get; private set; }
        public Collider Collider2 { get; private set; }
    }
}
