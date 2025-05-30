﻿using Polly;
using Polly.Retry;
using Serilog;

namespace NetCoreSamples.Messaging.Lib.Resiliency
{
    /// <summary>
    /// The application retry policies for handling exceptions.
    /// </summary>
    public static class RetryPolicies
    {
        /// <summary>
        /// Builds the default retry policy for handling exceptions.
        /// </summary>
        /// <param name="retryCount">The amount of retries to be executed. If null is provided, WaitAndRetryForeverAsync will be used.</param>
        /// <param name="sleepDurationInSeconds">The sleep between retries.</param>
        /// <returns>The default <see cref="AsyncRetryPolicy"/></returns>
        public static AsyncRetryPolicy DefaultAsyncExceptionRetryPolicy<T>(int sleepDurationInSeconds, int? retryCount = null)
            where T : Exception
        {
            if (!retryCount.HasValue)
            {
                return Policy
                    .Handle<T>()
                    .WaitAndRetryForeverAsync(
                        retryAttempt => TimeSpan.FromSeconds(sleepDurationInSeconds),
                        (exception, timeSpan) =>
                        {
                            Log.Logger.Error($"Operation attempt failed. Retrying in {timeSpan.TotalSeconds} seconds", exception);
                        });
            }

            return Policy
                .Handle<T>()
                .WaitAndRetryAsync(
                    retryCount.Value,
                    retryAttempt => TimeSpan.FromSeconds(sleepDurationInSeconds),
                    (exception, timeSpan, retryCount, context) =>
                    {
                        Log.Logger.Error($"Operation attempt {retryCount} failed. Retrying in {timeSpan.TotalSeconds} seconds", exception);
                    });
        }
    }
}
