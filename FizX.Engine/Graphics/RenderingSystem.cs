using System;
using System.Collections.Generic;
using System.Text;
using FizX.Engine;
using FizX.Engine.Physics.Collisions;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace FizX.Graphics
{
    internal class RenderingSystem
    {
        public RenderWindow Window { get; private set; }

        public RenderingSystem(uint width, uint height, string windowName)
        {
            Window = new RenderWindow(new VideoMode(width, height), windowName);
        }

        public void Render(World world)
        {
            Window.DispatchEvents();
            Window.Clear();

            foreach (var go in world.GameObjects)
            {
                if (go.Sprite == null) continue;

                go.Sprite.Position = go.Transform.Position;
                Window.Draw(go.Sprite);

                var center = new CircleShape(2);
                center.FillColor = Color.Red;
                center.Position = go.Transform.Position;
                Window.Draw(center);

                var collider = go.Collider as BoxCollider;
                var colliderShape = new RectangleShape(new Vector2f(collider.Width, collider.Height));
                colliderShape.Position = go.Transform.Position;
                colliderShape.FillColor = Color.Transparent;
                colliderShape.OutlineColor = Color.Green;
                colliderShape.OutlineThickness = 1f;

                Window.Draw(colliderShape);

            }

            Window.Display();
        }
    }
}
