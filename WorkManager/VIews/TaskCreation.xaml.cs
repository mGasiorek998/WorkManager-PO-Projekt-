using System;
using System.Linq;
using System.Windows;
using WorkManager.Data;
using WorkManager.Models;

namespace WorkManager.VIews
{
    //possible types of task statuses
    public enum TaskStatus
    {
        New,
        InProgress,
        Postponed,
        Done
    }

    /// <summary>
    /// Interaction logic for TaskCreation.xaml
    /// </summary>
    public partial class TaskCreation : Window
    {
        private wmDBContext context;
        private TaskRepository taskRepo;
        private User currentUser;

        public TaskCreation(User user)
        {
            this.context = new wmDBContext();
            this.taskRepo = new TaskRepository(context);
            this.currentUser = user;
            
            InitializeComponent();
        }



        //implementation of the button that adds tasks
        public void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            //binding gui fields to values
            string title, description;
            title = AddTitle.Text;
            description = AddDescription.Text;
            DateTime dueDate;
            
            // Check if correct dueDate was passed:
            try {
                dueDate = (DateTime)DueDateCalendar.SelectedDate;
            } catch(InvalidOperationException err) {
                MessageBox.Show("Check the due date!");
                return;
            }
            

            //creating a task if fields are filled
            bool isTaskValid = ValidateTask(title, description, dueDate);

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

                Task newTask = builder.Build();

                // Add it to the repo:
                taskRepo.Add(newTask);
                taskRepo.Save();

                MessageBox.Show("Task created successfully!");
                ReloadTaskList();
            }
        }

        //validating if fields are filled
        private bool ValidateTask(string title, string description, DateTime dueDate)
        {
            if (title == "" || description == "")
            {
                MessageBox.Show("Fill out the required fields");
                return false;
            }
            else return true;
        }


        private void ListOfTasks_Initialized(object sender, EventArgs e)
        {
            ReloadTaskList();
        }

        private void ReloadTaskList()
        {
            // Get all logged in user tasks:
            var tasks = taskRepo.GetUsersTasks(this.currentUser.Id);
            
            // Check if any task has 'DONE' Status if so delete it from database:
            foreach (Task task in tasks) {
                if (Enum.Parse(typeof(TaskStatus), task.Status).Equals(TaskStatus.Done)) {
                    taskRepo.RemoveTask(task);
                }
            }
            this.taskRepo.Save();

            // Attached them to ListView
            ListOfTasks.ItemsSource = tasks;
        }

        private void StatusDropMenu_Initialized(object sender, EventArgs e)
        {
            StatusDropMenu.ItemsSource = Enum.GetValues(typeof(TaskStatus)).Cast<TaskStatus>();
            StatusDropMenu.SelectedItem = TaskStatus.Done;
        }

        private void ChangeStatusButton_Click(object sender, RoutedEventArgs e)
        {
            Task selecedtask = (Task)ListOfTasks.SelectedItem;
            TaskStatus selectedstatus = (TaskStatus)StatusDropMenu.SelectedItem;

            // Check if any task was makred:
            if(selecedtask == null) {
                MessageBox.Show("Choose a task to update!");
                return;
            }
            selecedtask.Status = selectedstatus.ToString();
            taskRepo.Save();
            MessageBox.Show("Status changed succesfully!");
            ReloadTaskList();
        }
    }
}
