using DesignPatterns_PaintApplication.Enum;

namespace DesignPatterns_PaintApplication.Interfaces;

public interface IComponent
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Selected { get; set; }

    public ComponentType ComponentType { get; }
    public Rectangle Placement { get; set; }

    void Accept(IVisitor visitor);
    void Draw(PaintEventArgs paintEventArgs, Rectangle? rectangle = null);

    public IComponent InnerComponent();
}
