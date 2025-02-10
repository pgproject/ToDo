using SQLite;

namespace ToDo.Tasks
{
    public class TaskToDo
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string TitleTask { get; set; }
        public string DescriptionTask { get; set; }
        public bool IsChecked { get; set; } = false;
        public TaskToDo()
        {

        }
        
        public TaskToDo(int id, string titleTask, string descriptionTask)
        {
            this.Id = id;
            this.TitleTask = titleTask;
            this.DescriptionTask = descriptionTask;
        }
    }
}
