using DesignPatterns_PaintApplication.CompositePattern;
using DesignPatterns_PaintApplication.Interfaces;

namespace DesignPatterns_PaintApplication.VisitorPattern;

internal class ResizeVisitor : IVisitor
{
    private readonly Point _newPosition;

    public ResizeVisitor(Point newPosition)
    {
        _newPosition = newPosition;
    }

    public void VisitFigure(Figure figure)
    {
        figure.Placement = ResizeRectangle(figure.Placement);
    }

    public void VisitGroup(Group group)
    {
        IComponent[] figures = group.AllFiguresFlattened().ToArray();

        int newGroupWidth = _newPosition.X - group.Placement.X;
        float ratio = (float)newGroupWidth / group.Placement.Width;

        foreach (IComponent? figure in figures)
        {
            int originalRelativeX = figure.Placement.X - group.Placement.X;
            int originalRelativeY = figure.Placement.Y - group.Placement.Y;

            int newRelativeX = (int)Math.Floor(originalRelativeX * ratio);
            int newRelativeY = (int)Math.Floor(originalRelativeY * ratio);

            int newX = group.Placement.X + newRelativeX;
            int newY = group.Placement.Y + newRelativeY;

            int newWidth = (int)Math.Floor(figure.Placement.Width * ratio);
            int newHeight = (int)Math.Floor(figure.Placement.Height * ratio);

            figure.Placement = new Rectangle(newX, newY, newWidth, newHeight);
        }
    }

    private Rectangle ResizeRectangle(Rectangle currentRectangle)
    {
        Rectangle rectangle = new();

        rectangle.X = _newPosition.X < currentRectangle.X ? _newPosition.X : currentRectangle.X;
        rectangle.Y = _newPosition.Y < currentRectangle.Y ? _newPosition.Y : currentRectangle.Y;

        rectangle.Width = Math.Abs(_newPosition.X - currentRectangle.X);
        rectangle.Height = Math.Abs(_newPosition.Y - currentRectangle.Y);
        return rectangle;
    }
}
