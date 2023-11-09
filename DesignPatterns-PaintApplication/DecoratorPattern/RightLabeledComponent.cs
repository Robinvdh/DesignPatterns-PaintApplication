using DesignPatterns_PaintApplication.Enum;
using DesignPatterns_PaintApplication.Interfaces;

namespace DesignPatterns_PaintApplication.DecoratorPattern;

internal class RightLabeledComponent : LabeledComponent
{
    public RightLabeledComponent(IComponent component) : base(component)
    {
    }

    public override LabelDirection Direction => LabelDirection.Right;

    public override void Draw(PaintEventArgs e, Rectangle? rectangle = null)
    {
        base.Draw(e, rectangle);

        PointF labelPoint = new(Placement.X + Placement.Width + 5, Placement.Y + Placement.Height / 2);
        e.Graphics.DrawString(Text, DrawFont, SolidBrush, labelPoint);
    }

    public override bool TryGetLabel(LabelDirection direction, out LabeledComponent component)
    {
        if (direction == LabelDirection.Right)
        {
            component = this;
            return true;
        }

        component = null;
        return _component is LabeledComponent labeledComponent
                                && labeledComponent.TryGetLabel(direction, out component);
    }
}