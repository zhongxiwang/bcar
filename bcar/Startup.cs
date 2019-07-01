using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using bcar.model;
using bcar.service;
using bcar.Socket;
using Dapper;
using Hup.MessageBus;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore;
using Swashbuckle.AspNetCore.Swagger;

namespace bcar
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            repository = LogManager.CreateRepository("NETCoreRepository");
            XmlConfigurator.Configure(repository, new FileInfo("./data/log4net.config"));
            GlobConfiguration = configuration;
        }


        public static ILoggerRepository repository { get; set; }
        public IConfiguration Configuration { get; }
        public static IConfiguration GlobConfiguration { get;private set; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("any",
                 builder =>
                 {
                     builder.AllowAnyOrigin().AllowCredentials();
                 });
            });
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "百变出车",
                    Description = "百变出车后台api",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "",
                        Email = string.Empty,
                        Url = ""
                    },
                    License = new License
                    {
                        Name = "许可证名字",
                        Url = ""
                    }
                });
                // 为 Swagger JSON and UI设置xml文档注释路径
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                var xmlPath = Path.Combine(basePath, "bcar.xml");
                options.IncludeXmlComments(xmlPath);
            });
            services.AddSingleton<IConfiguration>(this.Configuration);
            services.AddSingleton(op =>
            {
                return LogManager.GetLogger(repository.Name, typeof(Startup));
            });
            services.AddTransient<IDbConnection, MySqlConnection>(Key =>
            {
                return new MySqlConnection(this.Configuration["mysql"]);
            });
            services.AddSingleton<RedisService>();
            ;
            //webToken wt = new webToken(this.Configuration["appsecret"], this.Configuration["AppID"],"");
            services.AddSingleton<TokenService>(key => new TokenService(this.Configuration["appsecret"], this.Configuration["AppID"]));
            //services.AddSingleton<webToken>(wt);
            services.AddSingleton<userCache>();
            services.AddSingleton<CostService>();
            services.AddSingleton<driverCache>();
            services.AddDistributedMemoryCache();
            services.AddSingleton<jsapiTokencs>();
            services.AddSession();
            outTimeOrders();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            var pathBase = Configuration["PATH_BASE"];
            app.UseSwagger()
              .UseSwaggerUI(c =>
              {
                  c.SwaggerEndpoint($"{ (!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty) }/swagger/v1/swagger.json", "bcar");

              });
            register();
            app.UseCors("any");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseSession();
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseMvc();
        }

        /// <summary>
        /// 注册
        /// </summary>
        public void register()
        {
            new driverService().Register();
            new MessgeService().Register();
            new orderService().Register();
            
            var ws = UseSocket.CreateSocket();
            ws.Message = (username, message) =>
            {
                var request= Newtonsoft.Json.JsonConvert.DeserializeObject<Request>(message);
                request.Head.Add("wxcount", username);
                if(request.ClientType.Other.Equals("uploadLocation"))
                    request.AddService = () =>
                {
                    IDbConnection con= new MySqlConnection(this.Configuration["mysql"]);
                    var driverState = con.ExecuteScalar<long>("select count(1) from orders where driverid="+request.Head["KeyID"] +" and state=2");
                    return driverState.ToString();
                };
                var result= Hup.CreateMsg.Run(request);
            };
        }

        void outTimeOrders()
        {
            var t = Startup.GlobConfiguration["mysql"];
            IDbConnection db = new MySql.Data.MySqlClient.MySqlConnection(this.Configuration["mysql"]);
             db.Open();
            JObject obj = new JObject();
            obj.Add("state", 3);
            var lists = db.Query<orders>("select * from orders where state='0' ");
            TaskManagerService tak = TaskManagerService.Factory();
            TokenService token = new TokenService(this.Configuration["appsecret"], this.Configuration["AppID"]);
            foreach (var key in lists)
            {
                Task.Run(async () =>
                {
                    await tak.AutoRun(key.StartTime, keys =>
                    {
                        IDbConnection _db = new MySql.Data.MySqlClient.MySqlConnection(this.Configuration["mysql"]);
                        JObject js = JObject.Parse(key.ordersInfo);
                        string sql = uilt.uiltT.Update(obj, "orders", " where id='" + key.id + "' ");
                        _db.Execute(sql);
                        uilt.uiltT.SendWxMessage(token, "您的订单从" + js["startingPoint"] + "到" + js["endingPoint"] + "的   行程，由于长时间没有司机接单已经超时，请重新创建行程。", js["openid"].ToString());
                        _db.Close();
                        return Task.CompletedTask;
                    });
                });
            }
            db.Close();
        }
    }
}
