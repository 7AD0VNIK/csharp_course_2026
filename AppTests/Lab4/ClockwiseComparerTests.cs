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
    
    [Test]
    public void Sort_WithShiftedCenter_OrdersPointsCounterclockwise()
    {
        var right = new Vertex(3, 3);
        var top = new Vertex(2, 4);
        var left = new Vertex(1, 3);
        var bottom = new Vertex(2, 2);

        var points = new List<I2DVertex>
        {
            bottom,
            left,
            top,
            right
        };

        points.Sort(new ClockwiseComparer(2, 3));

        Assert.That(
            points,
            Is.EqualTo(new[] { right, top, left, bottom }));
    }
}