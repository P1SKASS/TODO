using GraphQL;
using GraphQL.Types;
using System.Threading.Tasks;
using TODO.Models;
using TODO.Services;

public class TodoMutation : ObjectGraphType
{
    public TodoMutation(ITaskService taskService)
    {
        Field<TodoType>("addTask")
            .Arguments(new QueryArguments(
                new QueryArgument<TodoType> { Name = "taskName" }))
            .ResolveAsync(async context =>
            {
                var title = context.GetArgument<string>("taskName");
                var task = new TaskForList { TaskName = title, Completed = false };
                await taskService.AddTask(task);
                return task;
            });

        Field<BooleanGraphType>("deleteTask")
            .Arguments(new QueryArguments(
                new QueryArgument<IntGraphType> { Name = "id" }))
            .ResolveAsync(async context =>
            {
                var id = context.GetArgument<int>("id");
                await taskService.DeleteTask(id);
                return id;
            });

        Field<TodoType>("updateTask")
            .Arguments(new QueryArguments(
                new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" },
                new QueryArgument<StringGraphType> { Name = "taskName" },
                new QueryArgument<BooleanGraphType> { Name = "completed" }))
            .ResolveAsync(async context =>
            {
                var id = context.GetArgument<int>("id");
                var title = context.GetArgument<string>("taskName");
                var completed = context.GetArgument<bool?>("completed");
                var task = new TaskForList { Id = id, TaskName = title, Completed = completed ?? false };
                await taskService.UpdateTask(task);
                return task;
            });
    }
}
