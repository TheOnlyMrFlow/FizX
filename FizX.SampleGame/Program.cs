using FizX.Engine;
using FizX.Engine.Physics.Collisions;
using FizX.SampleGame;
using SFML.Graphics;
using SFML.System;

namespace GettingStarted
{
    class Program
    {
        public static void Main()
        {
            var game = new Engine(null);

            var player = new GameObject
            {
                Collider = new BoxCollider {Height = 20, Width = 20},
                Sprite = new RectangleShape()
                {
                    Texture = new Texture(@"C:\Users\comte\Desktop\sprite-player.png"),
                    Size = new Vector2f(40, 40)
                }
            };
            player.AddComponent(new SampleComponent());

            var obstacle = new GameObject
            {
                Collider = new BoxCollider {Height = 40, Width = 20},
                Sprite = new RectangleShape(new Vector2f(400, 50))
                {
                    FillColor = Color.Blue
                }
            };
            obstacle.Transform.Position = new Vector2f(100, 100);

            game.World.AddGameObject(player);
            game.World.AddGameObject(obstacle);

            game.Run();
        }
    }
}