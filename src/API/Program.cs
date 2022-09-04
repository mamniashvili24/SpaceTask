using Mapper;
using Application;
using API.BackgroundServices;
using API.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
    option.OperationFilter<LanguageHeader>()
);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMapperWithProfiles();
builder.Services.AddHostedService<RegisterHangfireJob>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.ErrorExceptionMiddleware();

app.MapControllers();

app.Run();