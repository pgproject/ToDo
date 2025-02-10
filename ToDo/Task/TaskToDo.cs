using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ToDo.Tasks
{
    public class TaskToDo : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string TitleTask { get; set; }
        public string DescriptionTask { get; set; }
        private bool m_isDone;
        public bool IsDone
        {
            get => m_isDone;
            set
            {
                if (m_isDone == value) return;
                m_isDone = value;
                OnPropertyChanged(nameof(IsDone));
            }
        }

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

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
            
    }
}
