using App.Lab4;

namespace AppTests.Lab4;

public class SquareTests
{
    [TestCase(3, 9)]
    [TestCase(2.5, 6.25)]
    [TestCase(0, 0)]
    public void CalculateArea_WithSide_ReturnsSideSquared(
        double side,
        double expectedArea)
    {
        var square = new Square(side);

        Assert.Multiple(() =>
        {
            Assert.That(
                square.CalculateArea(),
                Is.EqualTo(expectedArea).Within(1e-9));

            Assert.That(square.VertexCount, Is.EqualTo(4));
        });
    }
}