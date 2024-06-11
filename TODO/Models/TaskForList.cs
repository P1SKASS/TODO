namespace TODO.Models
{
    public class TaskForList
    {
        public int Id { get; set; }
        public required string TaskName { get; set; }
        public DateTime? Deadline { get; set; }
        public bool Completed { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
