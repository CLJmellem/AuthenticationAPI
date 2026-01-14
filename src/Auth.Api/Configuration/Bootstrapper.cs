using Auth.Application.Behaviors;
using Auth.Application.Commands.Register;
using Auth.Application.Interfaces;
using Auth.Domain.Interfaces;
using Auth.Infrastructure.Configuration;
using Auth.Infrastructure.Persistence;
using Auth.Infrastructure.Repositories;
using Auth.Infrastructure.Services;
using FluentValidation;
using MediatR;

namespace Auth.Api.Configuration;

public static class Bootstrapper
{
    public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbSettings>(configuration.GetSection(MongoDbSettings.SectionName));
        services.AddSingleton(typeof(MongoDbContext<>));
        services.AddScoped<IEncryptionService, EncryptionService>();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommandHandler).Assembly);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        services.AddControllers();
        services.AddOpenApi();
    }

    public static void RegisterRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
    }

    public static void RegisterValidators(IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(RegisterUserCommandValidator).Assembly);
    }
}
