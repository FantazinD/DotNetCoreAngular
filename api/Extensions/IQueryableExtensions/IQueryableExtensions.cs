using System.Linq.Expressions;
using api.Interfaces;

namespace api.Extensions.IQueryableExtensions
{
    public static class IQueryableExtensions
    {

        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, IQueryObject queryObject, Dictionary<string, Expression<Func<T, object>>> columnsMap){
            if(String.IsNullOrWhiteSpace(queryObject.SortBy) || !columnsMap.ContainsKey(queryObject.SortBy))
                return query;

            return queryObject.IsSortAscending ? 
                query.OrderBy(columnsMap[queryObject.SortBy]) 
                : 
                query.OrderByDescending(columnsMap[queryObject.SortBy]);
        }
    }
}