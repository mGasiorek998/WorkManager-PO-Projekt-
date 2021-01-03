using System.Collections.Generic;
using System.Linq;
using WorkManager.Models;

namespace WorkManager.Data
{
    class TaskRepository : IRepository<Models.Task>
    {
        private readonly wmDBContext _context;

        public TaskRepository( wmDBContext context )
        {
            this._context = context;
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

        public void RemoveTask(Models.Task entity) {
            this._context.Tasks.Remove(entity);
        }

        public IEnumerable<Models.Task> GetUsersTasks(int userId) {
            var query = this._context.Tasks
                .Where(t => t.userID == userId);

            return query.ToList();
        }

        public void Save()
        {
            this._context.SaveChanges();
        }
    }
}
