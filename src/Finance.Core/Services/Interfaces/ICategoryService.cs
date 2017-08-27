using System.Collections.Generic;
using System.Threading.Tasks;
using Finance.Common.Entities;

namespace Finance.Core.Services
{
    public interface ICategoryService
    {
        Task Delete(string id);
        
        Task Create(Category category);

        Task<Category> GetById(string id);
        
        IEnumerable<Category> GetAll();
        
        void Update(Category category);
    }
}
