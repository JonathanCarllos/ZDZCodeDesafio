using ZDZCodeAPI.Models;

namespace ZDZCodeAPI.Repository.Interfaces
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> GetCategories();
        Categoria GetCategory(int id);
        Categoria Create(Categoria category);
        Categoria Update(Categoria category);
        Categoria Delete(int id);
    }
}
