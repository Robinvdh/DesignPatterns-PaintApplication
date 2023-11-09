using DesignPatterns_PaintApplication.Enum;
using DesignPatterns_PaintApplication.Interfaces;

namespace DesignPatterns_PaintApplication.DecoratorPattern;

public class TopLabeledComponent : LabeledComponent
{
    public TopLabeledComponent(IComponent component) : base(component)
    {
    }

    public override LabelDirection Direction => LabelDirection.Top;

    public override void Draw(PaintEventArgs e, Rectangle? rectangle = null)
    {
        base.Draw(e, rectangle);

        PointF labelPoint = new(Placement.X, Placement.Y - 25);
        e.Graphics.DrawString(Text, DrawFont, SolidBrush, labelPoint);
    }

    public override bool TryGetLabel(LabelDirection direction, out LabeledComponent component)
    {

        if (direction == LabelDirection.Top)
        {
            component = this;
            return true;
        }

        component = null;
        return _component is LabeledComponent labeledComponent
                                && labeledComponent.TryGetLabel(direction, out component);
    }
}
