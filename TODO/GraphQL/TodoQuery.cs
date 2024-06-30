using GraphQL.Types;
using TODO.Models;
using TODO.Services;

public class TodoQuery : ObjectGraphType
{
    public TodoQuery(ITaskService taskService)
    {
        Field<ListGraphType<TodoType>>(
            "tasks")
            .Resolve(context => taskService.GetTasks()
        );
    }
}
