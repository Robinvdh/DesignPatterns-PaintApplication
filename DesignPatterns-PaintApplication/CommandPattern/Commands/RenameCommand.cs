using DesignPatterns_PaintApplication.Interfaces;

namespace DesignPatterns_PaintApplication.CommandPattern;

public class RenameCommand : ICommand
{
    private readonly IComponent _component;
    private readonly string _newName;
    private string _previousName;

    public RenameCommand(IComponent component, string newName)
    {
        _component = component;
        _newName = newName;
    }

    public void Execute()
    {
        _previousName = _component.Name;
        _component.Name = _newName;
    }

    public void Undo()
    {
        _component.Name = _previousName;
    }
}
