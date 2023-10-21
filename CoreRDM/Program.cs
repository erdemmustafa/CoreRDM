using CoreRDM.Entities;
using CoreRDM.Helpers;
using CoreRDM.Mapping;
using CoreRDM.Services;
using FluentNHibernate.Cfg;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NHibernate;
using NHibernate.Id.Insert;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

{
    var services = builder.Services;
    services.AddCors();
    services.AddControllers();

    // configure strongly typed settings object
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

    // configure DI for application services
    services.AddScoped<IUserService, UserService>();
}

builder.Services.AddSingleton<NHibernate.ISessionFactory>(factory =>
{
    return Fluently.Configure()
    .Database(
        () =>
        {
            return FluentNHibernate.Cfg.Db.MsSqlConfiguration.MsSql2012
            .ShowSql()
            .ConnectionString(builder.Configuration.GetConnectionString("SqlConnection"));
        }
    )
    .Mappings(z => z.FluentMappings.AddFromAssembly(System.Reflection.Assembly.GetExecutingAssembly()))
    .BuildSessionFactory();
}
);
builder.Services.AddSingleton<NHibernate.ISession>(factory => factory.GetServices<ISessionFactory>().First().OpenSession());


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "RDM API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[] { }
    }
});
});


var app = builder.Build();


{
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // custom jwt auth middleware
    app.UseMiddleware<JwtMiddleware>();
    app.UseMiddleware<ErrorHandlerMiddleware>();

    app.MapControllers();
}





// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); // 👈👈it is new line

app.UseAuthorization();

app.MapControllers();

app.Run();
