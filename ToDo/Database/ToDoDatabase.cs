using SQLite;
using ToDo.Tasks;

namespace ToDo.Database
{
    public class ToDoDatabase
    {
        SQLiteAsyncConnection Database;

        public ToDoDatabase()
        {

        }

        private async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<TaskToDo>();
        }

        public async Task<List<TaskToDo>> GetItemsAsync()
        {
            await Init();
            return await Database.Table<TaskToDo>().ToListAsync();
        }

        public async Task<int> SaveItemAsync(TaskToDo item)
        {
            await Init();
           
            return await Database.InsertAsync(item);
        }

        public async Task<int> DeleteItemAsync(TaskToDo item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }
    }
}