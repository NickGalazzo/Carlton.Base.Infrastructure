using System.Collections.Generic;
using System.Linq;

namespace Carlton.Base.Infrastructure.Data.Repository
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; }
        public int Count { get; }
        public int PageNumber { get; }

        public static PagedResult<T> Create(IEnumerable<T> items, int pageNumber)
        {
            return new PagedResult<T>(items, items.Count(), pageNumber);
        }

        public PagedResult(IEnumerable<T> items, int count, int pageNumber)
        {
            Items = items;
            Count = count;
            PageNumber = pageNumber;
        }
    }
}
