using System.Numerics;
using FizX.Core.Geometry;
using FizX.Core.Geometry.Shapes;
using FluentAssertions;
using Xunit;

namespace FizX.Core.Test.Geometry.Shapes
{
    public class RectangleShape_Test
    {
        // [Theory]
        // // todo
        // public void GetBoundingBox(float height, float width, float positionX, float positionY, float rotation, float expectedMinX, float expectedMaxX, float expectedMinY, float expectedMaxY)
        // {
        //     var rectangle = new RectangleShape(height, width);
        //
        //     var aabb = rectangle.GetBoundingBox(new Transform(new Vector2(positionX, positionY), rotation));
        //
        //     aabb.MinX.Should().Be(expectedMinX);
        //     aabb.MaxX.Should().Be(expectedMaxX);
        //     aabb.MinY.Should().Be(expectedMinY);
        //     aabb.MaxY.Should().Be(expectedMaxY);
        // }
    }
}