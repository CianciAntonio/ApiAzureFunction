using Microsoft.Data.SqlClient;
using Polly.Retry;
using Polly;
using System;

namespace ApiFunctionWithRepositoryPattern
{
    public class ExceptionPolicy
    {
        public static int maxRetryAttempts = 3;
        public static TimeSpan pauseBetweenFailures = TimeSpan.FromSeconds(2);

        public static readonly AsyncRetryPolicy retryPolicy =
               Policy.Handle<SqlException>()
                     .WaitAndRetryAsync(maxRetryAttempts,
                            i => pauseBetweenFailures,
                            onRetry: (exception, retryCount) =>
                            {
                                Console.WriteLine($"Retry {retryCount} due to {exception}");
                            });
    }
}