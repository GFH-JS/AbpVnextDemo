using Acme.BookStore.Repositories;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.BackgroundWorkers.Quartz;

namespace Acme.BookStore.Jobs
{
    public class TestJob : QuartzBackgroundWorkerBase
    {
        private readonly IRedisRepository _redisRepository;
        private readonly ILogger<TestJob> _logger;
        public TestJob(IRedisRepository redisRepository, ILogger<TestJob> logger)
        {
            _redisRepository = redisRepository;

            JobDetail = JobBuilder.Create<TestJob>().WithIdentity(nameof(TestJob)).Build();
            Trigger = TriggerBuilder.Create().WithIdentity(nameof(TestJob)).WithCronSchedule("1/3 * * * * ?").Build();
            _logger = logger;   
        }
        public override Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("定时任务执行中...");
            _redisRepository.SetStringAsync("quartz", DateTime.Now.ToString());
            return Task.CompletedTask;
        }
    }
}
