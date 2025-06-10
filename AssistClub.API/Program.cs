using System.Text.Json.Serialization;
using AssistClub.Application;
using Microsoft.AspNetCore.OData;
using Webbist.AideEnLigne.Data;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://*:5284");

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters
                .Add(new JsonStringEnumConverter());
        })
    .AddOData(options =>
        {
            options.Select()
                .Filter()
                .OrderBy()
                .Expand()
                .SetMaxTop(100);
        });
builder.Services.AddRouting(r => r.LowercaseUrls = true);
builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
    app =>
    {
        app.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        //.AllowCredentials();
    })
);

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseAuthorization();
app.MapControllers();
app.UseHttpsRedirection();

app.Run();
