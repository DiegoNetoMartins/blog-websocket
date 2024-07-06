using Blog.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddServices();

var app = builder.Build();
app.UseServices();

app.Run();
