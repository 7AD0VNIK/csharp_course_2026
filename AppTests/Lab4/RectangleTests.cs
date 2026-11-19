using App.Lab4;

namespace AppTests.Lab4;

public class RectangleTests
{
    [TestCase(3, 4, 12)]
    [TestCase(2.5, 4, 10)]
    [TestCase(0, 4, 0)]
    public void CalculateArea_WithDimensions_ReturnsExpectedArea(
        double width,
        double height,
        double expectedArea)
    {
        var rectangle = new Rectangle(width, height);

        Assert.Multiple(() =>
        {
            Assert.That(
                rectangle.CalculateArea(),
                Is.EqualTo(expectedArea).Within(1e-9));

            Assert.That(rectangle.VertexCount, Is.EqualTo(4));
        });
    }
}