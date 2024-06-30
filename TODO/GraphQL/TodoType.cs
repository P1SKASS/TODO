using GraphQL.Types;
using TODO.Models;

public class TodoType : ObjectGraphType<TaskForList>
{
    public TodoType()
    {
        Field(x => x.Id).Description("The ID of the Task.");
        Field(x => x.TaskName).Description("The title of the Task.");
        Field(x => x.Completed).Description("The status of the Task.");
    }
}
