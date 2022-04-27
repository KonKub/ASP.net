using MetricsAgent.DAL;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class DotNetMetricJob : IJob
    {
        private IDotNetMetricsRepository _repository;
        // Счётчик для метрики DotNet
        private PerformanceCounter _dotnetCounter;

        public DotNetMetricJob(IDotNetMetricsRepository repository)
        {
            _repository = repository;
            _dotnetCounter = new PerformanceCounter(".NET CLR Memory", "Gen 2 Heap Size");
        }
        public Task Execute(IJobExecutionContext context)
        {
            // Получаем значение занятости DotNet
            var dotnetUsageInPercents = Convert.ToInt32(_dotnetCounter.NextValue());
            // Узнаем, когда мы сняли значение метрики
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            // Теперь можно записать что-то посредством репозитория
            _repository.Create(new DotNetMetric
            {
                Time = time,
                Value = dotnetUsageInPercents
            });

            // Теперь можно записать что-то посредством репозитория
            return Task.CompletedTask;
        }
    }
}
