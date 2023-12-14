using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using FizX.Core.Geometry;
using FizX.Core.Geometry.Shapes;
using FluentAssertions;
using Xunit;

namespace FizX.Core.Test.Geometry.Shapes;

public class RectangleShape_Test
{
    [Theory]
    [InlineData(3f, 2f, 4f, -2f, 0f, 2.5f, 5.5f, -3f, -1f)]
    [InlineData(5f, 3f, -6f, 7f, 2.6f, 2.5f, 5.5f, -3f, -1f)]
    public void GetBoundingBox(float width, float height, float positionX, float positionY, float rotation, float expectedMinX, float expectedMaxX, float expectedMinY, float expectedMaxY)
    {
        var rectangle = new RectangleShape(width, height);
        
        var aabb = rectangle.GetBoundingBox(new Transform(new Vector2(positionX, positionY), rotation));
        
        aabb.MinX.Should().Be(expectedMinX);
        aabb.MaxX.Should().Be(expectedMaxX);
        aabb.MinY.Should().Be(expectedMinY);
        aabb.MaxY.Should().Be(expectedMaxY);
    }
}