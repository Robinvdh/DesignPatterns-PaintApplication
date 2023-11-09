using DesignPatterns_PaintApplication.Enum;
using DesignPatterns_PaintApplication.Interfaces;

namespace DesignPatterns_PaintApplication.CommandPattern;

class NewFigureCommand : ICommand
{
    private Controller _controller;
    private Rectangle _rectangle;
    private ObjectType _objectType;
    private readonly int? _parentGroupId;
    private int _id;

    // constructor
    public NewFigureCommand(Controller controller, Rectangle rectangle, ObjectType objectType,
        int? parentGroupId)
    {
        this._controller = controller;
        this._rectangle = rectangle;
        this._objectType = objectType;
        _parentGroupId = parentGroupId;
    }

    public void Execute()
    {
        _id = _controller.CreateFigure(_rectangle, _objectType, _parentGroupId);
    }

    public void Undo()
    {
        _controller.RemoveComponent(_id); // verwijder object
    }
}