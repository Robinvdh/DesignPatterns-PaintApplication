using System.ComponentModel.DataAnnotations;

namespace DesignPatterns_PaintApplication.Enum;

public enum LabelDirection
{
    [Display(Name = "Links")]
    Left,
    [Display(Name = "Rechts")]
    Right,
    [Display(Name = "Boven")]
    Top,
    [Display(Name = "Onder")]
    Bottom
}
