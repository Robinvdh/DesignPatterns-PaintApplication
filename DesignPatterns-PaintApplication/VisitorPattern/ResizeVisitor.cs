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
        var figures = group.AllFiguresFlattened().ToArray();

        var newGroupWidth = _newPosition.X - group.Placement.X;
        var ratio = (float)newGroupWidth / group.Placement.Width;

        foreach (var figure in figures)
        {
            var originalRelativeX = figure.Placement.X - group.Placement.X;
            var originalRelativeY = figure.Placement.Y - group.Placement.Y;

            var newRelativeX = (int)Math.Floor(originalRelativeX * ratio);
            var newRelativeY = (int)Math.Floor(originalRelativeY * ratio);

            var newX = group.Placement.X + newRelativeX;
            var newY = group.Placement.Y + newRelativeY;

            var newWidth = (int)Math.Floor(figure.Placement.Width * ratio);
            var newHeight = (int)Math.Floor(figure.Placement.Height * ratio);

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
