using System.Collections.Generic;
using System.Threading.Tasks;
using Finance.Common.Entities;

namespace Finance.Data.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        Task<Category> GetById(string id);
		Task Add(Category category);
        bool Delete(string id);
        void Update(Category category);
    }
}
