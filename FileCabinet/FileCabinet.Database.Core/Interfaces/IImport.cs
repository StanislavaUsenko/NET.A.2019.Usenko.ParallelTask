using FileCabinet.Database.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FileCabinet.Database.Core.Interfaces
{
    public interface IImport<T> where T : class
    {
        List<T> ReadAll();
    }
}
