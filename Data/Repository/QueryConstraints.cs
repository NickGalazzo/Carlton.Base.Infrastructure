using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Carlton.Base.Infrastructure.Data
{
    public class QueryConstraints<T> : IQueryConstraints<T> where T : class
    {
        public int StartRecord
        {
            get
            {
                if (PageNumber <= 1)
                    return 0;

                return (PageNumber - 1) * (PageSize);
            }
        }

        public int PageSize { get; private set; }

        public int PageNumber { get; private set; }

        public SortOrder SortOrder { get; private set; }

        public string SortPropertyName { get; private set; }

        protected Type ModelType { get; set; }

        public QueryConstraints()
        {
            ModelType = typeof(T);
            PageSize = 50;
            PageNumber = 1;
        }

        public IQueryConstraints<T> Page(int pageNumber, int pageSize)
        {
            if (pageNumber < 1 || pageNumber > 1000)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number must be between 1 and 1000.");
            if (pageSize < 1 || pageNumber > 1000)
                throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be between 1 and 1000.");

            PageSize = pageSize;
            PageNumber = pageNumber;
            return this;
        }

        public IQueryConstraints<T> SortBy(string propertyName)
        {
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            ValidatePropertyName(propertyName);

            SortOrder = SortOrder.Ascending;
            SortPropertyName = propertyName;
            return this;
        }

        public IQueryConstraints<T> SortByDescending(string propertyName)
        {
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            ValidatePropertyName(propertyName);

            SortOrder = SortOrder.Descending;
            SortPropertyName = propertyName;
            return this;
        }

        public IQueryConstraints<T> SortBy(Expression<Func<T, object>> propExpression)
        {
            if (propExpression == null) throw new ArgumentNullException(nameof(propExpression));
            var property = ((MemberExpression)propExpression.Body).Member as PropertyInfo;
            var name = property.Name;
            SortBy(name);
            return this;
        }

        public IQueryConstraints<T> SortByDescending(Expression<Func<T, object>> propExpression)
        {
            if (propExpression == null) throw new ArgumentNullException(nameof(propExpression));
            var property = ((MemberExpression)propExpression.Body).Member as PropertyInfo;
            var name = property.Name;
            SortByDescending(name);
            return this;
        }

        protected virtual void ValidatePropertyName(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (ModelType.GetProperty(name) == null)
            {
                throw new ArgumentException(string.Format(new System.Globalization.CultureInfo("en-US"), 
                                                          "'{0}' is not a public property of '{1}'.", name,
                                                          ModelType.FullName));
            }
        }
    }
}

