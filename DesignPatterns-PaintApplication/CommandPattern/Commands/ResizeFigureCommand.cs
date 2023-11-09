using DesignPatterns_PaintApplication.CompositePattern;
using DesignPatterns_PaintApplication.Interfaces;
using DesignPatterns_PaintApplication.VisitorPattern;

namespace DesignPatterns_PaintApplication.CommandPattern;

internal class ResizeFigureCommand : ICommand
{
    private readonly Figure _figure;
    private readonly Point _newPosition;
    private readonly Rectangle _oldPlacement;

    public ResizeFigureCommand(Figure figure, Point newPosition)
    {
        _figure = figure;
        _oldPlacement = figure.Placement;
        _newPosition = newPosition;
    }

    public void Execute()
    {
        var visitor = new ResizeVisitor(_newPosition);
        _figure.Accept(visitor);
    }

    public void Undo()
    {
        _figure.Placement = _oldPlacement;
    }
}
