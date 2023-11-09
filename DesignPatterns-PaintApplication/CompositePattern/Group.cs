using DesignPatterns_PaintApplication.Enum;
using DesignPatterns_PaintApplication.Interfaces;
using System.Drawing.Drawing2D;

namespace DesignPatterns_PaintApplication.CompositePattern;

public class Group : IComponent
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ComponentType ComponentType => ComponentType.Group;
    public bool Selected { get; set; }
    public List<IComponent> Children = new();

    public IEnumerable<IComponent> Figures => Children
      .Where(c => c.ComponentType == ComponentType.Figure);

    public IEnumerable<IComponent> Groups => Children
      .Where(c => c.ComponentType == ComponentType.Group);

    public Rectangle Placement { get => GetRectangle(); set => throw new InvalidOperationException(); }

    private Rectangle GetRectangle()
    {
        var figures = AllFiguresFlattened().ToArray();

        if (!figures.Any())
            return Rectangle.Empty;

        var X = figures.Min(f => f.Placement.X);
        var Y = figures.Min(f => f.Placement.Y);
        var Width = figures.Max(f => f.Placement.X + f.Placement.Width) - X;
        var Heigth = figures.Max(f => f.Placement.Y + f.Placement.Height) - Y;

        return new Rectangle(X, Y, Width, Heigth);
    }

    public void Accept(IVisitor visitor)
    {
        visitor.VisitGroup(this);
    }

    public IEnumerable<IComponent> AllFiguresFlattened() =>
                Figures.Concat(Groups.Select(c => c.InnerComponent() as Group)
                               .SelectMany(g => g.AllFiguresFlattened()));

    public void Draw(PaintEventArgs paintEventArgs, Rectangle? rectangle = null)
    {
        var pen = new Pen(Color.Green);
        pen.DashStyle = DashStyle.Dot;
        paintEventArgs.Graphics.DrawRectangle(pen, rectangle ?? Placement);

        foreach (var component in Children)
        {
            component.Draw(paintEventArgs, rectangle);
        }
    }

    public IComponent InnerComponent() => this;
}
