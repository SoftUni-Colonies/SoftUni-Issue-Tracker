using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIT.Data.Interfaces
{
    public interface IDentificatable<T>
    {
        T Id { get; }
    }
}
