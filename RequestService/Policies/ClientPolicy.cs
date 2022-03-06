using Polly;
using Polly.Retry;

namespace RequestService.Policies
{
    public class ClientPolicy
    {
        public AsyncRetryPolicy<HttpResponseMessage> ImmediateHttpRetry { get; }
        public AsyncRetryPolicy<HttpResponseMessage> LinearHttpRetry { get; }
        public AsyncRetryPolicy<HttpResponseMessage> ExponentialHttpRetry { get; }

        public ClientPolicy()
        {
            ImmediateHttpRetry = Policy.HandleResult<HttpResponseMessage>(
                response => !response.IsSuccessStatusCode
                ).RetryAsync(5);

            LinearHttpRetry = Policy.HandleResult<HttpResponseMessage>(
                response => !response.IsSuccessStatusCode
                ).WaitAndRetryAsync(5, retryCount => TimeSpan.FromSeconds(3));

            ExponentialHttpRetry = Policy.HandleResult<HttpResponseMessage>(
                response => !response.IsSuccessStatusCode
                ).WaitAndRetryAsync(5, retryCount => TimeSpan.FromSeconds(Math.Pow(2, retryCount)));
        }
    }
}
