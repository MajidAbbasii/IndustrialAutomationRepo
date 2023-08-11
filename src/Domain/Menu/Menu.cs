using Domain.Base;

namespace Domain.Menu;

public class Menu: BaseEntity
{
    public Menu(string title)
    {
        Title = title;
    }

    private string Title { get; set; }

    protected virtual Menu? TheParentMenu { get; set; }

    public Guid ParentMenuId { get; set; }

    protected virtual List<Menu>? TheChildMenuList { get; set; }
}