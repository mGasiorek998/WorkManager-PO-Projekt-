using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkManager.Models;

namespace WorkManager.Data {
    class UserRepository : IRepository<User> {

        private readonly wmDBContext _context;

        public UserRepository( wmDBContext context ) {
            this._context = context;
        }

        public bool Add( User entity ) {
            this._context.Users.Add(entity);
            return true;
        }

        public IEnumerable<User> GetAll() {
            return this._context.Users.ToList();
        }

        public User GetById( int id ) {
            return this._context.Users.Find(id);
        }

        public User GetByEmail(String email) {
            User user = this._context.Users.Find(email);

            return user;
        }

        public void Save() {
            this._context.SaveChanges();
        }
    }
}
