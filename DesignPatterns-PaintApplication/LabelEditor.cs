using DesignPatterns_PaintApplication.CommandPattern;
using DesignPatterns_PaintApplication.CompositePattern;
using DesignPatterns_PaintApplication.Enum;
using DesignPatterns_PaintApplication.Interfaces;

namespace DesignPatterns_PaintApplication;

internal class LabelEditor : Form
{
    private readonly IComponent _originalComponent;
    private readonly Group _parent;
    private readonly Invoker _invoker;

    public ComboBox DirectionSelector { get; set; }
    public TextBox TextBox { get; set; }
    public Button SubmitButton { get; set; }

    public LabelEditor(IComponent originalComponent, Group parent, Invoker invoker)
    {
        _originalComponent = originalComponent;
        _parent = parent;
        _invoker = invoker;

        Text = "Bijschrift toevoegen";

        DirectionSelector = new ComboBox { Text = "selecteer", Left = 50, Top = 20 };
        DirectionSelector.Items.Add(LabelDirection.Left.ToString());
        DirectionSelector.Items.Add(LabelDirection.Right.ToString());
        DirectionSelector.Items.Add(LabelDirection.Top.ToString());
        DirectionSelector.Items.Add(LabelDirection.Bottom.ToString());

        TextBox = new TextBox { Text = "Bijschrift", Left = 50, Top = 50 };

        SubmitButton = new Button() { Text = "Toevoegen", Left = 50, Top = 80 };
        SubmitButton.Click += SubmitLabelButtonClick;

        Controls.Add(DirectionSelector);
        Controls.Add(TextBox);
        Controls.Add(SubmitButton);

        AcceptButton = SubmitButton;
    }

    private void SubmitLabelButtonClick(object? sender, EventArgs e)
    {
        LabelDirection dir = (LabelDirection)DirectionSelector.SelectedIndex;
        SetLabelCommand cmd = new(TextBox.Text, dir, _originalComponent, _parent);
        _invoker.SetCommand(cmd);
        _invoker.Execute();

        Close();
    }
}