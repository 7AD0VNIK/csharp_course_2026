using App.Lab4;

namespace AppTests.Lab4;

public class TriangleTests
{
    [TestCase(0, 0, 4, 0, 0, 3, 6)]
    [TestCase(0, 0, 0, 3, 4, 0, 6)]
    [TestCase(1, 1, 2, 2, 3, 3, 0)]
    public void CalculateArea_WithVertices_ReturnsExpectedArea(
        double firstX,
        double firstY,
        double secondX,
        double secondY,
        double thirdX,
        double thirdY,
        double expectedArea)
    {
        var triangle = new Triangle(
            new Vertex(firstX, firstY),
            new Vertex(secondX, secondY),
            new Vertex(thirdX, thirdY));

        Assert.Multiple(() =>
        {
            Assert.That(
                triangle.CalculateArea(),
                Is.EqualTo(expectedArea).Within(1e-9));

            Assert.That(triangle.VertexCount, Is.EqualTo(3));
        });
    }
}