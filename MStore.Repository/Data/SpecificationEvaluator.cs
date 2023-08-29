using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MStore.Core.Entities;
using MStore.Core.Specification;

namespace MStore.Repository.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec) 
        {  
            var query = inputQuery;


            if(spec.Criteria is not null)
                query = query.Where(spec.Criteria);

            if (spec.IsPaginationEnabled)
                query = query.Skip(spec.Skip).Take(spec.Take);

            if (spec.OrderBy != null)
                query = query.OrderBy(spec.OrderBy);

            if (spec.OrderByDesc != null)
                query = query.OrderByDescending(spec.OrderByDesc);

            query = spec.Includes.Aggregate(query, (currentQuery, includeQuery) => currentQuery.Include(includeQuery));

            return query;
        }
    }
}
