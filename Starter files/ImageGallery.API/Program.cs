using ImageGallery.API.Authorization;
using ImageGallery.API.DbContexts;
using ImageGallery.API.Services;
using ImageGallery.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(configure => configure.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddDbContext<GalleryContext>(options =>
{
    options.UseSqlite(
        builder.Configuration["ConnectionStrings:ImageGalleryDBConnectionString"]);
});
// clear out defaults
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services
    .AddScoped<IGalleryRepository, GalleryRepository>()          // register the repository
    .AddHttpContextAccessor()                                    // our requirement handler needs this
    .AddScoped<IAuthorizationHandler, MustOwnImageHandler>();    // register the image owner requirement handler

// register AutoMapper-related services
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
    // register
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)

    //.AddJwtBearer(options =>              // this dos'nt handle intraspection
    //{
    //    options.Authority = "https://localhost:5001";
    //    options.Audience = "imagegalleryapi";
    //    options.TokenValidationParameters = new()
    //    {
    //        NameClaimType = "given_name"
    //        ,RoleClaimType = "role"
    //        ,ValidTypes = new[] { "at+jwt" }
    //    };

    //})
    .AddOAuth2Introspection(options =>      // this does
    {
        options.Authority = "https://localhost:5001";
        options.ClientId = "imagegalleryapi";
        options.ClientSecret = "apisecret";
        options.NameClaimType = "given_name";
        options.RoleClaimType = "role";
    })
    .Services.AddAuthorization(options =>       // first steps with an Authorization Policy
        {
            options.AddPolicy(Names.ImageAdd, AuthorizationPolicies.CanAddImage());
            options.AddPolicy("ClientApplicationCanWrite", policyBuilder =>
            {
                policyBuilder.RequireClaim("scope","imagegalleryapi.write");
            });
            options.AddPolicy(Names.ImageOwner,policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new MustOwnImageRequirement());
            });
        })
    ;


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
