using System;
using System.Collections.Generic;
using System.Text;

namespace CZ.FlowControl.Service
{
    using CZ.FlowControl.Spi;
    using System.Threading;

    /// <summary>
    /// 统一流控服务
    /// </summary>
    public class FlowControlService
    {
        /// <summary>
        /// 流控
        /// </summary>
        /// <param name="strategy">流控策略</param>
        /// <param name="count">请求次数</param>
        public static void FlowControl(FlowControlStrategy strategy, int count = 1)
        {
            var controller = FlowControllerFactory.GetInstance().GetOrCreateFlowController(strategy);

            TimeSpan waitTimespan = TimeSpan.Zero;

            var result = controller.ShouldThrottle(count, out waitTimespan);
            if (result)
            {
                if (strategy.IsRefusedRequest == false && waitTimespan != TimeSpan.Zero)
                {
                    WaitForAvailable(strategy, controller, waitTimespan, count);
                }
                else if (strategy.IsRefusedRequest)
                {
                    throw new Exception("触发流控！");
                }
            }
        }

        /// <summary>
        /// 等待可用
        /// </summary>
        /// <param name="strategy">流控策略</param>
        /// <param name="controller">流控器</param>
        /// <param name="waitTimespan">等待时间</param>
        /// <param name="count">请求次数</param>
        private static void WaitForAvailable(FlowControlStrategy strategy, IFlowController controller, TimeSpan waitTimespan, int count)
        {
            var timespan = waitTimespan;
            if (strategy.StrategyType == FlowControlStrategyType.Delay)
            {
                Thread.Sleep(timespan);
                return;
            }

            while (controller.ShouldThrottle(count, out timespan))
            {
                Thread.Sleep(timespan);
            }
        }
    }
}
