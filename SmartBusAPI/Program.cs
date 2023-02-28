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
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }

    // Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
    app.MapControllers();
    app.UseRouting();
    app.UseCors();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapHub<NotificationHub>("/notification_hub");
    app.Run();
}