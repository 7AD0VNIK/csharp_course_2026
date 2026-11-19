#nullable enable

namespace App.Lab4;

public class ClockwiseComparer : IComparer<I2DVertex>
{
    private readonly double centerX;
    private readonly double centerY;

    public ClockwiseComparer(double centerX = 0, double centerY = 0)
    {
        this.centerX = centerX;
        this.centerY = centerY;
    }

    public int Compare(I2DVertex? first, I2DVertex? second)
    {
        if (first is null)
            return second is null ? 0 : -1;

        if (second is null)
            return 1;
        
        return GetAngle(first).CompareTo(GetAngle(second));
    }

    private double GetAngle(I2DVertex vertex)
    {
        var angle = Math.Atan2(vertex.Y - centerY, vertex.X - centerX);
        return angle < 0 ? angle + 2 * Math.PI : angle;
    }
}