using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkManager.Data {
    interface IRepository<T> where T : class{

        bool Add( T entity );
        IEnumerable<T> GetAll();
        T GetById( int id );
        void Save();
    }
}
