using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ToDo.Database;

namespace ToDo.Tasks;

public partial class ListOfTask : ContentPage
{
    public ObservableCollection<TaskToDo> ToDoTasks { get; set; } = new ObservableCollection<TaskToDo>();
    public ObservableCollection<TaskToDo> DoneTasks { get; set; } = new ObservableCollection<TaskToDo>();

    private ToDoDatabase m_toDoDataBase;
    private Color m_defaultPlaceHolderColor;

    public ListOfTask()
	{
        InitializeComponent();
        LoadDataBase();

        BindingContext = this;
        m_defaultPlaceHolderColor = EntryTaskTitle.PlaceholderColor;
    }

    private async void LoadDataBase()
    {
        m_toDoDataBase = new ToDoDatabase();

        var allTask = await m_toDoDataBase.GetAllTaskAsync();
        
        ToDoTasks.Clear();
        foreach (var task in allTask)
        {
            ToDoTasks.Add(task);
        }
    }

    private async void OnTaskAdd(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(EntryTaskTitle.Text))
        {
            EntryTaskTitle.Placeholder = ListOfTaskExtensions.MISSING_TITLE;
            EntryTaskTitle.PlaceholderColor = new Color(255, 0, 0, 0.5f);
            return;
        }

        TaskToDo task = new TaskToDo(ToDoTasks.Count, EntryTaskTitle.Text, EnterTaskDescription.Text, false);
        ToDoTasks.Add(task);

        EntryTaskTitle.Text = "";
        EntryTaskTitle.Placeholder = ListOfTaskExtensions.SET_TITLE;
        EntryTaskTitle.PlaceholderColor = m_defaultPlaceHolderColor;
        EnterTaskDescription.Text = "";

        await m_toDoDataBase.SaveTaskAsync(task);
        await Shell.Current.GoToAsync("..");
    }
    private async void OnDeleteTask(object sender, EventArgs e)
    {
        var buttom = (Button)sender;
        var task = (TaskToDo)buttom.BindingContext;

        await Task.Delay(ListOfTaskExtensions.DELAY_TIME_MS);

        if (task.IsDone)
            DoneTasks.Remove(task);
        else
            ToDoTasks.Remove(task);

        await m_toDoDataBase.DeleteTaskAsync(task);
        await Shell.Current.GoToAsync("..");
    }

    private async void OnCheckBoxClicked(object sender, CheckedChangedEventArgs e)
    {
        if (sender is CheckBox checkBox && checkBox.BindingContext is TaskToDo task)
        {
            task.IsDone = e.Value;

            if (task.IsDone && DoneTasks.Contains(task)) return;
            if (!task.IsDone && ToDoTasks.Contains(task)) return;


            if (task.IsDone)
            {
                ToDoTasks.Remove(task);
                DoneTasks.Add(task);
            }
            else
            {
                ToDoTasks.Add(task);
                DoneTasks.Remove(task);
            }
            await m_toDoDataBase.UpdateTaskAsync(task);
        }
    }
}