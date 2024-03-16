using BuberBreakfast.Services;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddControllers();
    builder.Services.AddTransient<IBreakfastService, BreakfastService>();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
app.UseSwagger();
app.UseSwaggerUI();
}

{
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}
