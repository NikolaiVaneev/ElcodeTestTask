using ElcodeTestTask.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseMySql(configuration.GetConnectionString("DevConnection"), new MySqlServerVersion(new Version(8, 0, 21))));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "����� API",
        Description = "�������� ������� ��� ��� �����",
        Contact = new OpenApiContact
        {
            Name = "�����������",
            Url = new Uri("https://kazan.hh.ru/resume/8ff804ffff08d9d29b0039ed1f78324e683145")
        }

    });

    // ��� ����������� summary ������� � swagger
    // � ���������� ������� �������� �������� �����, ����������� ������������
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();


app.Run();
