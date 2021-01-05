# WorkManager
## Projekt zaliczeniowy "Programowanie Obiektowe"

## Spis Treści
1. (https://github.com/mGasiorek998/WorkManager/blob/main/README.md#opis-aplikacji Opis Aplikacji)
1. #Komunikacja aplikacji z bazą danych
   1. Modele
   1. Repozytoria
1. Tworzenie i autoryzacja użytkownika
   1. Rejestracja
   1. Logowanie
1. Zarządzanie Taskami
   1. Dodawanie zadań
   1. Wyświetlanie zadań
   1. Edycja statsów
1. Użyte Technologie
   
### OPIS APLIKACJI
   Aplikacja umożliwa lepszą organizację pracy. Użytkownik w prosty sposób będzie mógł dodawać do swojej listy zadania, a potem zmieniać ich statusy. 

### KOMUNIKACJA APLIKACJI Z BAZĄ DANCYH
   #### Modele
   Modele, które zostały wykorzystane do stworzenia aplikacji to:
   ##### User
   ```cs
   namespace WorkManager.Models {
    public class User {
        [Key]
        public int Id {get; set;}
        
        [Required]
        [MaxLength(50)]
        public String Email { get; set; }
        
        [Required]
        [MaxLength(20)]
        public String Username { get; set; }

        [Required]
        [MaxLength(20)]
        public String Password { get; set; }
    }
}
   ```
   Każdy użytknownik ma:
   * ID
   * Email
   * Username
   * Password
   
   ##### Task
   ```cs
   namespace WorkManager.Models
   {
    class Task {
          [Key]
          public int Id { get; set; }

          [Required]
          public int userID { get; set; }

          [Required]
          [MaxLength(100)]
          public String TaskTitle { get; set; }

          [Required]
          [MaxLength(420)]
          public String TaskDesc { get; set; }

          public DateTime CreationDate { get; set; }

          [Required]
          public DateTime DueDate { get; set; }

          [Required]
          public String Status { get; set; }

       }
   }
```
    Każde zadanie ma:
   * ID
   * Tytuł
   * Opis
   * Date utworzenia
   * Deadline
   * Status
   * UserId
   
   Dzięki polu userID po wczytywaniu listy zadań wyświetlają się tylko i wyłącznie zadania stworzone przez użytkownika.
   ```cs
   public IEnumerable<Models.Task> GetUsersTasks(int userId) {
            var query = this._context.Tasks
                .Where(t => t.userID == userId);

            return query.ToList();
        }
   ```
   #### Repozytoria
   Każdy z Modeli porozumiewa się z bazą danych za pomocą swoich repozytoriów.
   
   #### UserRepository
   ```cs
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
   ```
   
   #### TaskRepository
   ```cs
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
   ```
### REJESTRACJA I AUTORYZACJA UŻYTKOWNIKA
   #### Rejestracja
   Za nim użytkownich będzie mógł zacząć korzystać z Work Managera będzie on musiał założyć kotno podając:
   * Email
   * Nazwę użytkownika
   * Hasło
   
   Aplikacja weryfikuje podane przez użytkownia dane tak aby upewnić się, że hasło jest wystarczająco mocne i czy e-mail jest prawidłowy.
   Jeżeli wszystkie dane zostały podane poprawnie i jeżeli podany e-mail nie istnieje w bazie użytkownik zostaje stworzony.
   Użytkownik po pomyślnej rejestracji od razu zostanie przeniesony do okna z polami tekstowymi oraz pustą listą, na której docelowo znajdą się zadania.
   ```cs
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
   ```
   #### Logowanie
   Aby zalogować się na swoje konto użytkownik musi podać:
   * Email
   * Hasło
   ```cs
        private void LoginButton_Click( object sender, RoutedEventArgs e ) {
            // Get the Form inputs values:
            String email, password;
            email = LoginEmail.Text;
            password = LoginPassword.Password.ToString();

            User user = CheckIfUserExists(email);

            // CHECK IF USER PASSED CORRECT VALUES:
            if(user == null) {
                MessageBox.Show("Niepoprawna nazwa użytkownika albo hasło!");
            } else if (user.Password == password) {
                MessageBox.Show("Loguje...");
                TaskCreation taskCreation = new TaskCreation(user);
                taskCreation.Show();
                this.Close();
            } else {
                MessageBox.Show("Niepoprawna nazwa użytkownika albo hasło!");
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
        
   ```
   Po zweryfikowaniu czy dany użytkownik istnieje w bazie zostaje on przekierowany do swojej listy.

### ZARZĄDZANIE ZADANIAMI

#### Dodawanie zadań
Poprzez wypełnienie na formularzu TaskCreation pól tekstowych Title i Description oraz wybranie terminu, przed którym zrealizowany powinien zostać dany cel, użytkownik może dodać zadanie. Wszystkie z wymienionych wartości są wartościami wymaganymi, a aplikacja nie dopuści do przypadku, w którym dodane zostaje zagadanienie o niepełnych danych. 
```cs 
// Check if correct dueDate was passed:
            try {
                dueDate = (DateTime)DueDateCalendar.SelectedDate;
            } catch(InvalidOperationException err) {
                MessageBox.Show("NIE ZAZNACZONO DATY TERMINU!");
                return;
            }
            
```
```cs
private bool ValidateTask(string title, string description, DateTime dueDate)
        {
            if (title == "" || description == "")
            {
                MessageBox.Show("Fill out the required fields");
                return false;
            }
            else return true;
        }
```

Po wypełnieniu wyżej wymienionych pól wymaganych, zadanie można dodać poprzez kliknięcie przycisku ADD. W przypadku powodzenia przeprowadzonej operacji, wyświetlone zostaje okno świadczące o sukcesie, rekord zadania zapisany jest w bazie danych z unikatowym identyfikatorem (zarówno własnym jak i użytkownika, który je stworzył), a dodana powinność wyświetla się na liście ze statusem New.
```cs
if (isTaskValid)
            {
                //Buid a new Task
                TaskBuilder builder = new TaskBuilder();

                builder.SetTaskTitle(title)
                    .SetTaskDesc(description)
                    .SetCreationDate()
                    .SetDueDate(dueDate)
                    .SetUserId(currentUser.Id)
                    .SetStatus(TaskStatus.New.ToString());

                Models.Task newTask = builder.Build();

                // Add it to the repo:
                taskRepo.Add(newTask);
                taskRepo.Save();

                MessageBox.Show("Task created succesfully!");
                reloadTaskList();
            }
```

#### Lista zadań
Jeżeli dany użytkownik pomyślnie przeszedł proces dodawania zadań, swoje postępy będzie mógł zaobserwować na liście zadań. Wyświetlane są na niej dane kluczowe dla użytkownika. Widzi on zarówno skrótowe tytuły, jak i bardziej konkretne opisy swoich zadań, a także datę, przed którą powinien je ukończyć. Do listy dodać można nieskończoną ilość wierszy. Jednak aby uniknąć sytuacji nadwyraz długiej listy, kiedy w ostatniej kolumnie danego zadania pojawi się status Done - przy następnym logowaniu nie będzie ono już widoczne - zostanie usunięte.
```cs
private void reloadTaskList()
        {
            // Get all logged in user tasks:
            var tasks = taskRepo.GetUsersTasks(this.currentUser.Id);
            
            // Check if any task has 'DONE' Status if so delete it from database:
            foreach (Models.Task task in tasks) {
                if (Enum.Parse(typeof(TaskStatus), task.Status).Equals(TaskStatus.Done)) {
                    taskRepo.RemoveTask(task);
                }
            }
            this.taskRepo.Save();

            // Attached them to ListView
            ListOfTasks.ItemsSource = tasks;
        }
```
#### Edycja statusów.
Najważniejszym elementem panowania nad własnymi celami jest ich status. Aplikacja umożliwia jego edycję, aby użytkownik mógł odpowiednio oznaczać swoje zadania zgodnie z faktycznym stanem ich realizacji. Po wybraniu wiersza na rejestrze, w lewym dolnym rogu okna aplikacji wybrać można z rozwijanej listy jeden z 4 statusów: {New, InProgress, Postponed, Done}. Domyślną wartością jest Done, aby przyspieszyć proces pomyślnego wykonywania zadań przez pracowników. Kiedy zaznaczony jest odpowiedni wiersz i pożądany status, wybranie przycisku Change status spowoduje nadpisanie stanu zadania oraz wyświetlenie okna potwierdzającego powodzenie.
```cs
private void ChangeStatusButton_Click(object sender, RoutedEventArgs e)
        {
            Models.Task selecedtask = (Models.Task)ListOfTasks.SelectedItem;
            TaskStatus selectedstatus = (TaskStatus)StatusDropMenu.SelectedItem;

            // Check if any task was makred:
            if(selecedtask == null) {
                MessageBox.Show("NIE WYBRANO TASKU!");
                return;
            }
            selecedtask.Status = selectedstatus.ToString();
            taskRepo.Save();
            MessageBox.Show("Status changed succesfully!");
            reloadTaskList();
        }
```
### UŻYTE TECHNOLOGIE:
C#
WPF
Entity Framework
MYSQL
