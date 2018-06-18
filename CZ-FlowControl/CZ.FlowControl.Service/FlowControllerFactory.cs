using System;
using System.Collections.Generic;
using System.Text;

namespace CZ.FlowControl.Service
{
    using CZ.FlowControl.Spi;

    /// <summary>
    /// 流控策略工厂
    /// </summary>
    class FlowControllerFactory
    {
        private static Dictionary<string, IFlowController> fcControllers;
        private static object syncObj = new object();

        private static FlowControllerFactory instance;

        private FlowControllerFactory()
        {
            fcControllers = new Dictionary<string, IFlowController>();
        }

        public static FlowControllerFactory GetInstance()
        {
            if (instance == null)
            {
                lock (syncObj)
                {
                    if (instance == null)
                    {
                        instance = new FlowControllerFactory();
                    }
                }
            }

            return instance;
        }

        public IFlowController GetOrCreateFlowController(FlowControlStrategy strategy)
        {
            if (strategy == null)
                throw new ArgumentNullException("FlowControllerFactory.GetOrCreateFlowController.strategy");

            if (!fcControllers.ContainsKey(strategy.ID))
            {
                lock (syncObj)
                {
                    if (!fcControllers.ContainsKey(strategy.ID))
                    {
                        var fcController = CreateFlowController(strategy);
                        if (fcController != null)
                            fcControllers.Add(strategy.ID, fcController);
                    }
                }
            }

            if (fcControllers.ContainsKey(strategy.ID))
            {
                var controller = fcControllers[strategy.ID];
                return controller;
            }

            return null;
        }

        private IFlowController CreateFlowController(FlowControlStrategy strategy)
        {
            if (strategy == null)
                throw new ArgumentNullException("FlowControllerFactory.CreateFlowController.strategy");

            IFlowController controller = null;

            switch (strategy.StrategyType)
            {
                case FlowControlStrategyType.TPS:
                    controller = new TPSFlowController(strategy);
                    break;
                case FlowControlStrategyType.Delay:
                    controller = new DelayFlowController(strategy);
                    break;
                case FlowControlStrategyType.Sum:
                    controller = new SumFlowController(strategy);
                    break;
                default:
                    break;
            }

            return controller;
        }
    }
}
