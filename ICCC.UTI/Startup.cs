using ICCC.UTI.CORE.CoreEntities;
using ICCC.UTI.CORE.Coreinterface;
using ICCC.UTI.CORE.CoreInterfaces;
using ICCC.UTI.CORE.CoreServices;
using ICCC.UTI.DATA.DataInterfaces;
using ICCC.UTI.DATA.DataServices;
using ICCC.UTI.DATA.DataUtilities;
using ICCC.UTI.DATA.Repositories;
using ICCC.UTI.Extensions;
using ICCC.UTI.LOGGER;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NLog;
using System.IO;
using System.Text;

namespace ICCC.UTI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers().AddXmlDataContractSerializerFormatters();
            services.AddSwaggerDocumentation();
            services.AddScoped<IAppSettings, AppSetings>();
            services.AddScoped<IAuthenticationCore, AuthenticationCoreServices>();
            services.AddScoped<IAuthentication, AuthenticationServices>();
            services.AddScoped<ILoggerManager, LoggerManager>();
            services.AddScoped<IUser, UserDbClient>();
            services.AddScoped<IUserCore, UserCoreServices>();
            services.Configure<MySettingsEntity>(Configuration.GetSection("ConnectionStrings"));
            services.AddScoped<IRoleCore, RoleCoreServices>();
            services.AddScoped<IRole,RoleDbClient>();
            services.AddScoped<IMenuCore, MenuCoreServices>();
            services.AddScoped<IMenu, MenuDbClient>();
            services.AddScoped<IRoleAndMenuCore, RoleAndMenuCoreServices>();
            services.AddScoped<IRoleAndMenu, RoleAndMenuDbClient>();

            var appSettingSection = Configuration.GetSection("JWTDetails");
            services.Configure<AppSetings>(appSettingSection);

            //JWT Authentication
            var appsettings = appSettingSection.Get<AppSetings>();
            var Key = Encoding.ASCII.GetBytes(appsettings.Key);

            services.AddAuthentication(au =>
            {
                au.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                au.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(jwt =>
            {
                jwt.RequireHttpsMetadata = false;
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Key),
                    ValidateIssuer = false,
                    ValidateAudience = false,

                };

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            app.UseRouting();
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseCors();
            app.UseSwaggerGen();


        }
    }
}
