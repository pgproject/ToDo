using System.Collections.ObjectModel;
using ToDo.Database;

namespace ToDo.Tasks;

public partial class ListOfTask : ContentPage
{
    public ObservableCollection<TaskToDo> ToDoTask { get; set; } = new ObservableCollection<TaskToDo>();

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
        var items = await m_toDoDataBase.GetItemsAsync();

        ToDoTask.Clear(); 

        foreach (var item in items)
        {
            ToDoTask.Add(item);
        }
    }

    private async void OnTaskAdd(object sender, EventArgs e)
    {
        TaskToDo task = new TaskToDo(ToDoTask.Count, EntryToDoTask.Text)
        {
            IsChecked = false
        };

        ToDoTask.Add(task);
        EntryToDoTask.Text = "";

        await m_toDoDataBase.SaveItemAsync(task);
        await Shell.Current.GoToAsync("..");
    }

    private async void OnCheckBoxClicked(object sender, CheckedChangedEventArgs e)
    {
        bool isChecked = e.Value; 

        var checkBox = (CheckBox)sender;
        var task = (TaskToDo)checkBox.BindingContext;

        task.IsChecked = isChecked;
        if (e.Value)
        {
            await Task.Delay(ListOfTaskExtensions.DELAY_TIME_MS);

            ToDoTask.Remove(task);

            await m_toDoDataBase.DeleteItemAsync(task);
            await Shell.Current.GoToAsync("..");
        }

    }
}