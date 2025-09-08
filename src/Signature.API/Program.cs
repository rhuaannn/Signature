using Microsoft.EntityFrameworkCore;
using Signature.API.Filters;
using Signature.Application.Interface;
using Signature.Application.Mapping;
using Signature.Application.Services;
using Signature.Infra.ContextDB;
using Signature.Infra.Interface;
using Signature.Infra.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<Connection>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISignature, ServicesSignature>();
builder.Services.AddScoped<IStudent, ServiceStudent>();
builder.Services.AddScoped<ISignatureRepository, Repository>();
builder.Services.AddScoped<IStudentRepository, Repository>(); // Assumindo que você criou essa classe

builder.Services.AddScoped<Signature.Infra.ContextDB.Connection>();

builder.Services.AddRouting(option => option.LowercaseUrls = true);
builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
