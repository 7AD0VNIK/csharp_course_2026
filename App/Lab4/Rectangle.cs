using System.Drawing;

namespace App.Lab4;

public class Rectangle : IGeometry
{
    public double Height { get; }
    public double Width { get; }
    public int VertexCount => 4;

    public Rectangle(
        double width, 
        double height)
    {
        if (width < 0)
            throw new ArgumentOutOfRangeException(nameof(width));
        if (height < 0)
            throw new ArgumentOutOfRangeException(nameof(height));
        
        Height = height;
        Width = width;
    }
    
    public double CalculateArea() => Width * Height;
}