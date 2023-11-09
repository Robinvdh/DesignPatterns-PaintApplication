using DesignPatterns_PaintApplication.Enum;
using DesignPatterns_PaintApplication.Interfaces;
using DesignPatterns_PaintApplication.StrategyPattern;

namespace DesignPatterns_PaintApplication.CompositePattern;

public class Figure : IComponent
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Selected { get; set; }

    public ComponentType ComponentType => ComponentType.Figure;
    public Rectangle Placement { get; set; }
    public ObjectType ObjectType { get; set; }

    public IFigureStrategy Strategy => ObjectType switch
    {
        ObjectType.Ellipse => Strategies.EllipseStrategy,
        ObjectType.Rectangle => Strategies.RectangleStrategy,
        _ => throw new System.NotImplementedException()
    };

    public void Draw(PaintEventArgs e, Rectangle? preview = null) => Strategy.Draw(e, Selected, preview ?? Placement);
    public IComponent InnerComponent() => this;

    public void Accept(IVisitor visitor)
    {
        visitor.VisitFigure(this);
    }
}
