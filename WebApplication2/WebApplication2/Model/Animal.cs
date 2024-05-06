namespace WebApplication2.Model;

public class Animal
{
    public Animal(int idAnimal, string nameAnimal, string descriptionAnimal, string categoryAnimal, string area)
    {
    }

    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public string? Area { get; set; }
}