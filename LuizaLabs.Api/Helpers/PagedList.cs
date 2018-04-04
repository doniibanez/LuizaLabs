using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuizaLabs.Api.Helpers
{
    public class PagedList<T> : List <T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        private bool HasPrevious
        {
            get
            {
                return CurrentPage > 1;
            }
        }

        private bool HasNext
        {
            get
            {
                return TotalPages > CurrentPage;
            }
        }

        public PagedList(List<T> items, int totalCount, int pageNumber, int pageSize)
        {
            TotalCount = totalCount;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(totalCount/(double)pageSize);
            AddRange(items);
        }

        public static PagedList<T> Create(List<T> source, int pageNumber, int pageSize)
        {
            var totalCount = source.Count();
            var items = source.Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();

            return new PagedList<T>(items, totalCount, pageNumber, pageSize);
        }
    }
}