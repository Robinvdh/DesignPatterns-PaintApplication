using DesignPatterns_PaintApplication.Interfaces;
using System.Drawing.Drawing2D;

namespace DesignPatterns_PaintApplication.StrategyPattern;

public class EllipseStrategy : IFigureStrategy
{
    public void Draw(PaintEventArgs paintEventArgs, bool selected, Rectangle placement)
    {
        Pen pen = GeneratePen(selected);
        paintEventArgs.Graphics.DrawEllipse(pen, placement);
    }

    private Pen GeneratePen(bool selection)
    {
        Pen pen = new Pen(Color.Black, 2);

        if (selection)
        {
            pen.DashStyle = DashStyle.Dot;
        }

        return pen;
    }
}
