using JPI.SmartDomainDesign.Api;
using JPI.SmartDomainDesign.Application;

// Configure Services

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureApplication();

builder.Services.AddControllers();

builder.Services.ConfigureApi();


// Configure App

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", ApplicationConstants.AppName);
        options.RoutePrefix = "docs";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
