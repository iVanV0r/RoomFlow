using Microsoft.EntityFrameworkCore;
using RoomFlow.Application.Builder;
using RoomFlow.Application.Builder.Interfaces;
using RoomFlow.Application.Interfaces;
using RoomFlow.Application.Services;
using RoomFlow.Data;
using RoomFlow.Infrastructure;
using RoomFlow.Infrastructure.Interfaces;
using RoomFlow.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//// ����������� � ���� ������ SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// �����������
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IBookingService, BookingService>();


// UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// EmployeeService
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddScoped<IEmployeeBuilder, EmployeeBuilder>();

var app = builder.Build();

//// �������������� �������� �� ��� �������
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate(); // �������� ��� ��������, ���� ��� ����
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}")
    .WithStaticAssets();

app.Run();
