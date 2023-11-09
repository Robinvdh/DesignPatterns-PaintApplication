using DesignPatterns_PaintApplication.CompositePattern;
using DesignPatterns_PaintApplication.Interfaces;

namespace DesignPatterns_PaintApplication.CommandPattern;

internal class RemoveComponentCommand : ICommand
{
    private readonly Controller _controller;
    private readonly int _id;
    private IComponent _component;
    private readonly Group _parent;

    // constructor
    public RemoveComponentCommand(Controller controller, int id)
    {
        _controller = controller;
        _id = id;
        _parent = _controller.FindParentGroup(id);
    }

    public void Execute()
    {
        this._component = _controller.GetComponent(_id);
        _controller.RemoveComponent(_id);
    }

    public void Undo()
    {
        if (_component is Figure figure)
            _controller.CreateFigure(figure.Placement, figure.ObjectType, _parent?.Id);
        else
            if (_parent is not null)
            _parent.Children.Add(_component);
        else
            _controller.AddGroup(_component.InnerComponent() as Group);
    }
}