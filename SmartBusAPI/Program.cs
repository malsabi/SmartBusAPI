using SmartBusAPI;
using SmartBusAPI.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddPresentation();
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
    using (IServiceScope scope = app.Services.CreateScope())
    {
        SmartBusContext context = scope.ServiceProvider.GetRequiredService<SmartBusContext>();
        context.Database.EnsureCreated();
    }

    // Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}