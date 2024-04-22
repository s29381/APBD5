using System.Diagnostics;
using WebApplication2.Model;
using WebApplication2.Repository;

namespace WebApplication2.Service;

public interface IService
{
    public static abstract IEnumerable<Animal> GetAll(string orderBy);
    public static abstract bool Create(DTO.DTO dto);
    public static abstract bool Update(int id, DTO.DTO dto);
    public static abstract bool Delete(int id);
}
public class Service : IService
{
    private static IRepository? _repository;
    public Service(IRepository? repository)
    {
        _repository = repository;
    }

    public static IEnumerable<Animal> GetAll(string orderBy)
    {
        Debug.Assert(_repository != null, nameof(_repository) + " != null");
        return _repository.GetAll(orderBy);
    }

    public static bool Create(DTO.DTO dto)
    {
        Debug.Assert(_repository != null, nameof(_repository) + " != null");
        return _repository.Create(dto);
    }

    public static bool Update(int id, DTO.DTO dto)
    {
        Debug.Assert(_repository != null, nameof(_repository) + " != null");
        return _repository.Update(id, dto);
    }

    public static bool Delete(int id)
    {
        Debug.Assert(_repository != null, nameof(_repository) + " != null");
        return _repository.Delete(id);
    }
}