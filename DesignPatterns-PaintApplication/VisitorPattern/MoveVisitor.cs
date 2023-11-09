using DesignPatterns_PaintApplication.CompositePattern;
using DesignPatterns_PaintApplication.Interfaces;

namespace DesignPatterns_PaintApplication.VisitorPattern;

public class MoveVisitor : IVisitor
{
    private readonly int _moveX;
    private readonly int _moveY;

    public MoveVisitor(int moveX, int moveY)
    {
        _moveX = moveX;
        _moveY = moveY;
    }

    public void VisitFigure(Figure figure)
    {
        int newX = figure.Placement.X + _moveX;
        int newY = figure.Placement.Y + _moveY;
        figure.Placement = new(newX, newY, figure.Placement.Width, figure.Placement.Height);
    }

    public void VisitGroup(Group group)
    {
        MoveAllFiguresInGroupRecursive(group);
    }

    private void MoveAllFiguresInGroupRecursive(Group group)
    {
        foreach (IComponent figure in group.Figures)
        {
            figure.Placement = new Rectangle
            {
                X = figure.Placement.X + _moveX,
                Y = figure.Placement.Y + _moveY,
                Height = figure.Placement.Height,
                Width = figure.Placement.Width
            };
        }

        foreach (Group? subGroup in group.Groups.Select(c => c.InnerComponent() as Group))
        {
            if (subGroup == null)
            {
                break;
            }

            MoveAllFiguresInGroupRecursive(subGroup);
        }
    }
}