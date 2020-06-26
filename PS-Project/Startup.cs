using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using Persistence.Repositories.Implementation;
using Persistence.Repositories.Interfaces;
using PS_Project_Model.Resources;
using PS_Project_Model.Services.Implementation;
using PS_Project_Model.Services.Interfaces;
using PS_Project_Model.Utils;
using PS_Project_Model.Utils.Implementation;
using PS_Project_Model.Utils.Interfaces;

namespace PS_Project
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
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            
            services.AddMemoryCache();

            services.AddSwaggerGen();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionString:CookingDB"]);
            });
            
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddScoped<IRecipesRepository, RecipesRepository>();

            services.AddScoped<IRecipesService, RecipesService>();
            
            services.AddScoped<IRatingRepository, RatingRepository>();
            
            services.AddScoped<IRatingsService, RatingsService>();
            
            services.AddScoped<IIngredientsRepository, IngredientsRepository>();
            
            services.AddScoped<IIngredientsService, IngredientsService>();
            
            services.AddScoped<INutritionFactsRepository, NutritionFactsRepository>();
            
            services.AddScoped<INutritionFactsService, NutritionFactsService>();
            
            services.AddScoped<IRatingsUtils, RatingsUtils>();
            
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();

            services.AddScoped<IAuthService, AuthService>();
            
            services.AddScoped<IAttachmentsRepository, AttachmentsRepository>();

            services.AddScoped<IAttachmentsService, AttachmentsService>();
            
            services.AddScoped<IAttachmentUtils, AttachmentUtils>();
            
            services.AddScoped<ICommentsRepository, CommentsRepository>();

            services.AddScoped<ICommentsService, CommentsService>();
            
            services.AddScoped<ICommentsUtils, CommentsUtils>();
            
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<ICategoriesService, CategoriesService>();
            
            services.AddScoped<ICategoriesUtils, CategoriesUtils>();
            
            services.AddControllers();
            
            services.AddScoped<IRecipeUtils, RecipeUtils>();
            
            services.AddScoped<IRequestUtils, RequestUtils>();
            
            services.AddScoped<IPdfGeneratorUtils, PdfGeneratorUtils>();
            
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            services.AddAutoMapper();
            
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    });
            });
            
            services.AddMvc(options => options.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Latest);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rest API");
            });

            app.UseCors("AllowSpecificOrigin");
            
            app.UseStaticFiles();

            app.UseAuthentication();
            
            app.UseMvc();
            
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
                context.Database.EnsureCreated();
            }
        }
    }
}