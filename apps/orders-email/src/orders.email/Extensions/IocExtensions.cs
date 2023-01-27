using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using orders.email.Domain.Entities;
using orders.email.Domain.Interfaces;
using orders.email.Domain.Services;
using orders.email.Domain.Services.EventHandler;
using orders.email.Repositories;
using orders.email.Settings;

namespace orders.email.Extensions
{
    public static class IocExtensions
    {
        public static void AddIoc(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSingleton<IEventhandlerService, EventHandlerService>();

            services.Configure<MongodbSettings>(configuration.GetSection("MongoDbSettings"));
            services.AddSingleton<IMongodbServiceHandler<Order, string>, MongoDbServiceHandler<Order, string>>();
            services.AddSingleton<IOrderRepository, OrderRepository>();
        }
    }
}