using System.Collections.Generic;
using TODO.Models;

namespace TODO.Services
{
    public interface ITaskService
    {
        IEnumerable<TaskForList> GetTasks();
        Task<TaskForList> AddTask(TaskForList task);
        Task<bool> DeleteTask(int id);
        Task<TaskForList> UpdateTask(TaskForList task);
    }
}
