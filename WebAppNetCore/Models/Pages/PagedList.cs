using System;
using System.Collections.Generic;
using System.Linq;

namespace WebAppNetCore.Models.Pages
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

        public PagedList(IQueryable<T> query, QueryOptions option = null)
        {
            CurrentPage = option.CurrentPage;
            PageSize = option.PageSize;
            TotalPages = query.Count() / PageSize;
            AddRange(query.Skip((CurrentPage - 1 ) * PageSize).Take(PageSize));
        }
    }
}
