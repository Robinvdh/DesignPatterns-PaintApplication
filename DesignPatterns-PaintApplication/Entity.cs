using DesignPatterns_PaintApplication.CommandPattern;
using DesignPatterns_PaintApplication.CompositePattern;
using DesignPatterns_PaintApplication.Enum;
using DesignPatterns_PaintApplication.Interfaces;
using DesignPatterns_PaintApplication.VisitorPattern;
using Newtonsoft.Json;

namespace DesignPatterns_PaintApplication;

public partial class Entity : Form
{
    private static readonly Figure _preview = new();

    private DrawingMode _currentMode;

    private Point _mouseDragStartPosition;
    private Point _mouseDragEndPosition;

    private bool _isMouseDown;
    private bool _isMoving;
    private bool _isResizing;
    private bool _isDrawing;
    private int _modifyingFigureId = -1;

    private readonly Controller _controller = new();

    private IEnumerable<IComponent> Componenten() => _controller.GetComponents();
    private readonly Invoker _invoker = new();

    private IComponent _currentComponent;

    public Entity()
    {
        InitializeComponent();
    }

    #region PaintPanel
    private void panel_Paint(object sender, PaintEventArgs e)
    {
        foreach (IComponent? component in _controller.GetComponents())
        {
            if (component is not null)
            {
                component.Draw(e);
            }
        }

        FillTreeview();

        if (_isDrawing)
        {
            _preview.ObjectType = ToFigureType(_currentMode);
            _preview.Placement = GetRectangle();
            _preview.Draw(e);
        }

        if (_isMoving)
        {
            Figure figure = _controller.GetFigure(_modifyingFigureId);
            Rectangle preview = MoveRectangle(figure.Placement);
            figure.Draw(e, preview);
        }

        if (_isResizing)
        {
            Figure figure = _controller.GetFigure(_modifyingFigureId);
            Rectangle preview = ResizeRectangle(figure.Placement);
            figure.Draw(e, preview);
        }
    }

