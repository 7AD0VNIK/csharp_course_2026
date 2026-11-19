using App.Lab4;

namespace AppTests.Lab4;

public class LoggerTests
{
    [Test]
    public void LogGeometry_WithDifferentTypes_ReturnsExpectedNames()
    {
        var logger = new Logger();

        var triangle = new Triangle(
            new Vertex(0, 0),
            new Vertex(1, 0),
            new Vertex(0, 1));

        Assert.Multiple(() =>
        {
            Assert.That(
                logger.LogGeometry(triangle),
                Is.EqualTo("треугольник"));

            Assert.That(
                logger.LogGeometry(new Rectangle(2, 3)),
                Is.EqualTo("прямоугольник"));

            Assert.That(
                logger.LogGeometry(new Square(2)),
                Is.EqualTo("квадрат"));

            Assert.That(
                logger.LogGeometry(new UnknownGeometry()),
                Is.EqualTo("неизвестная геометрия"));
        });
    }

    private sealed class UnknownGeometry : IGeometry
    {
        public int VertexCount => 0;

        public double CalculateArea() => 0;
    }
}