using DesignPatterns_PaintApplication.CompositePattern;

namespace DesignPatterns_PaintApplication.Interfaces;

public interface IVisitor
{
    void VisitFigure(Figure figure);
    void VisitGroup(Group group);
}
