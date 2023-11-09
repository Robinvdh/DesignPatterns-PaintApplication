namespace DesignPatterns_PaintApplication.Interfaces;

public interface IFigureStrategy
{
    void Draw(PaintEventArgs paintEventArgs, bool selected, Rectangle rectangle);
}
