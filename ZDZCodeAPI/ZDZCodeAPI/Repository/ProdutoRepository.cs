using ZDZCodeAPI.Context;
using ZDZCodeAPI.Models;
using ZDZCodeAPI.Repository.Interfaces;

namespace ZDZCodeAPI.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public Produto GetProduct(int id)
        {
            return _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
        }
        public IEnumerable<Produto> GetProdutcs()
        {
            return _context.Produtos.ToList();
        }
        public Produto Create(Produto product)
        {
            if (product is null)
                throw new ArgumentNullException(nameof(product));

            _context.Produtos.Add(product);
            _context.SaveChanges();

            return product;
        }

        public bool Update(Produto produto)
        {
            if (produto is null)
                throw new InvalidOperationException("Produto é null");

            if (_context.Produtos.Any(p => p.ProdutoId == produto.ProdutoId))
            {
                _context.Produtos.Update(produto);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            var produto = _context.Produtos.Find(id);

            if (produto is not null)
            {
                _context.Produtos.Remove(produto);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
