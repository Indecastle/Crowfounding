
using Crowfounding.BusinessLogic;
using Crowfounding.Data;
using Crowfounding.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Crowfounding.Models.Finance;
using Microsoft.EntityFrameworkCore;

namespace Crowfounding.BackroundJob
{
    internal class TimedHostedService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private Timer _timer;
        private Timer _timer_UserBonuses;
        private readonly IServiceScopeFactory _scopeFactory;
        

        public TimedHostedService(ILogger<TimedHostedService> logger, IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
            _timer_UserBonuses = new Timer(DoWork_UserBonuses, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                DateTime date = DateTime.Now;
                foreach(Company company in _context.Companies.Where(c => !c.IsEnd))
                {
                    // Company comp = _context.Companies.Find(company.Id);
                    if (CompanyBusinessLogic.EndCompanyIfTime(company))
                        _logger.LogInformation($"Company '{company.Name}' is ended");
                }
                _context.SaveChanges();
            }
        }
        
        private void DoWork_UserBonuses(object state)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                DateTime dateNow = DateTime.Now;
                foreach(TotalDonate totalDonate in _context.TotalDonates
                    .Include(td => td.User)
                    .Include(td => td.Company.UserBonuses)
                    .Include(td => td.Company.Bonuses))
                {
                    foreach (var bonuse in totalDonate.Company.Bonuses)
                    {
                        if (!totalDonate.Company.UserBonuses.Any(ub => 
                                ub.BonuseId == bonuse.Id && ub.UserId == totalDonate.UserId)
                            && totalDonate.Amount > bonuse.NeedAmount)
                        {
                            _context.UserBonuses.Add(new UserBonuse
                            {
                                UserId = totalDonate.UserId,
                                CompanyId = totalDonate.CompanyId,
                                BonuseId = bonuse.Id,
                                TotalDonateId = totalDonate.Id,
                                CreateAt = dateNow
                            });
                            _logger.LogInformation($"User Get Bonuse '{bonuse.Title}' with {bonuse.NeedAmount}$ is ended");
                        }
                    }
                }
                _context.SaveChanges();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);
            _timer_UserBonuses?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
            _timer_UserBonuses?.Dispose();
        }
    }
}
