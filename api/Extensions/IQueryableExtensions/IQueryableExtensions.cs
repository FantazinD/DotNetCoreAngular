using System.Linq.Expressions;
using api.Interfaces;
using api.Models;

namespace api.Extensions.IQueryableExtensions
{
    public static class IQueryableExtensions
    {

        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, IQueryObject queryObject, Dictionary<string, Expression<Func<T, object>>> columnsMap){
            return queryObject.IsSortAscending ? 
                query.OrderBy(columnsMap[queryObject.SortBy]) 
                : 
                query.OrderByDescending(columnsMap[queryObject.SortBy]);
        }
    }
}