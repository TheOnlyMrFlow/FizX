using System.Numerics;
using FizX.Core.Geometry;
using FizX.Core.Geometry.Shapes;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Xunit;

namespace FizX.Core.Test.Geometry.Shapes
{
    public class CircleShape_Test
    {
        [Theory]
        [InlineData(3f, 1, 0, -2f, 4f, -3f, 3f)]
        [InlineData(0f, -12f, 15.7f, -12f, -12f, 15.7f, 15.7f)]
        [InlineData(4.56f, 0, 1, -4.56f, 4.56f, -3.56f, 5.56f)]
        public void GetBoundingBox(float radius, float positionX, float positionY, float expectedMinX, float expectedMaxX, float expectedMinY, float expectedMaxY)
        {
            var circle = new CircleShape(radius);

            var aabb = circle.GetBoundingBox(new Transform(new Vector2(positionX, positionY)));

            aabb.MinX.Should().Be(expectedMinX);
            aabb.MaxX.Should().Be(expectedMaxX);
            aabb.MinY.Should().Be(expectedMinY);
            aabb.MaxY.Should().Be(expectedMaxY);
        }
    }
}