    private void paintPanel_MouseDown(object sender, MouseEventArgs e)
    {
        _isMouseDown = true;
        _isDrawing = IsInDrawingMode() && _mouseDragStartPosition != _mouseDragEndPosition;

        _mouseDragStartPosition = e.Location;

        IEnumerable<IComponent> figures = _controller.GetAllFiguresFlattened();
        IComponent? huidigFiguur = figures.LastOrDefault(f => f.Placement.Contains(e.Location));
        if (huidigFiguur is not null)
        {
            _modifyingFigureId = huidigFiguur.Id;
        }

        switch (_currentMode)
        {
            case DrawingMode.Select:
                foreach (IComponent figuur in _controller.GetAllFiguresFlattened())
                {
                    if (figuur.Placement.Contains(_mouseDragStartPosition))
                    {
                        _isMoving = true;
                    }
                }
                break;
            case DrawingMode.Resize:
                foreach (IComponent figuur in _controller.GetAllFiguresFlattened())
                {
                    if (figuur.Placement.Contains(_mouseDragStartPosition))
                    {
                        _isResizing = true;
                    }
                }
                break;
            case DrawingMode.Rectangle:
            case DrawingMode.Ellipse:
            case DrawingMode.Delete:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void paintPanel_MouseMove(object sender, MouseEventArgs e)
    {
        if (_isMouseDown)
        {
            _mouseDragEndPosition = e.Location;
            Refresh();
        }
    }

    private void paintPanel_MouseUp(object sender, MouseEventArgs e)
    {
        _isMouseDown = false;
        _mouseDragEndPosition = e.Location;
        _isMoving = false;
        _isResizing = false;
        _isDrawing = false;

        int? selectedGroupId = _controller.SelectedGroupId();

        switch (_currentMode)
        {
            case DrawingMode.Select:
                if (_modifyingFigureId >= 0)
                {
                    if (_mouseDragEndPosition == _mouseDragStartPosition)
                    {
                        _controller.SelectFigure(_modifyingFigureId);
                    }
                    else
                    {
                        if (selectedGroupId.HasValue)
                        {
                            int moveX = _mouseDragEndPosition.X - _mouseDragStartPosition.X;
                            int moveY = _mouseDragEndPosition.Y - _mouseDragStartPosition.Y;
                            _invoker.SetCommand(new MoveGroupCommand(_controller, selectedGroupId.Value, moveX, moveY));
                        }
                        else
                        {
                            int moveRight = _mouseDragEndPosition.X - _mouseDragStartPosition.X;
                            int moveDown = _mouseDragEndPosition.Y - _mouseDragStartPosition.Y;
                            _invoker.SetCommand(new MoveFigureCommand(_controller, moveRight, moveDown, _modifyingFigureId));
                        }


                    }
                }
                break;
            case DrawingMode.Resize:
                if (_mouseDragEndPosition != _mouseDragStartPosition)
                {
                    if (selectedGroupId.HasValue)
                    {
                        _invoker.SetCommand(new ResizeGroupCommand(selectedGroupId.Value, _controller, _mouseDragEndPosition));
                    }
                    else if (_modifyingFigureId >= 0)
                    {
                        Figure figure = _controller.GetFigure(_modifyingFigureId);
                        _invoker.SetCommand(new ResizeFigureCommand(figure, _mouseDragEndPosition));
                    }
                }
                break;
            case DrawingMode.Rectangle:
            case DrawingMode.Ellipse:
                if (_mouseDragEndPosition != _mouseDragStartPosition)
                {
                    _invoker.SetCommand(new NewFigureCommand(_controller, GetRectangle(),
                        ToFigureType(_currentMode), selectedGroupId));
                }

                break;
            case DrawingMode.Delete:
                if (_modifyingFigureId >= 0 && _mouseDragEndPosition == _mouseDragStartPosition)
                {
                    _invoker.SetCommand(new RemoveComponentCommand(_controller, _modifyingFigureId));
                }

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (_invoker.HasCommand)
        {
            _invoker.Execute();
        }

        _modifyingFigureId = -1;

        Refresh();
    }
    #endregion

    #region  ToolStrip File Clicks
    private void btnNewFile_Click(object sender, EventArgs e)
    {
        CheckForUnsavedChanges();

        _controller.ResetComponents();
        Refresh();
    }

    private void btnOpenFile_Click(object sender, EventArgs e)
    {
        CheckForUnsavedChanges();

        OpenFileDialog dialog = new();
        dialog.Filter = "JSON files (*.json)|*.json";

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            string json = File.ReadAllText(dialog.FileName);
            Group? Group = JsonConvert.DeserializeObject<Group>(json, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
            _controller.ParentGroup = Group;
        }

        Refresh();
    }

    private void btnSaveFile_Click(object? sender, EventArgs? e)
    {
        SaveFileDialog saveFileDialog = new()
        {
            Filter = "JSON files (*.json)|*.json"
        };

        if (saveFileDialog.ShowDialog() != DialogResult.OK)
        {
            return;
        }

        SaveVisitor visitor = new(saveFileDialog.FileName);
        _controller.ParentGroup.Accept(visitor);
    }
    #endregion

    #region ToolStrip Menu Edit Clicks
    private void btnUndoToolStripMenuItem_Click(object sender, EventArgs e)
    {
        _invoker.Undo();
        Refresh();
        FillTreeview();
    }

    private void btnRedoToolStripMenuItem_Click(object sender, EventArgs e)
    {
        _invoker.Redo();
        Refresh();
        FillTreeview();
    }
    #endregion

    #region ToolStrip Help Clicks
    private void btnAboutUsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        About about = new();
        about.ShowDialog();
    }
    #endregion

    #region Toolbar Buttons Clicks
    private void btnSelect_Click(object sender, EventArgs e)
    {
        _currentMode = DrawingMode.Select;
        Cursor = Cursors.NoMove2D;
    }

    private void btnResize_Click(object sender, EventArgs e)
    {
        _currentMode = DrawingMode.Resize;
        Cursor = Cursors.SizeNWSE;
    }

    private void btnEllipse_Click(object sender, EventArgs e)
    {
        _currentMode = DrawingMode.Ellipse;
        Cursor = Cursors.Cross;
    }

    private void btnRectangle_Click(object sender, EventArgs e)
    {
        _currentMode = DrawingMode.Rectangle;
        Cursor = Cursors.Cross;
    }

    private void btnRemove_Click(object sender, EventArgs e)
    {
        _currentMode = DrawingMode.Delete;
        Cursor = Cursors.No;
    }
    #endregion

    #region Group Button Clicks
    private void btnAddGroup_Click(object sender, EventArgs e)
    {
        int? selectedGroupId = null;
        if (treeView.SelectedNode?.Tag is Group)
        {
            selectedGroupId = ((IComponent)treeView.SelectedNode.Tag).Id;
        }

        _invoker.SetCommand(new NewGroupCommand(_controller, selectedGroupId));
        _invoker.Execute();
        FillTreeview();
    }
    #endregion

    #region Groups
    private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
    {
        return;
    }

    private void treeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
    {
        IComponent component;

        if (e.Node?.Tag is not null && !string.IsNullOrWhiteSpace(e.Label))
        {
            component = e.Node.Tag as IComponent;

            _invoker.SetCommand(new RenameCommand(component, e.Label));
            _invoker.Execute();

            FillTreeview();
        }
        else
        {
            e.CancelEdit = true;
        }
    }

    private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
    {
        _currentComponent = e.Node.Tag as IComponent;
        TreeView? treeView = sender as TreeView;
        treeView.SelectedNode = e.Node;

        switch (e.Button)
        {
            case MouseButtons.Right:
                {
                    ContextMenuStrip menu = new();
                    menu.Items.Add("Verwijderen", null, DeleteContextMenuItemClick);

                    if (e.Node.Tag is Group)
                    {
                        menu.Items.Add("Group toevoegen", null, AddChildGroupMenuItemClick);
                    }

                    menu.Items.Add("Bijschriften", null, LabelsMenuItemClick);

                    menu.Show(treeView, e.Location);
                    break;
                }
            case MouseButtons.Left:
                _controller.ClearSelection();
                _currentComponent.Selected = true;
                if (_currentComponent is Group Group)
                {
                    _controller.SelectGroupRecursive(Group);
                }

                Refresh();
                FillTreeview();
                break;
            default:
                break;
        }
    }
    #endregion

    #region Private Functions
    private Rectangle GetRectangle()
    {
        Rectangle rectangle = new();
        rectangle.X = Math.Min(_mouseDragStartPosition.X, _mouseDragEndPosition.X);
        rectangle.Y = Math.Min(_mouseDragStartPosition.Y, _mouseDragEndPosition.Y);
        rectangle.Width = Math.Abs(_mouseDragStartPosition.X - _mouseDragEndPosition.X);
        rectangle.Height = Math.Abs(_mouseDragStartPosition.Y - _mouseDragEndPosition.Y);
        return rectangle;
    }

    private Rectangle MoveRectangle(Rectangle rectangle)
    {
        int moveRight = _mouseDragEndPosition.X - _mouseDragStartPosition.X;
        rectangle.X += moveRight;
        int moveDown = _mouseDragEndPosition.Y - _mouseDragStartPosition.Y;
        rectangle.Y += moveDown;
        return rectangle;
    }

    private Rectangle ResizeRectangle(Rectangle currentRectangle)
    {
        Rectangle rectangle = new();

        rectangle.X = _mouseDragEndPosition.X < currentRectangle.X ? _mouseDragEndPosition.X : currentRectangle.X;
        rectangle.Y = _mouseDragEndPosition.Y < currentRectangle.Y ? _mouseDragEndPosition.Y : currentRectangle.Y;

        rectangle.Width = Math.Abs(_mouseDragEndPosition.X - currentRectangle.X);
        rectangle.Height = Math.Abs(_mouseDragEndPosition.Y - currentRectangle.Y);
        return rectangle;
    }


    private static ObjectType ToFigureType(DrawingMode modus)
    {
        return modus switch
        {
            DrawingMode.Rectangle => ObjectType.Rectangle,
            DrawingMode.Ellipse => ObjectType.Ellipse,
            _ => throw new ArgumentOutOfRangeException(nameof(modus), modus, null)
        };
    }

    private void CheckForUnsavedChanges()
    {
        if (Componenten().Count() != 0)
        {
            DialogResult saveDialogResult = MessageBox.Show("Wil je de huidige tekening opslaan?", "Opslaan", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (saveDialogResult == DialogResult.Yes)
            {
                btnSaveFile_Click(null, null);
            }
        }
    }

    private void DeleteContextMenuItemClick(object sender, EventArgs e)
    {
        _invoker.SetCommand(new RemoveComponentCommand(_controller, _currentComponent.Id));
        _invoker.Execute();
        Refresh();
        FillTreeview();

    }

    private bool IsInDrawingMode()
    {
        return _currentMode is DrawingMode.Ellipse or DrawingMode.Rectangle;
    }

    private void FillTreeview()
    {
        treeView.BeginUpdate();
        treeView.Nodes.Clear();

        foreach (IComponent component in _controller.GetComponents())
        {
            TreeNode newNode = new() { Text = component.Name, Tag = component };

            if (component is Group Group)
            {
                AddChildNodesRecursive(newNode, Group, treeView);
            }

            treeView.Nodes.Add(newNode);
            if (component.Selected)
            {
                treeView.SelectedNode = newNode;
            }
        }

        treeView.EndUpdate();

        treeView.ExpandAll();
    }

    private static void AddChildNodesRecursive(TreeNode node, Group Group, TreeView treeView)
    {
        foreach (IComponent component in Group.Children)
        {
            TreeNode subNode = new() { Text = component.Name, Tag = component };

            if (component is Group subGroup)
            {
                AddChildNodesRecursive(subNode, subGroup, treeView);
            }

            node.Nodes.Add(subNode);
            if (!Group.Selected && component.Selected)
            {
                treeView.SelectedNode = subNode;
            }
        }
    }

    private void AddChildGroupMenuItemClick(object? sender, EventArgs e)
    {
        NewGroupCommand command = new(_controller, _currentComponent.Id);
        _invoker.SetCommand(command);
        _invoker.Execute();
        FillTreeview();
    }

    private void LabelsMenuItemClick(object? sender, EventArgs e)
    {
        Group parent = _controller.FindParentGroup(_currentComponent.Id);
        LabelEditor form = new(_currentComponent, parent, _invoker);
        form.Closed += (_, _) => Refresh();
        form.ShowDialog();
    }

    #endregion
}