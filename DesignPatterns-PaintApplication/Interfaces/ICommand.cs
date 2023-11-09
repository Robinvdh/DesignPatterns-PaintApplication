namespace DesignPatterns_PaintApplication.Interfaces;

public interface ICommand
{
    void Execute();
    void Undo();
}
