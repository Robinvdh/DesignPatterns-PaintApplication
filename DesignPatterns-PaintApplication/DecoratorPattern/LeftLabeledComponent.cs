using DesignPatterns_PaintApplication.Enum;
using DesignPatterns_PaintApplication.Interfaces;

namespace DesignPatterns_PaintApplication.DecoratorPattern;

public class LeftLabeledComponent : LabeledComponent
{
    public LeftLabeledComponent(IComponent component) : base(component)
    {
    }

    public override LabelDirection Direction => LabelDirection.Left;

    public override void Draw(PaintEventArgs e, Rectangle? rectangle = null)
    {
        base.Draw(e, rectangle);

        PointF labelPoint = new(Placement.X - 9 * Text.Length, Placement.Y + Placement.Height / 2);
        e.Graphics.DrawString(Text, DrawFont, SolidBrush, labelPoint);
    }

    public override bool TryGetLabel(LabelDirection direction, out LabeledComponent component)
    {
        if (direction == LabelDirection.Left)
        {
            component = this;
            return true;
        }

        component = null;
        return _component is LabeledComponent labeledComponent
                                && labeledComponent.TryGetLabel(direction, out component);
    }
}
