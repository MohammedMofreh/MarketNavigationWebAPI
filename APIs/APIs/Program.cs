
using APIs.Models;
using APIs.ServiceImplement;
using APIs.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;


namespace APIs
{
    public class Program
    {
        public static void Main(string[] args)
        {

               // Hi Mofreh

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers(
                Options =>
                {
                    var policy = new AuthorizationPolicyBuilder().
                    RequireAuthenticatedUser().Build();

                    Options.Filters.Add(new AuthorizeFilter(policy));
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                    options.SerializerSettings.MaxDepth = 64; // Adjust depth as needed
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                    options.JsonSerializerOptions.MaxDepth = 64; // Adjust depth as needed
                });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            //Swagger_Configuration
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "E-Commerce APP",
                    Description = "E-Commerce APP,Contains Many features can applied it in many projects",
                    TermsOfService = new Uri("https://mohamedmofreh236@gmail.com"),
                    Contact = new OpenApiContact
                    {
                        Name = "MOFREH",
                        Email = "mohamedmofreh236@gmail.com",
                        Url = new Uri("https://mohamedmofreh236@gmail.com")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "My license",
                        Url = new Uri("https://mohamedmofreh236@gmail.com")
                    }
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\""
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
            });

            //JWT Configuration
            builder.Services.AddTransient<IJwtService, JwtService>();

            //Configuration of connectionString
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<GraduationDataBaseContext>(options =>
                options.UseSqlServer(connectionString));

            //Configuration of Identity
            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>
                (options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
            })
            .AddEntityFrameworkStores<GraduationDataBaseContext>()
            .AddDefaultTokenProviders()
            .AddUserStore<UserStore<ApplicationUser, ApplicationRole, GraduationDataBaseContext, Guid>>()
            .AddRoleStore<RoleStore<ApplicationRole, GraduationDataBaseContext, Guid>>();


            // JWT
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new
                  TokenValidationParameters()
                  {
                      ValidateAudience = false,
                      ValidAudience = builder.Configuration[" Jwt:Audience"],
                      ValidateIssuer = true,
                      ValidIssuer = builder.Configuration["Jwt:Issuer"],
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      IssuerSigningKey = new SymmetricSecurityKey
                      (System.Text.Encoding.UTF8.GetBytes(builder.Configuration
                      ["Jwt:Key"]))

                  };
              });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireSellerRole", policy => policy.RequireRole("Seller"));
                options.AddPolicy("RequireBuyerRole", policy => policy.RequireRole("Buyer"));
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                });
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseStaticFiles();
            app.UseHsts();
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.MapBooksProductEndpoints();

            app.Run();
        }
    }
}