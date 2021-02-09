using System;
using FizX.Engine;
using FizX.Engine.Debug;
using FizX.Engine.Input;
using FizX.Engine.Physics.Collisions;
using SFML.System;
using SFML.Window;

namespace FizX.SampleGame
{
    public class BulletComponent : Component
    {
        public override void OnStart()
        {
            this.GameObject.Velocity = new Vector2f(2f, 0);
        }

        public override void OnUpdate()
        {

        }

        public void OnCollisionEnter(Collision collision)
        {
            Console.WriteLine("Enter !");
        }

        public void OnCollisionStay(Collision collision)
        {
            Console.WriteLine("Stay !");
        }

        public void OnCollisionExit(Collision collision)
        {
            Console.WriteLine("Exit !"); 
        }
    }
}
