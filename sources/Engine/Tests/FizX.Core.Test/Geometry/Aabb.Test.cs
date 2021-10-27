using System.Linq;
using FizX.Core.Geometry;
using FluentAssertions;
using Xunit;

namespace FizX.Core.Test.Geometry
{
    public class Aabb_Test
    {
        [Theory]
        [InlineData(0, 1, 0, 1, 2, 3, 2, 3, false)]
        [InlineData(-1, 0, 1, 9, 0.1f, 4, 3, 6, false)]
        [InlineData(-1, 0, 1, 9, -0.5f, 4, 3, 6, true)]
        [InlineData(0, 1, 0, 1, 0, 1, 0, 1, true)]
        public void Overlap(float minXA, float maxXA, float minYA, float maxYA, float minXB, float maxXB, float minYB, float maxYB, bool expectToOverlap)
        {
            var a = new Aabb(minXA, maxXA, minYA, maxYA);
            var b = new Aabb(minXB, maxXB, minYB, maxYB);

            var overlaps = a.Overlaps(b);

            overlaps.Should().Be(expectToOverlap);
        }
    }
}
