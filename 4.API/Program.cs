using _2.Core.Extensions;
using _3.Infra;
using _3.Infra.Extensions;
using _3.Infra.Database;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(opt =>{
  opt.UseNpgsql(builder.Configuration.GetConnectionString(
    "DefaultConnection"));
});

#region Configure DI Container - Service Lifetimes - Infra

builder.Services.AddInfraDependencyInjection(builder.Configuration);

#endregion

#region Configure DI Container - Service Lifetimes - Business Services

builder.Services.AddCoreDependencyInjection();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await SeedDatabase();

app.Run();

async Task SeedDatabase()
{
  using (var scope = app.Services.CreateScope())
  {
    var dbcontext = scope.ServiceProvider.GetRequiredService<DataContext>();
    Console.WriteLine(dbcontext.Database);
    // Run migration scripts
    await dbcontext.Database.MigrateAsync();

    // Seed data to the project
    await Seed.SeedData(dbcontext);
  }
}