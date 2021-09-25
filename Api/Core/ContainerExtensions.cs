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
using Application.Queries;
using EfDataAccess;
using Implementation.Commands;
using Implementation.Commands.CategoryCommands;
using Implementation.Commands.DiscountCommands;
using Implementation.Commands.OrderCommands;
using Implementation.Commands.ProductCommands;
using Implementation.Commands.UserAddressCommands;
using Implementation.Commands.UserCommands;
using Implementation.Commands.UserPaymentCommands;
using Implementation.Queries;
using Implementation.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Text;

namespace Nedelja7.Api.Core
{
    public static class ContainerExtensions
    {
        public static void AddUsesCases(this IServiceCollection services)
        {
            services.AddTransient<UseCaseExecutor>();

            services.AddTransient<CreateOrderValidator>();
            services.AddTransient<IGetOrdersQuery, EfGetOrdersQuery>();
            services.AddTransient<IGetOrderQuery, EfGetOrderQuery>();
            services.AddTransient<ICreateOrderCommand, EfCreateOrderCommand>();
            services.AddTransient<IChangeOrderStatusCommand, EfChangeOrderStatusCommand>();

            services.AddTransient<RegisterUserValidator>();
            services.AddTransient<UpdateUserValidator>();
            services.AddTransient<IGetUsersQuery, EfGetUsersQuery>();
            services.AddTransient<IGetUserQuery, EfGetUserQuery>();
            services.AddTransient<IRegisterUserCommand, EfRegisterUserCommand>();
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
        }

        public static void AddApplicationActor(this IServiceCollection services)
        {
            services.AddTransient<IApplicationActor>(x =>
            {
                var accessor = x.GetService<IHttpContextAccessor>();
                //izvuci token
                //pozicionirati se na payload
                //izvuci ActorData claim
                //Deserijalizovati actorData string u c# objekat

                var user = accessor.HttpContext.User;

                if (user.FindFirst("ActorData") == null)
                {
                    return new AnonymousActor();
                }

                var actorString = user.FindFirst("ActorData").Value;

                var actor = JsonConvert.DeserializeObject<JwtActor>(actorString);

                return actor;
            });
        }

        public static void AddJwt(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddTransient<JwtManager>(x =>
            {
                var context = x.GetService<EcomShopContext>();

                return new JwtManager(context, appSettings.JwtIssuer, appSettings.JwtSecretKey);
            });

            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = appSettings.JwtIssuer,
                    ValidateIssuer = true,
                    ValidAudience = "Any",
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.JwtSecretKey)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
    }
}