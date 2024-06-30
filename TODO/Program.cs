using Microsoft.EntityFrameworkCore;
using TODO.GraphQL;
using TODO.Models;
using GraphQL;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<SiteContex>(options =>
{
    options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TODO-List;Integrated Security=True;Encrypt=True");
});

builder.Services.AddGraphQL(b => b
    .AddAutoSchema<GraphQLSchema>()
    .AddSystemTextJson());


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseDeveloperExceptionPage();
app.UseWebSockets();
app.UseGraphQL("/graphql");            // url to host GraphQL endpoint
app.UseGraphQLPlayground(
    "/",                               // url to host Playground at
    new GraphQL.Server.Ui.Playground.PlaygroundOptions
    {
        GraphQLEndPoint = "/graphql",         // url of GraphQL endpoint
        SubscriptionsEndPoint = "/graphql",   // url of GraphQL endpoint
    });

app.Run();
