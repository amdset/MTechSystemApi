using MTechSystemApi.DataAccess;
using MTechSystemApi.Filters;
using MTechSystemApi.Infrastructure;
using MTechSystemApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IDataAccess, MysqlDataAccess>();
builder.Services.AddSingleton<IEmployeeService, EmployeeService>();
builder.Services.AddAutoMapper(options =>
           options.AddProfile<MappingProfile>());
builder.Services.AddMvc(options => {
    options.Filters.Add<JsonExceptionFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
