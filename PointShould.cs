using System.Drawing;
using FluentAssertions;
using Xunit;

namespace Sample.DeconstructorsForNonTuple
{
    public class PointShould
    {
        [Fact]
        public void BeDeconstructedToXAndY()
        {
            Point point = new Point(45, 85);

            var (x, y) = point;

            x.Should().Be(45);
            y.Should().Be(85);
        }
    }
}