using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Core.Work
{
    public interface IUnitOfWork
    {
        void Save();
    }
}
