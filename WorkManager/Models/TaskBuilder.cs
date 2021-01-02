using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkManager.Models {
    class TaskBuilder {
        public int userID;
        public String taskTitle;
        public String taskDesc;
        public DateTime creationDate;
        public DateTime dueDate;
        public String status;

        public TaskBuilder SetUserId(int value ) {
            this.userID = value;
            return this;
        }

        public TaskBuilder SetTaskTitle( string value ) {
            this.taskTitle = value;
            return this;
        }

        public TaskBuilder SetTaskDesc( string value ) {
            this.taskDesc = value;
            return this;
        }

        public TaskBuilder SetCreationDate() {
            this.creationDate = DateTime.Now;
            return this;
        }

        public TaskBuilder SetDueDate( DateTime value ) {
            this.dueDate = value;
            return this;
        }
        public TaskBuilder SetStatus( string value ) {
            this.status = value;
            return this;
        }

        public Task Build() {
            return new Models.Task {
                TaskTitle = this.taskTitle,
                TaskDesc = this.taskDesc,
                CreationDate = this.creationDate,
                DueDate = this.dueDate,
                Status = this.status,
                userID = this.userID,
            };
        }
    }
}
