using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;

namespace Extensions.IQueryableEx
{
    public static class IQueryableEx
    {
        /// <summary>
        /// Perform custom paging using LINQ to SQL
        /// Eg.: Collection.Page(pageIndex, pageSize, p => p.AnyFieldFromCollection, true, out rowsFetched);
        /// </summary>
        /// <typeparam name="T">Type of the Datasource to be paged</typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="obj">Object to be paged through</param>
        /// <param name="page">Page Number to fetch</param>
        /// <param name="pageSize">Number of rows per page</param>
        /// <param name="keySelector">Sorting Expression</param>
        /// <param name="asc">Sort ascending if true. Otherwise descending</param>
        /// <param name="rowsCount">Output parameter hold total number of rows</param>
        /// <returns>Page of result from the paged object</returns>
        public static IQueryable<T> Page<T, TResult>(this IQueryable<T> obj, int page, int pageSize, System.Linq.Expressions.Expression<Func<T, TResult>> keySelector, bool asc, out int rowsCount)
        {
            rowsCount = obj.Count();
            //int innerRows = rowsCount - (page * pageSize);
            if (asc)
                return obj.OrderByDescending(keySelector).Skip((page == 0 ? 0 : page - 1) * pageSize).Take(pageSize);
                    //.Take(innerRows).OrderBy(keySelector).Take(pageSize).AsQueryable();
            else
                return obj.OrderBy(keySelector).Skip((page == 0 ? 0 : page - 1) * pageSize).Take(pageSize);
                    //.Take(innerRows).OrderByDescending(keySelector).Take(pageSize).AsQueryable();
        }

        //public static IQueryable<T> AddEqual<T, V>(this IQueryable<T> queryable, string propertyName, V propertyValue)
        //{
        //    ParameterExpression pe = Expression.Parameter(typeof(T), "p");

        //    IQueryable<T> x = queryable.Where<T>(
        //        Expression.Lambda<Func<T, bool>>(
        //        Expression.Equal(Expression.Property(pe, typeof(T).GetProperty(propertyName)),
        //            Expression.Constant(propertyValue, typeof(V)), false, typeof(T).GetMethod("op_Equality")), new ParameterExpression[] { pe }));

        //    return (x);
        //}

        //public static IQueryable<T> AddGreaterThan<T, V>(this IQueryable<T> queryable, string propertyName, V propertyValue)
        //{
        //    ParameterExpression pe = Expression.Parameter(typeof(T), "p");

        //    IQueryable<T> x = queryable.Where<T>(
        //        Expression.Lambda<Func<T, bool>>(
        //        Expression.GreaterThan(Expression.Property(pe, typeof(T).GetProperty(propertyName)),
        //            Expression.Constant(propertyValue, typeof(V)), false, typeof(T).GetMethod("op_Equality")), new ParameterExpression[] { pe }));

        //    return (x);
        //}

        //public static IQueryable<T> AddGreaterThanOrEqual<T, V>(this IQueryable<T> queryable, string propertyName, V propertyValue)
        //{
        //    ParameterExpression pe = Expression.Parameter(typeof(T), "p");

        //    IQueryable<T> x = queryable.Where<T>(
        //        Expression.Lambda<Func<T, bool>>(
        //        Expression.GreaterThanOrEqual(Expression.Property(pe, typeof(T).GetProperty(propertyName)),
        //            Expression.Constant(propertyValue, typeof(V)), false, typeof(T).GetMethod("op_Equality")), new ParameterExpression[] { pe }));

        //    return (x);
        //}

        //public static IQueryable<T> AddLessThan<T, V>(this IQueryable<T> queryable, string propertyName, V propertyValue)
        //{
        //    ParameterExpression pe = Expression.Parameter(typeof(T), "p");

        //    IQueryable<T> x = queryable.Where<T>(
        //        Expression.Lambda<Func<T, bool>>(
        //        Expression.LessThan(Expression.Property(pe, typeof(T).GetProperty(propertyName)),
        //            Expression.Constant(propertyValue, typeof(V)), false, typeof(T).GetMethod("op_Equality")), new ParameterExpression[] { pe }));

        //    return (x);
        //}

        //public static IQueryable<T> AddLessThanOrEqual<T, V>(this IQueryable<T> queryable, string propertyName, V propertyValue)
        //{
        //    ParameterExpression pe = Expression.Parameter(typeof(T), "p");

        //    IQueryable<T> x = queryable.Where<T>(
        //        Expression.Lambda<Func<T, bool>>(
        //        Expression.Or(Expression.Property(pe, typeof(T).GetProperty(propertyName)),
        //            Expression.Constant(propertyValue, typeof(V)), false, typeof(T).GetMethod("op_Equality")), new ParameterExpression[] { pe }));

        //    return (x);
        //}
    }
}