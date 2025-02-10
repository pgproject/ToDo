using SQLite;

namespace ToDo.Tasks
{
    public class TaskToDo
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string TextTask { get; set; }
        public bool IsChecked { get; set; } = false;
        public TaskToDo()
        {

        }
        
        public TaskToDo(int id, string textTask)
        {
            this.Id = id;
            this.TextTask = textTask;
        }
    }
}
