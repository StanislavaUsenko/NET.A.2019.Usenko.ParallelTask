using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinet.Database.Core.Interfaces
{
    public interface IExport<T>
    {
        bool SaveAll(List<T> objects);
    }
}
