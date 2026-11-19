namespace App.Lab4;

public class Triangle : IGeometry
{
    private readonly I2DVertex first;
    private readonly I2DVertex second;
    private readonly I2DVertex third;

    public int VertexCount => 3;

    public Triangle(I2DVertex first, I2DVertex second, I2DVertex third)
    {
        this.first = first;
        this.second = second;
        this.third = third;
    }

    public double CalculateArea()
    {
        var secondX = second.X - first.X;
        var secondY = second.Y - first.Y;
        var thirdX = third.X - first.X;
        var thirdY = third.Y - first.Y;
        
        return Math.Abs(secondX * thirdY - secondY * thirdX) / 2;
    }
}