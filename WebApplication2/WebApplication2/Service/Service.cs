using System.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using WebApplication2.Model;
using WebApplication2.Repository;

namespace WebApplication2.Service;

public interface IService
{
    public IEnumerable<Animal> GetAll(string? orderBy);
    public bool Create(DTO.DTO dto);
    public bool Update(int id, DTO.DTO dto);
    public bool Delete(int id);
}
public class Service : IService
{
    private IRepository _repository;
    public Service(IRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Animal> GetAll(string? orderBy)
    {
        Debug.Assert(_repository != null, nameof(_repository) + " != null");
        return _repository.GetAll(orderBy);
    }

    public bool Create(DTO.DTO dto)
    {
        Debug.Assert(_repository != null, nameof(_repository) + " != null");
        return _repository.Create(dto);
    }

    public bool Update(int id, DTO.DTO dto)
    {
        Debug.Assert(_repository != null, nameof(_repository) + " != null");
        return _repository.Update(id, dto);
    }

    public bool Delete(int id)
    {
        Debug.Assert(_repository != null, nameof(_repository) + " != null");
        return _repository.Delete(id);
    }
}