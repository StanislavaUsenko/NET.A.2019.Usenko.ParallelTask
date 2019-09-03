using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinet.Database.Core.Interfaces
{
    public interface IRepository<T> where T: class
    {
        int Create(T obj);
        int Update(T obj);
        bool Delete(T obj);
        List<T> GetAll();
        T GetById(int id);
        T GetByTag(string tag, string str);
    }
}
