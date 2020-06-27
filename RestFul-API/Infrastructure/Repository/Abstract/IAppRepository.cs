using RestFul_API.Infrastructure.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestFul_API.Infrastructure.Repository.Abstract
{
    public interface IAppRepository
    {
        ICollection<Category> GetCategory();
        Category GetCategory(Guid id);
        bool CategoryExists(Guid id);
        bool CategoryExists(string categoryName);
        bool CreateCategory(Category categoryObj);
        bool UpdateCategory(Guid id);
        bool DeleteCategory(Guid id);
        bool Save();
    }
}
