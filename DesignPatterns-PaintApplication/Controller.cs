using DesignPatterns_PaintApplication.CompositePattern;
using DesignPatterns_PaintApplication.Enum;
using DesignPatterns_PaintApplication.Interfaces;

namespace DesignPatterns_PaintApplication;

public class Controller
{
    private Group _parentGroup = new();

    public Group ParentGroup { get => _parentGroup; set => _parentGroup = value; }

    public IEnumerable<IComponent> GetComponents() { return _parentGroup.Children; }

    public IComponent GetComponent(int id)
    {
        IComponent? component = _parentGroup.Children.FirstOrDefault(f => f.Id == id);
        if (component is not null)
        {
            return component;
        }

        IEnumerable<IComponent> groups = Groups();

        foreach (IComponent group in groups)
        {
            component = GetComponent(id, group.InnerComponent() as Group);
            if (component is not null)
            {
                return component;
            }
        }

        return null;
    }

    public IComponent GetComponent(int id, Group group)
    {
        IComponent? component = group.Children.FirstOrDefault(f => f.Id == id);
        if (component is not null)
        {
            return component;
        }
        else
        {
            IEnumerable<IComponent> groups = group.Children.Where(g => g.ComponentType == ComponentType.Group);
            foreach (IComponent? subgroup in groups)
            {
                component = GetComponent(id, subgroup.InnerComponent() as Group);
                if (component is not null)
                {
                    return component;
                }
            }
        }

        return null;
    }

    public Group FindParentGroup(int childId, Group ancestor = null)
    {
        IComponent topLevelComponent;
        IEnumerable<Group> groups;

        if (ancestor is null)
        {
            topLevelComponent = _parentGroup.Children.FirstOrDefault(c => c.Id == childId);
            if (topLevelComponent is not null)
            {
                return ParentGroup;
            }

            groups = Groups().Select(g => g.InnerComponent() as Group);
        }
        else
        {
            topLevelComponent = ancestor.Children.FirstOrDefault(c => c.Id == childId);
            if (topLevelComponent is not null)
            {
                return ancestor;
            }

            groups = ancestor.Groups.Select(c => c.InnerComponent() as Group);
        }


        foreach (Group subgroup in groups)
        {
            Group? parent = FindParentGroup(childId, subgroup);
            if (parent is not null)
            {
                return parent;
            }
        }

        return null;
    }

    public IEnumerable<IComponent> GetAllFiguresFlattened() => _parentGroup.AllFiguresFlattened();

    public int? SelectedGroupId()
    {
        IEnumerable<Group?> groups = Groups().Select(g => g.InnerComponent() as Group);

        foreach (Group? group in groups)
        {
            if (group.Selected)
            {
                return group.Id;
            }
        }

        int? id = null;

        foreach (Group? group in groups)
        {
            id = SelectedSubgroupIdRecursive(group);
        }

        return id;
    }

    private int? SelectedSubgroupIdRecursive(Group group)
    {
        foreach (IComponent subGroup in group.Groups)
        {
            if (subGroup.Selected)
            {
                return subGroup.Id;
            }
        }

        int? id = null;

        foreach (IComponent subGroup in group.Groups)
        {
            id = SelectedSubgroupIdRecursive(subGroup.InnerComponent() as Group);
        }

        return id;
    }

    public int CreateFigure(Rectangle rectangle, ObjectType objectType, int? parentGroupId)
    {
        int newId = GetNewId();

        Figure figure = new()
        {
            Id = newId,
            Name = "figure " + newId,
            Placement = rectangle,
            ObjectType = objectType,
            Selected = false
        };

        if (parentGroupId is null)
        {
            _parentGroup.Children.Add(figure);
        }
        else
        {
            Group parent = GetGroup(parentGroupId.Value);
            parent.Children.Add(figure);
        }

        return newId;
    }

    public Group GetGroup(int groupId)
    {
        IComponent component = GetComponent(groupId);
        return component.InnerComponent() as Group;
    }

    private int GetNewId()
    {
        if (_parentGroup.Children.Count == 0)
        {
            return 1;
        }

        List<int> ids = new();
        foreach (IComponent component in _parentGroup.Children)
        {
            AddIdsFromChildren(ids, component);
        }

        return ids.Max() + 1;
    }

    private void AddIdsFromChildren(List<int> ids, IComponent component)
    {
        ids.Add(component.Id);
        if (component is Group childGroup)
        {
            ids.AddRange(GetIdsFromGroupRecursive(childGroup));
        }
    }

    private IEnumerable<int> GetIdsFromGroupRecursive(Group group)
    {
        List<int> ids = new();

        foreach (IComponent child in group.Children)
        {
            AddIdsFromChildren(ids, child);
        }

        return ids;
    }

    public void RemoveComponent(int id)
    {
        IComponent? component = _parentGroup.Children.FirstOrDefault(f => f.Id == id);
        if (component is not null)
        {
            _parentGroup.Children.Remove(component);
        }
        else
        {
            foreach (Group? group in Groups().Select(g => g.InnerComponent() as Group))
            {
                if (RemoveComponentFromGroupRecursive(group, id))
                {
                    return;
                }
            }
        }
    }

    private static bool RemoveComponentFromGroupRecursive(Group group, int id)
    {
        IComponent? component = group.Children.FirstOrDefault(c => c.Id == id);
        if (component is not null)
        {
            group.Children.Remove(component);
            return true;
        }

        return group.Groups.Any(subGroup => RemoveComponentFromGroupRecursive(subGroup.InnerComponent() as Group, id));
    }

    public void SelectFigure(int id)
    {
        if (GetFigure(id) is { } figure)
        {
            ClearSelection();
            figure.Selected = true;
        }
    }

    public Figure GetFigure(int id)
    {
        IComponent component = GetComponent(id);
        return (Figure)component.InnerComponent();
    }

    public void ResetComponents()
    {
        _parentGroup.Children.Clear();
    }

    public int CreateGroup(int? parentGroupId = null)
    {
        int newId = GetNewId();
        Group group = new() { Name = "group " + newId, Id = newId };
        group.Children = new List<IComponent>();

        if (parentGroupId is null)
        {
            _parentGroup.Children.Add(group);
        }
        else
        {
            Group parent = GetGroup(parentGroupId.Value);
            parent.Children.Add(group);
        }

        return newId;
    }

    public void AddGroup(Group group)
    {
        _parentGroup.Children.Add(group);
    }

    public IEnumerable<IComponent> Groups()
    {
        return _parentGroup.Children.Where(c => c.ComponentType == ComponentType.Group);
    }

    public void ClearSelection()
    {
        foreach (IComponent component in _parentGroup.Children)
        {
            component.Selected = false;
            if (component is Group group)
            {
                ClearSelectionInGroup(group);
            }
        }
    }

    private void ClearSelectionInGroup(Group group)
    {
        foreach (IComponent component in group.Children)
        {
            component.Selected = false;
            if (component is Group subGroup)
            {
                ClearSelectionInGroup(subGroup);
            }
        }
    }

    public void SelectGroupRecursive(Group group)
    {
        foreach (IComponent component in group.Children)
        {
            component.Selected = true;
            if (component is Group subGroup)
            {
                SelectGroupRecursive(subGroup);
            }
        }
    }
}
