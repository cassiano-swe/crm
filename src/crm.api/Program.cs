using crm.api.Database;
using crm.api.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.CustomSchemaIds(s=> s.FullName?.Replace('+','.')));

builder.Services.AddDbContext<Context>(o=> o.UseInMemoryDatabase("contactDB"));

builder.Services.AddEndpoints();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapEndpoints();

app.Run();
