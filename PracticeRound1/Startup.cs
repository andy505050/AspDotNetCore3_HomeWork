using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PracticeRound1.Models;

namespace PracticeRound1
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
            // using Microsoft.EntityFrameworkCore;
            services.AddDbContext<Contosouniversitypracticeround1Context>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Register the Swagger services
            services.AddSwaggerDocument();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    // 當驗證失敗時，回應標頭會包含 WWW-Authenticate 標頭，這裡會顯示失敗的詳細錯誤原因
                    options.IncludeErrorDetails = true; // 預設值為 true，有時會特別關閉

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // 透過這項宣告，就可以從 "sub" 取值並設定給 User.Identity.Name
                        NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
                        // 透過這項宣告，就可以從 "roles" 取值，並可讓 [Authorize] 判斷角色
                        RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",

                        // 一般我們都會驗證 Issuer
                        ValidateIssuer = true,
                        ValidIssuer = "JwtAuthDemo", // "JwtAuthDemo" 應該從 IConfiguration 取得

                        // 若是單一伺服器通常不太需要驗證 Audience
                        ValidateAudience = false,
                        //ValidAudience = "JwtAuthDemo", // 不驗證就不需要填寫

                        // 一般我們都會驗證 Token 的有效期間
                        ValidateLifetime = true,

                        // 如果 Token 中包含 key 才需要驗證，一般都只有簽章而已
                        ValidateIssuerSigningKey = false,

                        // "1234567890123456" 應該從 IConfiguration 取得
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1234567890123456"))
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment ()) {
            //    app.UseDeveloperExceptionPage ();
            //}
            //else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //注意先認證才授權
            app.UseAuthentication();
            app.UseAuthorization();

            // Register the Swagger generator and the Swagger UI middlewares
            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}