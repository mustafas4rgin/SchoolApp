using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SchoolApp.Domain.Contracts;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Application.BackgroundServices;

public class TokenCleanupService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public TokenCleanupService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();
            var repo = scope.ServiceProvider.GetRequiredService<IGenericRepository>();

            var expiredTokens = await repo.GetAll<AccessToken>()
                .Where(t => t.ExpiresAt < DateTime.UtcNow)
                .ToListAsync();

            foreach (var token in expiredTokens)
                repo.Delete(token);
                
            await repo.SaveChangesAsync();

            await Task.Delay(TimeSpan.FromHours(3), stoppingToken);
        }
    }
}