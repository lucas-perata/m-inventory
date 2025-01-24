using MainService.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MainService.Services.UserItem;
using MassTransit;

var bld = WebApplication.CreateBuilder();

bld.Services.AddDbContext<DataContext>(opt => 
{
    opt.UseNpgsql(bld.Configuration.GetConnectionString("DefaultConnection"));
});

bld.Services.AddScoped<IUserItemService, UserItemService>();

bld.Services.AddFastEndpoints();

bld.Services.AddMassTransit(x => 
{
    x.AddEntityFrameworkOutbox<DataContext>(o => 
    {
        o.QueryDelay = TimeSpan.FromSeconds(10);
        o.UsePostgres();
        o.UseBusOutbox();
    });

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});

bld.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => 
    {
        options.Authority = bld.Configuration["IdentityServiceUrl"];
        options.RequireHttpsMetadata = false; 
        options.TokenValidationParameters.ValidateAudience = false;
        options.TokenValidationParameters.NameClaimType = "username";
    });

bld.Services.AddAuthorization();


var app = bld.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseFastEndpoints();


app.Run();
