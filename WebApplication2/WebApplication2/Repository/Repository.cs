using System.Data.SqlClient;
using WebApplication2.Model;

namespace WebApplication2.Repository;

public interface IRepository
{
    public IEnumerable<Animal> GetAll(string orderBy);
    public bool Create(DTO.DTO dto);
    public bool Update(int id, DTO.DTO dto);
    public bool Delete(int id);
}
public class Repository : IRepository
{
    private IConfiguration _configuration;
    public Repository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public IEnumerable<Animal> GetAll(string orderBy)
    {
        using var con = new SqlConnection(_configuration.GetConnectionString("DefaultConnectionString"));
        con.Open();
        string[] allowedColumns = ["idanimal", "name", "description", "category", "area"];
        int orderColumn = Array.IndexOf(allowedColumns, orderBy.ToLower());
        if (orderColumn < 0)
            orderColumn = 1;
        using var command = new SqlCommand($"SELECT IdAnimal, Name, Description, Category, Area FROM s29143.Animal ORDER BY {allowedColumns[orderColumn]}", con);

        var animals = new List<Animal>();
        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var animal = new Animal
            {
                Id = (int)reader["IdAnimal"],
                Name = reader["Name"].ToString()!,
                Description = reader["Description"].ToString(),
                Category = reader["Category"].ToString()!,
                Area = reader["Area"].ToString()!
            };

            animals.Add(animal);
        }

        return animals;
    }

    public bool Create(DTO.DTO dto)
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnectionString"));
        connection.Open();
        
        using var command = new SqlCommand("INSERT INTO s29143.Animal (Name, Description, Category, Area) " +
                                           "VALUES (@name, @description, @category, @area)", connection);
        command.Parameters.AddWithValue("name", dto.Name);
        command.Parameters.AddWithValue("description", dto.Description);
        command.Parameters.AddWithValue("category", dto.Category);
        command.Parameters.AddWithValue("area", dto.Area);

        return command.ExecuteNonQuery() == 1;
    }

    public bool Update(int id, DTO.DTO dto)
    {
        using var con = new SqlConnection(_configuration.GetConnectionString("DefaultConnectionString"));
        con.Open();
        
        using var command = new SqlCommand("UPDATE s29143.Animal SET Name = @name, Description = @description," +
                                           "Category = @category, Area = @area WHERE IdAnimal = @id", con);
        command.Parameters.AddWithValue("id", id);
        command.Parameters.AddWithValue("name", dto.Name);
        command.Parameters.AddWithValue("description", dto.Description);
        command.Parameters.AddWithValue("category", dto.Category);
        command.Parameters.AddWithValue("area", dto.Area);

        return command.ExecuteNonQuery() == 1;
    }

    public bool Delete(int id)
    {
        using var con = new SqlConnection(_configuration.GetConnectionString("DefaultConnectionString"));
        con.Open();
        
        using var command = new SqlCommand("DELETE FROM s29143.Animal WHERE IdAnimal = @id", con);
        command.Parameters.AddWithValue("id", id);

        return command.ExecuteNonQuery() == 1;
    }
}