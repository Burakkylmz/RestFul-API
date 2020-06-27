using RestFul_API.Infrastructure.Context;
using RestFul_API.Infrastructure.Entities.Concrete;
using RestFul_API.Infrastructure.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestFul_API.Infrastructure.Repository.Concrete
{
    public class EfAppRepository : IAppRepository
    {
        private readonly ApplicationDbContext _context;

        public EfAppRepository(ApplicationDbContext applicationDbContext)
        {
            this._context = applicationDbContext;
        }

        public bool CreateCategory(Category categoryObj)
        {
            _context.Categories.AddAsync(categoryObj);
            return Save();
        }

        public bool DeleteCategory(Guid id)
        {
            var category = _context.Categories.Find(id);
            _context.Categories.Remove(category);
            return Save();
        }

        public ICollection<Category> GetCategory()
        {
            return _context.Categories.OrderBy(x => x.Id).ToList();
        }

        public Category GetCategory(Guid id)
        {
            return _context.Categories.FirstOrDefault(x => x.Id == id);
        }

        public bool CategoryExists(Guid id)
        {
            return _context.Categories.Any(x => x.Id == id);
        }

        public bool CategoryExists(string categoryName)
        {
            return _context.Categories.Any(x => x.Name.ToLower().Trim() == categoryName.ToLower().Trim());
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateCategory(Guid id)
        {
            var category = _context.Categories.Find(id);
            _context.Categories.Update(category);
            return Save();
        }
    }
}
