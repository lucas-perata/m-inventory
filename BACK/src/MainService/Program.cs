using MainService.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

var bld = WebApplication.CreateBuilder();

// Services
bld.Services.AddDbContext<DataContext>(opt => 
{
    opt.UseNpgsql(bld.Configuration.GetConnectionString("DefaultConnection"));
});

bld.Services.AddFastEndpoints();

var app = bld.Build();
app.UseFastEndpoints();

app.Run();
