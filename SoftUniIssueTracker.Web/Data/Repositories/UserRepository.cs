using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIT.Data;
using SIT.Data.Interfaces;

namespace SIT.Data.Repositories
{
   public class UserRepository<TEntity> : AbstractRepository<TEntity, string> where TEntity : class, IDentificatable<string>
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
