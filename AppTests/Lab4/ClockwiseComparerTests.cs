using App.Lab4;

namespace AppTests.Lab4;

public class ClockwiseComparerTests
{
    [Test]
    public void Sort_WithPointsAroundOrigin_OrdersThemCounterclockwise()
    {
        var right = new Vertex(1, 0);
        var top = new Vertex(0, 1);
        var left = new Vertex(-1, 0);
        var bottom = new Vertex(0, -1);

        var points = new List<I2DVertex>
        {
            bottom,
            left,
            top,
            right
        };

        points.Sort(new ClockwiseComparer());

        Assert.That(
            points,
            Is.EqualTo(new[] { right, top, left, bottom }));
    }

    [Test]
    public void Compare_WithSamePoint_ReturnsZero()
    {
        var point = new Vertex(1, 2);
        var comparer = new ClockwiseComparer();

        Assert.That(comparer.Compare(point, point), Is.Zero);
    }
}