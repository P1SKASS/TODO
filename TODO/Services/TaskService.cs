using System.Collections.Generic;
using System.Linq;
using TODO.Models;

namespace TODO.Services
{
    public class TaskService : ITaskService
    {
        private readonly List<TaskForList> _tasks = new List<TaskForList>();

        public IEnumerable<TaskForList> GetTasks()
        {
            return _tasks;
        }

        public async Task<TaskForList> AddTask(TaskForList task)
        {
            _tasks.Add(task);
            await Task.CompletedTask;
            return task;
        }

        public async Task<bool> DeleteTask(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                _tasks.Remove(task);
                await Task.CompletedTask;
                return true;
            }
            await Task.CompletedTask;
            return false;
        }

        public async Task<TaskForList> UpdateTask(TaskForList task)
        {
            var existingTask = _tasks.FirstOrDefault(t => t.Id == task.Id);
            if (existingTask != null)
            {
                existingTask.TaskName = task.TaskName;
                existingTask.Completed = task.Completed;
            }
            await Task.CompletedTask;
            return task;
        }
    }

}
