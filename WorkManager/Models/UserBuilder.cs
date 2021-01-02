using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkManager.Models {
    class UserBuilder {
        private String email;
        private String username;
        private String password;

        public UserBuilder SetEmail( string value ) {
            this.email = value;
            return this;
        }

        public UserBuilder SetUsername(string value) {
            this.username = value;
            return this;
        }

        public UserBuilder SetPassword( string value ) {
            this.password = value;
            return this;
        }

        public User Build() {
            return new User {
                Username = this.username,
                Email = this.email,
                Password = this.password
            };
        }
    }
}
