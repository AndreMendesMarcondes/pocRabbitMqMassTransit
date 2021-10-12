using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "MT.API", Version = "v1" });
});

builder.Services.AddMassTransit(bus =>
{
    bus.UsingRabbitMq();

    var busControl = Bus.Factory.CreateUsingRabbitMq(c =>
    {
        c.Host(builder.Configuration.GetConnectionString("RabbitMq"));
    });

    busControl.Start();
});
builder.Services.AddMassTransitHostedService();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MT.API v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();