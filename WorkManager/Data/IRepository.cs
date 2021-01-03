using System.Collections.Generic;

namespace WorkManager.Data
{
    interface IRepository<T> where T : class{

        bool Add( T entity );
        IEnumerable<T> GetAll();
        T GetById( int id );
        void Save();
    }
}
