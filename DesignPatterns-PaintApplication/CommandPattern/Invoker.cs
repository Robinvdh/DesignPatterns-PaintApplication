using DesignPatterns_PaintApplication.Interfaces;

namespace DesignPatterns_PaintApplication.CommandPattern;

public class Invoker
{
    private ICommand? _command;
    private Stack<ICommand> _Undo = new();
    private Stack<ICommand> _Redo = new();
    public bool HasCommand => _command is not null;

    public void SetCommand(ICommand command)
    {
        this._command = command;
    }


    public void Execute()
    {
        if (_command == null)
        {
            return;
        }

        _command.Execute();
        _Undo.Push(_command);
        _Redo.Clear();
        _command = null;
    }

    public void Undo()
    {
        if (_Undo.Count > 0)
        {
            ICommand cmd = _Undo.Pop();
            cmd.Undo();
            _Redo.Push(cmd);
        }
    }

    public void Redo()
    {
        if (_Redo.Count > 0)
        {
            ICommand cmd = _Redo.Pop();
            cmd.Execute();
            _Undo.Push(cmd);
        }
    }
}
