using DesignPatterns_PaintApplication.CompositePattern;
using DesignPatterns_PaintApplication.Interfaces;
using DesignPatterns_PaintApplication.VisitorPattern;

namespace DesignPatterns_PaintApplication.CommandPattern;

internal class MoveGroupCommand : ICommand
{
    private readonly int _moveX;
    private readonly int _moveY;
    private readonly Group _group;

    public MoveGroupCommand(Controller controller, int groupId, int moveX, int moveY)
    {
        _group = controller.GetGroup(groupId);
        this._moveX = moveX;
        this._moveY = moveY;
    }

    public void Execute()
    {
        var visitor = new MoveVisitor(_moveX, _moveY);
        _group.Accept(visitor);
    }

    public void Undo()
    {
        var visitor = new MoveVisitor(-_moveX, -_moveY);
        _group.Accept(visitor);
    }
}
