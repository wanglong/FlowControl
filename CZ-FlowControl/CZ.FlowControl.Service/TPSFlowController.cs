using System;

namespace CZ.FlowControl.Service
{
    using CZ.FlowControl.Spi;

    /// <summary>
    /// TPS流量控制器
    /// </summary>
    class TPSFlowController : IFlowController
    {
        public IThrottleStrategy InnerThrottleStrategy
        {
            get; private set;
        }

        public FlowControlStrategy FlowControlStrategy { get; private set; }

        public bool ShouldThrottle(long n, out TimeSpan waitTime)
        {
            return InnerThrottleStrategy.ShouldThrottle(n, out waitTime);
        }

        public TPSFlowController(FlowControlStrategy strategy)
        {
            FlowControlStrategy = strategy;

            InnerThrottleStrategy = new FixedTokenBucket(strategy.IntThreshold, 1, 1000);
        }
    }
}
