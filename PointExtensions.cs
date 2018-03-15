using System.Drawing;

namespace Sample.DeconstructorsForNonTuple
{
    public static class PointExtensions
    {
        public static void Deconstruct(this Point point, out int x, out int y)
        {
            x = point.X;
            y = point.Y;
        }
    }
}