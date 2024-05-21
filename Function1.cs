using System;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace daviherntrackavailability
{
    public class Function1
    {
        [FunctionName("Function1")]
        public void Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var config = new TelemetryConfiguration("f7c088e1-6774-421d-bdea-4d79566ca134");
            var telemetryClient = new TelemetryClient(config);
            var availabilityTelemetry = new AvailabilityTelemetry
            {
                Name = "My custom function availability test",
                RunLocation = "UK South",
                Success = true,
                Message = "This is a test message from function",
                Duration = TimeSpan.FromMilliseconds(2000)
            };

            telemetryClient.TrackAvailability(availabilityTelemetry);
            telemetryClient.Flush();

        }
    }
}
