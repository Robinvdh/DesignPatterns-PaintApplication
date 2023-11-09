using DesignPatterns_PaintApplication.Interfaces;

namespace DesignPatterns_PaintApplication.CommandPattern;

internal class NewGroupCommand : ICommand
{
    private readonly Controller _controller;
    private int _id;
    private readonly int? _parentGroupId;

    public NewGroupCommand(Controller controller, int? parentGroupId)
    {
        _controller = controller;
        _parentGroupId = parentGroupId;
    }

    public void Execute()
    {
        _id = _controller.CreateGroup(_parentGroupId);
    }

    public void Undo()
    {
        _controller.RemoveComponent(_id);
    }
}
