using System;

namespace CZ.FlowControl.Service
{
    using CZ.FlowControl.Spi;

    class Throttler
    {
        private readonly IThrottleStrategy strategy;

        public Throttler(IThrottleStrategy strategy)
        {
            if (strategy == null) throw new ArgumentNullException("strategy");
            this.strategy = strategy;
        }

        public bool CanConsume()
        {
            return !strategy.ShouldThrottle();
        }
    }
}
