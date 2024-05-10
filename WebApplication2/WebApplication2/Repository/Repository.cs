using System.Data;
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
        }
        
        using SqlConnection con = new SqlConnection(_configuration.GetConnectionString("Default"));
        con.Open();
        using SqlCommand command = new SqlCommand();
        command.Connection = con;
        
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

        int idO = reader.GetOrdinal("idAnimal");
        int nameO = reader.GetOrdinal("name");
        int descriptionO = reader.GetOrdinal("description");
        int categoryO = reader.GetOrdinal("category");
        int areaO = reader.GetOrdinal("area");
        
        while (reader.Read())
        {
            int id = reader.GetInt32(idO);
            string name = reader.GetString(nameO);
            string description;
            if (reader.IsDBNull(descriptionO))
            {
                description = "";
            }
            else
            {
                description = reader.GetString(descriptionO);
            }
            string category = reader.GetString(categoryO);
            string area = reader.GetString(areaO);
            animals.Add(new Animal(id,name,description,category,area));
        }
        con.Close();
        
        return animals;
    }

    public bool Create(DTO.DTO dto)
    {
        using var con = new SqlConnection(_configuration.GetConnectionString("Default"));
        con.Open();
        using SqlCommand command = new SqlCommand();
        command.Connection = con;

        command.CommandText = "INSERT INTO Animal (Name, Description, Category, Area) VALUES (@name, @description, @category, @area)";
        command.Parameters.AddWithValue("@name", dto.Name);
        command.Parameters.AddWithValue("@description", dto.Description);
        command.Parameters.AddWithValue("@category", dto.Category);
        command.Parameters.AddWithValue("@area", dto.Area);

        var affectedRows = command.ExecuteNonQuery();
        
        con.Close();
        return affectedRows == 1;
    }

    public bool Update(int id, DTO.DTO dto)
    {
        using var con = new SqlConnection(_configuration.GetConnectionString("Default"));
        con.Open();
        using SqlCommand command = new SqlCommand();
        command.Connection = con;
        
        command.CommandText = "UPDATE Animal SET Name = @name, Description = @description, Category = @category, Area = @area WHERE IdAnimal = @id";
        command.Parameters.AddWithValue("id", id);
        command.Parameters.AddWithValue("name", dto.Name);
        command.Parameters.AddWithValue("description", dto.Description);
        command.Parameters.AddWithValue("category", dto.Category);
        command.Parameters.AddWithValue("area", dto.Area);
        
        var affectedRows = command.ExecuteNonQuery();
        
        con.Close();
        return affectedRows == 1;
    }

    public bool Delete(int id)
    {
        using var con = new SqlConnection(_configuration.GetConnectionString("Default"));
        con.Open();
        using SqlCommand command = new SqlCommand();
        command.Connection = con;
        
        command.CommandText = "DELETE FROM Animal WHERE IdAnimal = @id";
        command.Parameters.AddWithValue("id", id);

        var affectedRows = command.ExecuteNonQuery();
        
        con.Close();
        return affectedRows == 1;
    }
}