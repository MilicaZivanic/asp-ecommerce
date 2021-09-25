using API.Core;
using Application;
using Application.Commands;
using Application.Commands.CategoryCommands;
using Application.Commands.DiscountCommands;
using Application.Commands.OrderCommands;
using Application.Commands.ProductCommands;
using Application.Commands.UserAddressCommands;
using Application.Commands.UserCommands;
using Application.Commands.UserPaymentCommands;
using Application.Email;
using Application.Queries;
using AutoMapper;
using EfDataAccess;
using Implementation.Commands;
using Implementation.Commands.CategoryCommands;
using Implementation.Commands.DiscountCommands;
using Implementation.Commands.OrderCommands;
using Implementation.Commands.ProductCommands;
using Implementation.Commands.UserAddressCommands;
using Implementation.Commands.UserCommands;
using Implementation.Commands.UserPaymentCommands;
using Implementation.Email;
using Implementation.Logging;
using Implementation.Queries;
using Implementation.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Nedelja7.Api.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API
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
            var appSettings = new AppSettings();
            Configuration.Bind(appSettings);

            services.AddTransient<EcomShopContext>();

            services.AddTransient<CreateOrderValidator>();
            services.AddTransient<IGetOrdersQuery, EfGetOrdersQuery>();
            services.AddTransient<IGetOrderQuery, EfGetOrderQuery>();
            services.AddTransient<ICreateOrderCommand, EfCreateOrderCommand>();
            services.AddTransient<IChangeOrderStatusCommand, EfChangeOrderStatusCommand>();

            services.AddTransient<UpdateUserValidator>();
            services.AddTransient<IGetUsersQuery, EfGetUsersQuery>();
            services.AddTransient<IGetUserQuery, EfGetUserQuery>();
            services.AddTransient<IDeleteUserCommand, EfDeleteUserCommand>();
            services.AddTransient<IUpdateUserCommand, EfUpdateUserCommand>();

            services.AddTransient<UserAddressValidator>();
            services.AddTransient<ICreateUserAddressCommand, EfCreateUserAddressCommand>();
            services.AddTransient<IUpdateUserAddressCommand, EfUpdateUserAddressCommand>();
            services.AddTransient<IDeleteUserAddressCommand, EfDeleteUserAddressCommand>();

            services.AddTransient<UserPaymentValidator>();
            services.AddTransient<ICreateUserPaymentCommand, EfCreateUserPaymentCommand>();
            services.AddTransient<IUpdateUserPaymentCommand, EfUpdateUserPaymentCommand>();
            services.AddTransient<IDeleteUserPaymentCommand, EfDeleteUserPaymentCommand>();

            services.AddTransient<DiscountValidator>();
            services.AddTransient<IGetDiscountQuery, EfGetDiscountQuery>();
            services.AddTransient<IGetDiscountsQuery, EfGetDiscountsQuery>();
            services.AddTransient<ICreateDiscountCommand, EfCreateDiscountCommand>();
            services.AddTransient<IUpdateDiscountCommand, EfUpdateDiscountCommand>();
            services.AddTransient<IDeleteDiscountCommand, EfDeleteDiscountCommand>();

            services.AddTransient<CreateProductValidator>();
            services.AddTransient<UpdateProductValidator>();
            services.AddTransient<IGetProductsQuery, EfGetProductsQuery>();
            services.AddTransient<IGetProductQuery, EfGetProductQuery>();
            services.AddTransient<ICreateProductCommand, EfCreateProductCommand>();
            services.AddTransient<IUpdateProductCommand, EfUpdateProductCommand>();
            services.AddTransient<IDeleteProductCommand, EfDeleteProductCommand>();

            services.AddTransient<CreateCategoryValidator>();
            services.AddTransient<UpdateCategoryValidator>();
            services.AddTransient<IGetCategoriesQuery, EfGetCategoriesQuery>();
            services.AddTransient<ICreateCategoryCommand, EfCreateCategoryCommand>();
            services.AddTransient<IDeleteCategoryCommand, EfDeleteCategoryCommand>();
            services.AddTransient<IUpdateCategoryCommand, EfUpdateCategoryCommand>();

            services.AddTransient<ISetUserUseCase, EfSetUserUseCases>();
            services.AddTransient<IGetUserUseCases, EfGetUserUseCases>();

            services.AddHttpContextAccessor();
            services.AddApplicationActor();
            services.AddJwt(appSettings);
            services.AddUsesCases();

            services.AddTransient<IUseCaseLogger, DatabaseUseCaseLogger>();
            services.AddTransient<IEmailSender, SmtpEmailSender>(x => new SmtpEmailSender(appSettings.EmailFrom, appSettings.EmailPassword));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseRouting();

            app.UseMiddleware<GlobalExceptionHandler>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
