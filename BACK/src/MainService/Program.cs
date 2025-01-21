using MainService.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MainService.Services.UserItem;

var bld = WebApplication.CreateBuilder();

// Services
bld.Services.AddDbContext<DataContext>(opt => 
{
    opt.UseNpgsql(bld.Configuration.GetConnectionString("DefaultConnection"));
});

bld.Services.AddScoped<IUserItemService, UserItemService>();

bld.Services.AddFastEndpoints();

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
