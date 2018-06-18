using System;
using System.Collections.Generic;
using System.Text;

namespace CZ.FlowControl.Service
{
    using CZ.FlowControl.Spi;

    /// <summary>
    /// 延迟流控
    /// </summary>
    class DelayFlowController : IFlowController
    {
        public IThrottleStrategy InnerThrottleStrategy
        {
            get; private set;
        }

        public FlowControlStrategy FlowControlStrategy { get; private set; }

        public bool ShouldThrottle(long n, out TimeSpan waitTime)
        {
            waitTime = TimeSpan.FromMilliseconds(FlowControlStrategy.IntThreshold);

            return true;
        }

        public DelayFlowController(FlowControlStrategy strategy)
        {
            FlowControlStrategy = strategy;
        }
    }
}
