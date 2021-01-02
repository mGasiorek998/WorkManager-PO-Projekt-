using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkManager.Models;

namespace WorkManager.Data
{
    class TaskRepository : IRepository<Models.Task>
    {
        private readonly wmDBContext _context;

        public TaskRepository()
        {
            this._context = wmDBContext.GetInstance();
        }

        public bool Add(Models.Task entity)
        {
            this._context.Tasks.Add(entity);
            return true;
        }

        public IEnumerable<Models.Task> GetAll()
        {
            return this._context.Tasks.ToList();
        }

        public Models.Task GetById(int id)
        {
            return this._context.Tasks.Find(id);
        }

        public void Save()
        {
            this._context.SaveChanges();
        }
    }
}
