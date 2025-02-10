namespace ToDo.Task
{
    public class TaskToDo
    {
        public int Id { get; private set; }
        public string TextTask { get; private set; }
        public bool IsChecked { get; set; } = false;
        public TaskToDo(int id, string textTask)
        {
            this.Id = id;
            this.TextTask = textTask;
        }
    }
}
