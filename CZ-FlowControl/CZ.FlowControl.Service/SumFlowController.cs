using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CZ.FlowControl.Service
{
    using CZ.FlowControl.Spi;

    /// <summary>
    /// 一段时间内合计值流量控制器
    /// </summary>
    class SumFlowController : IFlowController
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

        public SumFlowController(FlowControlStrategy strategy)
        {
            FlowControlStrategy = strategy;

            var refillInterval = GetTokenBucketRefillInterval(strategy);

            InnerThrottleStrategy = new FixedTokenBucket(strategy.IntThreshold, refillInterval, 1000);
        }

        private long GetTokenBucketRefillInterval(FlowControlStrategy strategy)
        {
            long refillInterval = 0;

            switch (strategy.TimeSpan)
            {
                case FlowControlTimespan.Second:
                    refillInterval = 1;
                    break;
                case FlowControlTimespan.Minute:
                    refillInterval = 60;
                    break;
                case FlowControlTimespan.Hour:
                    refillInterval = 60 * 60;
                    break;
                case FlowControlTimespan.Day:
                    refillInterval = 24 * 60 * 60;
                    break;
            }

            return refillInterval;
        }
    }
}