using BillingReminder.Application.Services;
using BillingReminder.Application.Interfaces;
using BillingReminder.Domain.Interfaces;
using BillingReminder.Infrastructure.Email;
using BillingReminder.Infrastructure.Email.Settings;
using BillingReminder.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mongoSection = builder.Configuration.GetSection("Mongo");
var mongoSettings = mongoSection.Get<MongoSettings>()!;
builder.Services.AddSingleton(mongoSettings);

builder.Services.AddSingleton<IInvoiceRepository, MongoInvoiceRepository>();
builder.Services.AddSingleton<IEmailSender, SmtpEmailSender>();
builder.Services.AddScoped<IReminderProcessor, ReminderProcessor>();


builder.Services.Configure<SmtpSettings>(
    builder.Configuration.GetSection("SmtpSettings"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("dev", p => p
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin()
    );
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

app.UseCors("dev");

app.MapControllers();

app.Run();
