using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wolverine;
using Wolverine.RabbitMQ;

namespace SASS.Chassis.Messaging;

public static class Extensions
{
    public static IHostApplicationBuilder AddMessagingQueue(
        this IHostApplicationBuilder builder,
        Action<WolverineOptions>? configure = null)
    {
        var section = builder.Configuration.GetSection(MessagingQueueOptions.SectionName);

        builder.Services.AddOptions<MessagingQueueOptions>()
            .Bind(section)
            .Validate(static options => !string.IsNullOrWhiteSpace(options.Host), "Messaging Host is required")
            .Validate(static options => options.Port > 0, "Messaging Port must be greater than zero")
            .Validate(static options => !string.IsNullOrWhiteSpace(options.UserName), "Messaging UserName is required")
            .Validate(static options => !string.IsNullOrWhiteSpace(options.Password), "Messaging Password is required")
            .ValidateOnStart();

        var settings = section.Get<MessagingQueueOptions>() ?? new MessagingQueueOptions();

        builder.UseWolverine(options =>
        {
            var rabbitMq = options.UseRabbitMq(factory =>
                {
                    factory.HostName = settings.Host;
                    factory.Port = settings.Port;
                    factory.UserName = settings.UserName;
                    factory.Password = settings.Password;
                    factory.VirtualHost = settings.VirtualHost;
                    factory.Ssl.Enabled = settings.UseSsl;
                });

            if (settings.UseConventionalRouting)
            {
                rabbitMq.UseConventionalRouting();
            }

            if (settings.DisableSystemRequestReplyQueueDeclaration)
            {
                rabbitMq.DisableSystemRequestReplyQueueDeclaration();
            }

            if (settings.DisableConventionalLocalRouting)
            {
                options.Policies.DisableConventionalLocalRouting();
            }

            if (settings.AutoProvision)
            {
                rabbitMq.AutoProvision();
            }

            configure?.Invoke(options);
        });

        return builder;
    }
}
