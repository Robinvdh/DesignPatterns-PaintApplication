using DesignPatterns_PaintApplication.CompositePattern;
using DesignPatterns_PaintApplication.Interfaces;
using DesignPatterns_PaintApplication.VisitorPattern;
using Newtonsoft.Json;

namespace DesignPatterns_PaintApplication.CommandPattern;

internal class ResizeGroupCommand : ICommand
{
    private readonly string _original;
    private readonly int? _parentGroupId;
    private readonly int _groupId;
    private readonly Controller _controller;
    private readonly Point _newPosition;

    public ResizeGroupCommand(int groupId, Controller controller, Point newPosition)
    {
        var group = controller.GetGroup(groupId);
        _original = JsonConvert.SerializeObject(group, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
        _parentGroupId = controller.FindParentGroup(groupId)?.Id;
        _groupId = groupId;
        _controller = controller;
        _newPosition = newPosition;
    }

    public void Execute()
    {
        var visitor = new ResizeVisitor(_newPosition);
        var group = _controller.GetGroup(_groupId);
        group.Accept(visitor);
    }

    public void Undo()
    {
        var group = JsonConvert.DeserializeObject<Group>(_original, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });

        if (_parentGroupId.HasValue)
        {
            var parent = _controller.GetGroup(_parentGroupId.Value);
            parent.Children.Remove(parent.Groups.First(g => g.Id == _groupId));
            parent.Children.Add(group);
        }
        else
        {
            _controller.RemoveComponent(_groupId);
            _controller.AddGroup(group);
        }
    }
}
