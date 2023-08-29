using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MStore.Core.Entities;

namespace MStore.Core.IRepository
{
    public interface IUnitOfWork: IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity: BaseEntity;

        Task<int> Complete();
    }
}
