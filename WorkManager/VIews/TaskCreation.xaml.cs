using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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

        public TaskCreation()
        {
            InitializeComponent();
            this.context = new wmDBContext();
            this.taskRepo = new TaskRepository(this.context);
        }

        //implementation of the button that adds tasks
        public void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            //filling the tasks required fields
            string title, description;
            title = AddTitle.Text;
            description = AddDescription.Text;
            DateTime dueDate = (DateTime) DueDateCalendar.SelectedDate;

            //creating a task if fields are filled
            bool isTaskValid = ValidateTask(title, description, dueDate);

            if (isTaskValid)
            {
                var newTask = new Models.Task
                {
                    TaskTitle = title,
                    TaskDesc = description,
                    CreationDate = DateTime.Now,
                    DueDate = dueDate,
                    Status = TaskStatus.New.ToString()
                    // TODO: user
                };
                taskRepo.Add(newTask);
                taskRepo.Save();

                MessageBox.Show("Task created succesfully!");
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
    }
}
