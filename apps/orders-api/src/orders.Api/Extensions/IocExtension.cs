using System.Reflection;
using MediatR;
using orders.Api.Domain.Entites;
using orders.Api.Domain.Interfaces;
using orders.Api.Domain.Services;
using orders.Api.Repositories;
using orders.Api.Settings;
using Orders.Api.Domain.Interfaces;

namespace Orders.Api.Extensions;

public static class IocExtension
{
    public static void AddIoc(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddScoped<IEventHandlerService, EventHandlerService>();

        services.Configure<MongodbSettings>(configuration.GetSection("MongoDbSettings"));
        services.AddSingleton<IMongoDbServiceHandler<Order, string>, MongoDbServiceHandler<Order, string>>();
        services.AddScoped<IOrderRepository, OrderRepository>();


    }
}