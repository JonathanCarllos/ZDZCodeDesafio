using ZDZCodeAPI.Models;

namespace ZDZCodeAPI.Repository.Interfaces
{
    public interface IProdutoRepository
    {
        IEnumerable<Produto> GetProdutcs();
        Produto GetProduct(int id);
        Produto Create(Produto product);
        bool Update(Produto produto);
        bool Delete(int id);
    }
}
