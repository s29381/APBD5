using System.ComponentModel.DataAnnotations;

namespace WebApplication2.DTO;

public class DTO
{
    public DTO(string name, string description, string category, string area)
    {
        Name = name;
        Description = description;
        Category = category;
        Area = area;
    }

    [Length(0, 100)]
    public string Name { get; set; }
    [Length(0, 100)]
    public string Description { get; set; }
    [Length(0, 100)]
    public string Category { get; set; }
    [Length(0, 100)]
    public string Area { get; set; }
}