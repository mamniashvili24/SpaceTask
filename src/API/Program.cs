using Mapper;
using Hangfire;
using API.Headers;
using Application;
using CQRS.Query.Abstraction;
using Application.Commands.WatchlistRecuringJob;
using CQRS.Command.Abstraction;

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

var commandBus = builder.Services.BuildServiceProvider().GetService<ICommandBus>();

RecurringJob.AddOrUpdate(() => commandBus.SendAsync(new WatchlistRecuringJob(), default), Cron.Weekly(DayOfWeek.Sunday, 19, 30));

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