using InvoiceApi.Data;
using InvoiceApi.Security;
using InvoiceApi.Services;
using InvoiceApi.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

namespace InvoiceApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();

            services.Configure<JwtSettings>(Configuration.GetSection(JwtSettings.SectionName));

            var bytesOutput = Encoding.UTF8.GetBytes(
                Configuration["JwtSettings:SecretKey"]);

            var secret = new SymmetricSecurityKey(bytesOutput);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwtOptions =>
                {

                    jwtOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidAudience = Configuration["JwtSettings:Audience"],
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["JwtSettings:Issuer"],
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        IssuerSigningKey = secret
                    };
                });

            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter JWT Bearer token"
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
                            }
                        },
                        new string[] { }
                    }
                });

            });
            services.AddHttpContextAccessor();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
