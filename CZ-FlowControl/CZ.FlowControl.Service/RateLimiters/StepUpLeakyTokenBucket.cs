using System;

namespace CZ.FlowControl.Service
{
    /// <summary>
    /// 漏桶(空桶)
    /// </summary>
    /// <remarks>
    ///  StepUpLeakyTokenBucketStrategy resemembles an empty bucket at the beginning but get filled will tokens over a fixed interval.
    /// </remarks>
    class StepUpLeakyTokenBucket : LeakyTokenBucket
    {
        private long lastActivityTime;

        public StepUpLeakyTokenBucket(long maxTokens, long refillInterval, int refillIntervalInMilliSeconds, long stepTokens, long stepInterval, int stepIntervalInMilliseconds) 
            : base(maxTokens, refillInterval, refillIntervalInMilliSeconds, stepTokens, stepInterval, stepIntervalInMilliseconds)
        {
        }

        protected override void UpdateTokens()
        {
            var currentTime = SystemTime.UtcNow.Ticks;

            if (currentTime >= nextRefillTime)
            {
                tokens = stepTokens;

                lastActivityTime = currentTime;
                nextRefillTime = currentTime + ticksRefillInterval;

                return;
            }

            //calculate tokens at current step

            long elapsedTimeSinceLastActivity = currentTime - lastActivityTime;
            long elapsedStepsSinceLastActivity = elapsedTimeSinceLastActivity / ticksStepInterval;

            tokens += (elapsedStepsSinceLastActivity*stepTokens);

            if (tokens > bucketTokenCapacity) tokens = bucketTokenCapacity;
            lastActivityTime = currentTime;
        }
    }
}