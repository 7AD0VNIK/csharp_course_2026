namespace App.Lab4;

public class Vertex : I2DVertex
{
    public double X { get; set; }
    public double Y { get; set; }

    public Vertex(double x, double y)
    {
        X = x;
        Y = y;
    }
    
    public override string ToString()
    {
        return FormattableString.Invariant($"(x = {X:F3}, y = {Y:F3})");
    }
}