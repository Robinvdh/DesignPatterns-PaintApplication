using DesignPatterns_PaintApplication.CompositePattern;
using DesignPatterns_PaintApplication.Interfaces;
using Newtonsoft.Json;

namespace DesignPatterns_PaintApplication.VisitorPattern;

internal class SaveVisitor : IVisitor
{
    private readonly string _filePath;

    public SaveVisitor(string filePath)
    {
        _filePath = filePath;
    }

    public void VisitFigure(Figure figure)
    {
        MessageBox.Show("Single figures cannot be saved.", "Error",
        MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    public void VisitGroup(Group group)
    {
        StreamWriter fileStream = File.CreateText(_filePath);
        string json = JsonConvert.SerializeObject(group, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
        fileStream.Write(json);
        fileStream.Close();
    }
}
