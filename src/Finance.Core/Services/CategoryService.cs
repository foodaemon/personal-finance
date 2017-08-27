using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Finance.Common.Entities;
using Finance.Data.Repositories;

namespace Finance.Core.Services
{
    public class CategoryService : ICategoryService
    {
        readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {   
            _categoryRepository = categoryRepository;
        }

        public async Task Create(Category category)
        {
            category.CreatedAt = DateTime.UtcNow;
            await _categoryRepository.Add(category);
        }

        public async Task Delete(string id)
        {
            var category = await GetById(id);
            _categoryRepository.Delete(category.Id);
        }

        public IEnumerable<Category> GetAll()
        {
            var data = _categoryRepository.GetAll();
            return data;
        }

        public async Task<Category> GetById(string id)
        {
            var data = await _categoryRepository.GetById(id);
            return data;
        }

        public void Insert(Category category)
        {
            _categoryRepository.Add(category);
        }

        public void Update(Category category)
        {
            _categoryRepository.Update(category);
        }
    }
}
