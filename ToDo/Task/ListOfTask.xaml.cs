using System.Collections.ObjectModel;
using ToDo.Database;

namespace ToDo.Tasks;

public partial class ListOfTask : ContentPage
{
    public ObservableCollection<TaskToDo> Tasks { get; set; } = new ObservableCollection<TaskToDo>();

    private ToDoDatabase m_toDoDataBase;

    public ListOfTask()
	{
        InitializeComponent();

        m_toDoDataBase = new ToDoDatabase();
        LoadDataBase();

        BindingContext = this;
    }

    private async void LoadDataBase()
    {
        var allTask = await m_toDoDataBase.GetAllTaskAsync();

        Tasks.Clear(); 
        
        foreach (var task in allTask)
        {
            Tasks.Add(task);
        }
        Tasks.OrderBy(x => !x.IsDone).ToList();
    }

    private async void OnTaskAdd(object sender, EventArgs e)
    {
        TaskToDo task = new TaskToDo(Tasks.Count, EntryTaskTitle.Text, EnterTaskDescription.Text, false);
        
        Tasks.Add(task);
        EntryTaskTitle.Text = "";
        EnterTaskDescription.Text = ""; 

        await m_toDoDataBase.SaveTaskAsync(task);
        await Shell.Current.GoToAsync("..");
    }
    private async void OnDeleteTask(object sender, EventArgs e)
    {
        var buttom = (Button)sender;
        var task = (TaskToDo)buttom.BindingContext;

        await System.Threading.Tasks.Task.Delay(ListOfTaskExtensions.DELAY_TIME_MS);
        Tasks.Remove(task);

        await m_toDoDataBase.DeleteTaskAsync(task);
        await Shell.Current.GoToAsync("..");
    }

    private async void OnCheckBoxClicked(object sender, CheckedChangedEventArgs e)
    {
        if (sender is CheckBox checkBox && checkBox.BindingContext is TaskToDo task)
        {
            task.IsDone = e.Value;
            await m_toDoDataBase.UpdateTaskAsync(task);
        }
    }
}