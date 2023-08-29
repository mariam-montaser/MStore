using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MStore.Core.Entities;

namespace MStore.Core.Specification
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {

        public BaseSpecification()
        { }


        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }

        public bool IsPaginationEnabled { get; set; }

        public void AddOrderBy(Expression<Func<T, object>> orderBy)
        {
            OrderBy = orderBy;
        }

        public void AddOrderByDesc(Expression<Func<T, object>> orderByDesc)
        {
            OrderByDesc = orderByDesc;
        }

        public void ApplyPagination(int skip, int take)
        {
            IsPaginationEnabled = true;
            
                Skip = skip;
                Take = take;
            
        }
    }
}
