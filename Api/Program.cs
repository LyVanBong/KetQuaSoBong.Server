using Api.BackgroundServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var service = builder.Services;

service.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
service.AddEndpointsApiExplorer();
service.AddSwaggerGen();

service.AddHostedService<ClearDataService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
