using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace WorkManager.Models {
    class wmDBContext : DbContext{

        private static wmDBContext instance;

        private wmDBContext() { }

        public static wmDBContext GetInstance()
        {
            if (wmDBContext.instance == null)
            {
                wmDBContext.instance = new wmDBContext();
            }
            return wmDBContext.instance;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
    }
}
