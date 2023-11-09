using DesignPatterns_PaintApplication.CompositePattern;
using DesignPatterns_PaintApplication.Enum;
using DesignPatterns_PaintApplication.Interfaces;

namespace DesignPatterns_PaintApplication.DecoratorPattern;

public abstract class LabeledComponent : IComponent
{
    protected readonly IComponent _component;

    public LabeledComponent(IComponent component)
    {
        _component = component switch
        {
            Figure or Group or LabeledComponent => component,
            _ => throw new InvalidOperationException()

        };
    }

    protected Font DrawFont = new("Arial", 12);
    protected SolidBrush SolidBrush = new(Color.Black);

    public int Id { get => _component.Id; set => _component.Id = value; }
    public string Name { get => _component.Name; set => _component.Name = value; }
    public string Text { get; set; }
    public bool Selected { get => _component.Selected; set => _component.Selected = value; }

    public ComponentType ComponentType => _component.ComponentType;
    public IComponent InnerComponent() => _component;
    public Rectangle Placement { get => _component.Placement; set => _component.Placement = value; }

    public void Accept(IVisitor visitor) => _component.Accept(visitor);
    public virtual void Draw(PaintEventArgs e, Rectangle? rectangle = null) => _component.Draw(e, rectangle);

    public abstract bool TryGetLabel(LabelDirection direction, out LabeledComponent component);
    public abstract LabelDirection Direction { get; }
}
