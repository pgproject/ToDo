using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ToDo.Tasks
{
    public class TaskToDo 
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string TitleTask { get; set; }
        public string DescriptionTask { get; set; }
        public bool IsDone { get; set; }

        public TaskToDo()
        {

        }

        public TaskToDo(int id, string titleTask, string descriptionTask, bool isDone)
        {
            this.Id = id;
            this.TitleTask = titleTask;
            this.DescriptionTask = descriptionTask;
            this.IsDone = isDone;
        }
    }
}
