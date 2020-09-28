using System;
using FizX.Engine;
using FizX.Engine.Debug;
using FizX.Engine.Input;
using FizX.Engine.Physics.Collisions;
using SFML.System;
using SFML.Window;

namespace FizX.SampleGame
{
    public class SampleComponent : Component
    {
        private float velX = 0f, velY = 0f;
        public override void OnStart()
        {
            Debug.LogInformation("on start");
        }

        public override void OnUpdate()
        {

            if (Input.WasPressedDown(Keyboard.Key.Up))
                velY -= 0.1f;
            if (Input.WasReleased(Keyboard.Key.Up))
                velY += 0.1f;
            if (Input.WasPressedDown(Keyboard.Key.Down))
                velY += 0.1f;
            if (Input.WasReleased(Keyboard.Key.Down))
                velY -= 0.1f;
            if (Input.WasPressedDown(Keyboard.Key.Left))
                velX -= 0.1f;
            if (Input.WasReleased(Keyboard.Key.Left))
                velX += 0.1f;
            if (Input.WasPressedDown(Keyboard.Key.Right))
                velX += 0.1f;
            if (Input.WasReleased(Keyboard.Key.Right))
                velX -= 0.1f;

            GameObject.Velocity = new Vector2f(velX, velY);

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
