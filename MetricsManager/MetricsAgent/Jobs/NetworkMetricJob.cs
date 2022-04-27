using MetricsAgent.DAL;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class NetworkMetricJob : IJob
    {
        private INetworkMetricsRepository _repository;
        // Счётчик для метрики Network
        private PerformanceCounter _networkCounter;

        public NetworkMetricJob(INetworkMetricsRepository repository)
        {
            _repository = repository;
            _networkCounter = new PerformanceCounter("Network", "Bytes Received/sec", "");
        }
        public Task Execute(IJobExecutionContext context)
        {
            // Получаем значение занятости Network
            var networkUsageInPercents = Convert.ToInt32(_networkCounter.NextValue());
            // Узнаем, когда мы сняли значение метрики
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            // Теперь можно записать что-то посредством репозитория
            _repository.Create(new NetworkMetric
            {
                Time = time,
                Value = networkUsageInPercents
            });

            // Теперь можно записать что-то посредством репозитория
            return Task.CompletedTask;
        }
    }
}
