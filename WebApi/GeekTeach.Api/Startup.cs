using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Geek.Framework;
using Geek.Framework.Jwt;
using Geek.Framework.Middlewares;
using GeekTeach.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Serialization;

namespace GeekTeach.Api
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
            //services.AddGeek(opt => opt.UseIdGen(1))
            //    .AddDb<MySqlConnection>(Configuration.GetConnectionString("DefaultConnection"));

            services.AddGeek()
                .AddDb<MySqlConnection>(Configuration.GetConnectionString("DefaultConnection"));

            //添加Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GeekTeach Api Doc", Version = "v1.0" });
                // 获取xml文件名
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                // 获取xml文件路径
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                // 添加控制器层注释，true表示显示控制器注释
                c.IncludeXmlComments(xmlPath, true);

                #region 启用swagger验证功能
                // Add security definitions
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "Please enter into field the word 'Bearer' followed by a space and the JWT value",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference()
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    }, Array.Empty<string>() }
                });
                #endregion
            });


            services.AddCors(options =>
            {
                options.AddPolicy("any", builder =>
                {
                    builder.AllowAnyOrigin()//允许所有地址访问
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                    //.WithOrigins("")//指定接受访问的地址
                    //.AllowCredentials()//指定处理cookie 使用AllowAnyOrigin时不可以使用这个
                });
            });

            services.AddControllers().AddNewtonsoftJson(options =>
                                {
                                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                                });

            services.AddOptions();

            #region JWT

            //services.Configure<JwtOptions>(Configuration.GetSection(nameof(JwtOptions)));
            services.AddJwtBearer(Configuration);

            #endregion

            //调用前面的静态方法，将映射关系注册
            ColumnMapper.SetMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseErrorMiddleware();

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "GeekTeach Api v1");
            });


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("any");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        /// <summary>
        /// DI
        /// </summary>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).AsImplementedInterfaces().InstancePerLifetimeScope();
            var repAssemblys = Assembly.Load("GeekTeach.Data");
            var appAssemblys = Assembly.Load("GeekTeach.Application");
            builder.RegisterAssemblyTypes(repAssemblys).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(appAssemblys).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
