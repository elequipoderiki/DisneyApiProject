using System;
using System.Collections.Generic;
using System.Text;
using DisneyApi.AppCode.Db;
using DisneyApi.AppCode.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using DisneyApi.AppCode.Playings;
using DisneyApi.AppCode.Characters;
using DisneyApi.AppCode.Movies;
using DisneyApi.AppCode.Genres;
using DisneyApi.AppCode.Services;

namespace DisneyApi
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
            services.AddControllers();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters{
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])) //check appsettings.json for jwt:key
                };
            });
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IGenreCommandService, GenreCommandService>();
            services.AddScoped<IGenreQueryService, GenreQueryService>();
            services.AddScoped<IUserCommandService, UserCommandService>();
            services.AddScoped<IPlayingService, PlayingService>();
            services.AddScoped<IMovieCommandService, MovieCommandService>();
            services.AddScoped<IMovieQueryService, MovieQueryService>();
            services.AddScoped<IEMailService, EMailService>();
            services.AddScoped<ICharacterCommandService, CharacterCommandService>();
            services.AddScoped<ICharacterQueryService, CharacterQueryService>();
            services.AddDbContext<GlobalDbContext>(opciones => opciones.UseSqlServer(Configuration.GetConnectionString("DisneyDb")));
            services.AddScoped<IDbContext, GlobalDbContext>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
