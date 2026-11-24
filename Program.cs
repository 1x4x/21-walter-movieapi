using MovieApi.Data;
using MovieApi.Extensions;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(); builder.Services.AddOpenApi();
builder.Services.AddDbContext<MovieContext>(o => o.UseSqlite(builder.Configuration.GetConnectionString("Movies") ?? "Data Source=movies.db").ConfigureWarnings(w => w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning)));
var app = builder.Build();
app.MapOpenApi(); app.UseDefaultFiles(); app.UseStaticFiles(); app.MapControllers();
app.SeedData(); app.Run();
