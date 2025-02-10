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
                m_isDone = value;
                OnPropertyChanged();
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
