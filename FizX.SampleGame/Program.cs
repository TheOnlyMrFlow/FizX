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
                Collider = new BoxCollider {Height = 40, Width = 40},
                Sprite = new RectangleShape()
                {
                    Texture = new Texture(@"C:\Users\comte\Desktop\sprite-player.png"),
                    Size = new Vector2f(40, 40)
                }
            };
            player.AddComponent(new SampleComponent());

            var bullet = new GameObject
            {
                Collider = new BoxCollider { Height = 10, Width = 10 },
                Sprite = new RectangleShape(new Vector2f(10, 10))
                {
                    FillColor = Color.Red
                }
            };
            bullet.Transform.Position = new Vector2f(0, 110);
            bullet.AddComponent(new BulletComponent());

            var obstacle = new GameObject
            {
                Collider = new BoxCollider {Height = 40, Width = 20},
                Sprite = new RectangleShape(new Vector2f(20, 40))
                {
                    FillColor = Color.Blue
                }
            };
            obstacle.Transform.Position = new Vector2f(100, 100);


            game.World.AddGameObject(player);
            game.World.AddGameObject(bullet);
            game.World.AddGameObject(obstacle);

            game.Run();
        }
    }
}