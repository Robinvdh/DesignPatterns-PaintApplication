using DesignPatterns_PaintApplication.CompositePattern;
using DesignPatterns_PaintApplication.Interfaces;
using DesignPatterns_PaintApplication.VisitorPattern;

namespace DesignPatterns_PaintApplication.CommandPattern;

internal class MoveFigureCommand : ICommand
{
    private readonly int _moveX;
    private readonly int _moveY;
    private readonly Figure _figure;

    public MoveFigureCommand(Controller controller, int moveX, int moveY, int id)
    {
        _moveX = moveX;
        _moveY = moveY;
        _figure = controller.GetComponent(id).InnerComponent() as Figure;
    }

    public void Execute()
    {
        MoveVisitor visitor = new(_moveX, _moveY);
        _figure.Accept(visitor);
    }

    public void Undo()
    {
        MoveVisitor visitor = new(-_moveX, -_moveY);
        _figure.Accept(visitor);
    }
}