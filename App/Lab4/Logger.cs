namespace App.Lab4;

public class Logger
{
    public string LogGeometry(IGeometry geometry)
    {
        if (geometry is Triangle)
            return "треугольник";
        
        if (geometry is Square)
            return "квадрат";
        
        if (geometry is Rectangle)
            return "прямоугольник";
        
        return "неизвестная геометрия";
    }
}