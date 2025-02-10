using SQLite;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices.Marshalling;
using ToDo.Tasks;

namespace ToDo.Database
{
    public class ToDoDatabase
    {
        private SQLiteAsyncConnection m_database;

        public ToDoDatabase()
        {

        }

        private async Task Init()
        {
            if (m_database is not null)
                return;

            m_database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await m_database.CreateTableAsync<TaskToDo>();
        }

        public async Task<List<TaskToDo>> GetAllTaskAsync()
        {
            await Init();
            return await m_database.Table<TaskToDo>().ToListAsync();
        }

        public async Task<int> SaveTaskAsync(TaskToDo task)
        {
            await Init();
            return await m_database.InsertAsync(task);
        }
        public async Task<int> UpdateTaskAsync(TaskToDo task)
        {
            await Init();
            return await m_database.UpdateAsync(task);
        }

        public async Task<int> DeleteTaskAsync(TaskToDo task)
        {
            await Init();
            return await m_database.DeleteAsync(task);
        }

        public async Task<List<TaskToDo>> GetTaskToDoAsync()
        {
            await Init();
            return await m_database.Table<TaskToDo>().Where(t => t.IsDone).ToListAsync();
        }

        public async Task<List<TaskToDo>> GetDoneTaskAsync()
        {
            await Init();
            return await m_database.Table<TaskToDo>().Where(t => !t.IsDone).ToListAsync();
        }

    }
}