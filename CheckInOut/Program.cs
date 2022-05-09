var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHostedService<CheckInService.CheckInService>();

var app = builder.Build();

// Configure the HTTP request pipeline.



app.Run();