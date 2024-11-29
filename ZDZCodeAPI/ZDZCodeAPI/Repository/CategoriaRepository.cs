using ZDZCodeAPI.Context;
using ZDZCodeAPI.Models;
using ZDZCodeAPI.Repository.Interfaces;

namespace ZDZCodeAPI.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Categoria> GetCategories()
        {
            return _context.Categorias.ToList();
        }

        public Categoria GetCategory(int id)
        {
            return _context.Categorias.FirstOrDefault(c => c.categoriaId == id);
        }
        public Categoria Create(Categoria category)
        {
            if (category is null)
                throw new ArgumentNullException(nameof(category));

            _context.Categorias.Add(category);
            _context.SaveChanges();

            return category;
        }

        public Categoria Delete(int id)
        {
            var category = _context.Categorias.Find(id);

            if (category is null)
                throw new ArgumentNullException(nameof(category));

            _context.Categorias.Remove(category);
            _context.SaveChanges();

            return category;
        }

        public Categoria Update(Categoria category)
        {
            if (category is null)
                throw new ArgumentNullException(nameof(category));

            _context.Categorias.Update(category);
            _context.SaveChanges();

            return category;
        }
    }
}
