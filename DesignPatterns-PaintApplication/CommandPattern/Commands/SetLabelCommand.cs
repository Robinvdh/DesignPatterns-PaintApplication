using DesignPatterns_PaintApplication.CompositePattern;
using DesignPatterns_PaintApplication.DecoratorPattern;
using DesignPatterns_PaintApplication.Enum;
using DesignPatterns_PaintApplication.Interfaces;

namespace DesignPatterns_PaintApplication.CommandPattern;

internal class SetLabelCommand : ICommand
{
    private readonly bool _hasExistingLabelInDirection;
    private readonly string _originalText;
    private readonly string _text;
    private readonly LabelDirection _direction;
    private readonly IComponent _originalComponent;
    private readonly Group _parent;

    public SetLabelCommand(string text, LabelDirection direction, IComponent originalComponent, Group parent)
    {
        _text = text;
        _direction = direction;
        if (originalComponent is LabeledComponent labeledComponent &&
            labeledComponent.TryGetLabel(_direction, out var originalLabel))
        {
            _hasExistingLabelInDirection = true;
            _originalText = originalLabel.Text;
            _originalComponent = (IComponent?)originalLabel;
        }
        else
        {
            _originalComponent = originalComponent;
        }
        _parent = parent;
    }

    public void Execute()
    {
        if (_hasExistingLabelInDirection)
        {
            (_originalComponent as LabeledComponent)!.Text = _text;
        }
        else
        {
            LabeledComponent newLabel = _direction switch
            {
                LabelDirection.Left => new LeftLabeledComponent(_originalComponent),
                LabelDirection.Right => new RightLabeledComponent(_originalComponent),
                LabelDirection.Top => new TopLabeledComponent(_originalComponent),
                LabelDirection.Bottom => new BottomLabeledComponent(_originalComponent),
                _ => throw new ArgumentOutOfRangeException()
            };
            newLabel.Text = _text;

            if (_parent.Children.Remove(_originalComponent))
                _parent.Children.Add((IComponent)newLabel);
        }
    }

    public void Undo()
    {
        var fromParent = _parent.Children.First(c => c.Id == _originalComponent.Id);

        if (_hasExistingLabelInDirection)
        {
            (_originalComponent as LabeledComponent)!.Text = _originalText;
        }
        else
        {
            if (_parent.Children.Remove(fromParent))
                _parent.Children.Add(_originalComponent);
        }
    }
}
