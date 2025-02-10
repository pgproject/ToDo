using System.Collections.ObjectModel;

namespace ToDo.Task;

public partial class ListOfTask : ContentPage
{
    public ObservableCollection<TaskToDo> ToDoTask { get; set; } = new ObservableCollection<TaskToDo>();

    public ListOfTask()
	{
        InitializeComponent();

        BindingContext = this;
    }

    private void OnTaskAdd(object sender, EventArgs e)
    {
        TaskToDo task = new TaskToDo(ToDoTask.Count, EntryToDoTask.Text)
        {
            IsChecked = false
        };

        ToDoTask.Add(task);
        EntryToDoTask.Text = "";
    }

 
    private async void OnCheckBoxClicked(object sender, CheckedChangedEventArgs e)
    {
        bool isChecked = e.Value; 

        var checkBox = (CheckBox)sender;
        var task = (TaskToDo)checkBox.BindingContext;

        task.IsChecked = isChecked;
        if (e.Value)
        {
            await System.Threading.Tasks.Task.Delay(ListOfTaskExtensions.DELAY_TIME_MS);

            ToDoTask.Remove(task);
        }
    }
}