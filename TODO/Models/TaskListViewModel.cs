namespace TODO.Models
{
    public class TaskListViewModel
    {
        public List<TaskForList> TaskList { get; set; }
        public TaskForList NewTask { get; set; }
        public string StorageMode { get; set; } = "Database";
    }
}