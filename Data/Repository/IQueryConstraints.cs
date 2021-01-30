using System;
using System.Linq.Expressions;

namespace Carlton.Base.Infrastructure.Data
{
    public interface IQueryConstraints<T> 
    {
        int PageSize { get; }
        int PageNumber { get; }
        SortOrder SortOrder { get; }
        string SortPropertyName { get; }

        IQueryConstraints<T> Page(int pageNumber, int pageSize);
        IQueryConstraints<T> SortBy(string propertyName);
        IQueryConstraints<T> SortByDescending(string propertyName);
        IQueryConstraints<T> SortBy(Expression<Func<T, object>> sortByProperty);
        IQueryConstraints<T> SortByDescending(Expression<Func<T, object>> sortByProperty);
    }
}


