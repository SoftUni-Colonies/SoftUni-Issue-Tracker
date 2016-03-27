using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIT.Data;
using SIT.Data.Interfaces;

namespace SIT.Data.Repositories
{
    public class EntityRepository<TEntity> : AbstractRepository<TEntity, int> where TEntity : class, IDentificatable<int>
    {
        public EntityRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
