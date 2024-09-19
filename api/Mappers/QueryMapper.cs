using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Common;
using api.Models;

namespace api.Mappers
{
    public static class QueryMapper
    {
        public static QueryResultDTO<T> ToQueryResultDTO<T>(this QueryResult<T> queryResult){
            return new QueryResultDTO<T>
            {
                TotalItems = queryResult.TotalItems,
                Items = queryResult.Items
            };
        }
    }
}