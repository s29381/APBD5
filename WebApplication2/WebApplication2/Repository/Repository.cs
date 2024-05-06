using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using WebApplication2.Model;

namespace WebApplication2.Repository;

public interface IRepository
{
    public IEnumerable<Animal> GetAll(string? orderBy);
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
    public IEnumerable<Animal> GetAll(string? orderBy)
    {
        if (orderBy != null)
        {
            orderBy = orderBy.ToLower();
            string regexPattern = "^(name|description|category|area)$";
        }
        
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        
        if (orderBy == null)
        {
            command.CommandText = "SELECT * FROM Animal order by Name";
        }
        else
        {
            command.CommandText = "SELECT * FROM Animal order by " + orderBy;
        }
        var reader = command.ExecuteReader();

        List<Animal> animals = new List<Animal>();

        int idO = reader.GetOrdinal("IdAnimal");
        int nameO = reader.GetOrdinal("Name");
        int DescriptionO = reader.GetOrdinal("Description");
        int categoryO = reader.GetOrdinal("Category");
        int areaO = reader.GetOrdinal("Area");
        
        while (reader.Read())
        {
            int id = reader.GetInt32(idO);
            string name = reader.GetString(nameO);
            string description;
            if (reader.IsDBNull(DescriptionO))
            {
                description = "";
            }
            else
            {
                description = reader.GetString(DescriptionO);
            }
            string category = reader.GetString(categoryO);
            string area = reader.GetString(areaO);
            animals.Add(new Animal(id,name,description,category,area));
        }
        connection.Close();
        
        return animals;
    }

    public bool Create(DTO.DTO dto)
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        
        using var command = new SqlCommand("INSERT INTO Animal (Name, Description, Category, Area) " +
                                           "VALUES (@name, @description, @category, @area)", connection);
        command.Parameters.AddWithValue("name", dto.Name);
        command.Parameters.AddWithValue("description", dto.Description);
        command.Parameters.AddWithValue("category", dto.Category);
        command.Parameters.AddWithValue("area", dto.Area);

        return command.ExecuteNonQuery() == 1;
    }

    public bool Update(int id, DTO.DTO dto)
    {
        using var con = new SqlConnection(_configuration.GetConnectionString("Default"));
        con.Open();
        
        using var command = new SqlCommand("UPDATE Animal SET Name = @name, Description = @description," +
                                           "Category = @category, Area = @area WHERE IdAnimal = @id", con);
        command.Parameters.AddWithValue("name", dto.Name);
        command.Parameters.AddWithValue("description", dto.Description);
        command.Parameters.AddWithValue("category", dto.Category);
        command.Parameters.AddWithValue("area", dto.Area);

        return command.ExecuteNonQuery() == 1;
    }

    public bool Delete(int id)
    {
        using var con = new SqlConnection(_configuration.GetConnectionString("Default"));
        con.Open();
        
        using var command = new SqlCommand("DELETE FROM Animal WHERE IdAnimal = @id", con);
        command.Parameters.AddWithValue("id", id);

        return command.ExecuteNonQuery() == 1;
    }
}