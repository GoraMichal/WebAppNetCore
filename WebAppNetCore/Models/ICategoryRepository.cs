using System.Collections.Generic;
using WebAppNetCore.Models.Pages;

namespace WebAppNetCore.Models
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
        PagedList<Category> GetCategories(QueryOptions options);
    }
}
