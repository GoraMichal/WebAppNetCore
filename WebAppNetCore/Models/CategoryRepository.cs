using System.Collections.Generic;
using WebAppNetCore.Models;
using WebAppNetCore.Models.Pages;

namespace WebAppNetCore.Models
{

    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext context;

        public CategoryRepository(DataContext ctx) => context = ctx;

        public IEnumerable<Category> Categories => context.Categories;

        public PagedList<Category> GetCategories(QueryOptions options)
        {
            return new PagedList<Category>(context.Categories, options);
        }

        public void AddCategory(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            context.Categories.Update(category);
            context.SaveChanges();
        }

        public void DeleteCategory(Category category)
        {
            context.Categories.Remove(category);
            context.SaveChanges();
        }
    }
}
