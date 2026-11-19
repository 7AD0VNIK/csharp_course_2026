using App.Lab4;

namespace AppTests.Lab4;

public class VertexTests
{
    [TestCase(1, -2.5, "(x = 1.000, y = -2.500)")]
    [TestCase(1.23456, 0, "(x = 1.235, y = 0.000)")]
    [SetCulture("ru-RU")]
    public void ToString_WithCoordinates_ReturnsThreeDecimalPlaces(
        double x,
        double y,
        string expected)
    {
        var vertex = new Vertex(x, y);

        Assert.That(vertex.ToString(), Is.EqualTo(expected));
    }
}