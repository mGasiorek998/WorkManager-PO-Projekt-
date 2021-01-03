using System;
using System.Windows;
using WorkManager.Data;
using WorkManager.Models;

namespace WorkManager.VIews
{
    /// <summary>
    /// Logika interakcji dla klasy Auth.xaml
    /// </summary>
    public partial class Auth : Window {
        private wmDBContext context;
        private UserRepository userRepo;

        public Auth() {
            this.context = new wmDBContext();
            this.userRepo = new UserRepository(context);

            InitializeComponent();
            
        }

        private void RegisterButton_Click( object sender, RoutedEventArgs e ) {
            // Get the Form inputs values:
            String username, email, password;
            username = RegisterUsername.Text;
            email = RegisterEmail.Text;
            password = RegisterPassword.Password.ToString();

            // Check if any of the value was missed
            bool isFormValid = ValidateRegisterForm(username, email, password);

            // If Form is valid add new user to database:
            if(isFormValid) { 
                // Build a new user and add it to the database:
                UserBuilder builder = new UserBuilder();

                builder.SetEmail(email).SetUsername(username).SetPassword(password);
                User newUser = builder.Build();
                
                userRepo.Add(newUser);
                userRepo.Save();

                MessageBox.Show($"{username} created!");

                // Redirect user to the Task Mamanger:
                TaskCreation tc = new TaskCreation(newUser);
                tc.Show();
                this.Close();
            }
        }

        private bool ValidateRegisterForm(String username, String email, String password) {
            /* 
                Function to validate a Register Form inputs
                Returs True if form is valid
            */
            if (username == "" || email == "" || password == "") {
                MessageBox.Show("Invalid Register Form Inputs!");
                return false;
            }

            else if (!ValidateEmail(email)) {
                MessageBox.Show($"{email} as Email is incorect.");
                return false;
            }
            else if (!ValidateUsername(username)) {
                MessageBox.Show($"{username} cannot have any ' '.");
                return false;
            }
            else if (!ValidatePassword(password)) {
                MessageBox.Show($"Password should be between 5 and 20 characters!");
                return false;
            }

            // Check if given email or username already exists in database
            var users = userRepo.GetAll();

            foreach (User user in users) {
                if (user.Email == email || user.Username == username) {
                    MessageBox.Show("This user already exists!");
                    return false;
                }
            }

            return true;
        }

        private bool ValidateEmail(String value) {
            /* 
                Function is checking if email contains '@' and '.'.
                Return true if passed value is correct  
             */
            if (value.Contains("@") && value.Contains(".")) {
                return true;
            }
            return false;
        }

        private bool ValidateUsername( String value ) {
            /* 
                Function is checking if username has a whitespace.
                Return false if so.  
             */
            if (!value.Contains(" ")) {
                return true;
            }
            return false;
        }

        private bool ValidatePassword( String value ) {
            /* 
                Function is checking if password is beetwien 5 and 20 characters.
                Return false password is too short or too long.  
             */
            if (value.Length >= 5 && value.Length <= 20) {
                return true;
            }
            return false;
        }

        private void LoginButton_Click( object sender, RoutedEventArgs e ) {
            // Get the Form inputs values:
            String email, password;
            email = LoginEmail.Text;
            password = LoginPassword.Password.ToString();

            User user = CheckIfUserExists(email);

            // CHECK IF USER PASSED CORRECT VALUES:
            if(user == null) {
                MessageBox.Show("Incorrect username or password!");
            } else if (user.Password == password) {
                MessageBox.Show("Logging in...Click OK");
                TaskCreation taskCreation = new TaskCreation(user);
                taskCreation.Show();
                this.Close();
            } else {
                MessageBox.Show("Incorrect username or password!");
            }


        }

        private User CheckIfUserExists(String email) {
            var users = userRepo.GetAll();

            foreach (User user in users) {
                if(user.Email == email) {
                    return user;
                }
            }

            return null;
        }
    }
}
