using System;

namespace CZ.FlowControl.Test
{
    using CZ.FlowControl.Service;
    using CZ.FlowControl.Spi;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Diagnostics;

    /// <summary>
    /// 流控单元测试
    /// </summary>
    [TestClass]
    public class FlowControlUnitTest
    {
        /// <summary>
        /// TPS流控单元测试
        /// </summary>
        [TestMethod]
        public void TPSFlowControlTest()
        {
            var strategy = new FlowControlStrategy()
            {
                ID = "TPSFC",
                Name = "服务TPS限流",
                Creator = "Teld",
                LastModifier = "Teld",
                CreateTime = DateTime.Now,
                LastModifyTime = DateTime.Now,
                StrategyType = FlowControlStrategyType.TPS,
                IntThreshold = 100,
                IsRefusedRequest = true
            };

            FlowControlService.FlowControl(strategy, 1);

            try
            {
                for (int i = 0; i < 300; i++)
                {
                    FlowControlService.FlowControl(strategy, 1);
                }
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "触发流控！");
            }
        }

        /// <summary>
        /// TPS流控等待单元测试
        /// </summary>
        [TestMethod]
        public void TPSFlowControlWaitTest()
        {
            var strategy = new FlowControlStrategy()
            {
                ID = "TPSFC",
                Name = "服务TPS限流",
                Creator = "Teld",
                LastModifier = "Teld",
                CreateTime = DateTime.Now,
                LastModifyTime = DateTime.Now,
                StrategyType = FlowControlStrategyType.TPS,
                IntThreshold = 100,
                IsRefusedRequest = false
            };

            FlowControlService.FlowControl(strategy, 1);

            for (int i = 0; i < 300; i++)
            {
                FlowControlService.FlowControl(strategy, 1);
            }
        }

        /// <summary>
        /// 访问总量流控等待单元测试
        /// </summary>
        [TestMethod]
        public void SumFlowControlTest()
        {
            var strategy = new FlowControlStrategy()
            {
                ID = "SumFC",
                Name = "服务总访问量限流",
                Creator = "Teld",
                LastModifier = "Teld",
                CreateTime = DateTime.Now,
                LastModifyTime = DateTime.Now,
                StrategyType = FlowControlStrategyType.Sum,
                IntThreshold = 100,
                TimeSpan = FlowControlTimespan.Second,
                IsRefusedRequest = true
            };

            FlowControlService.FlowControl(strategy, 1);

            try
            {
                for (int i = 0; i < 300; i++)
                {
                    FlowControlService.FlowControl(strategy, 1);
                }
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "触发流控！");
            }
        }

        /// <summary>
        /// 访问总量流控等待单元测试
        /// </summary>
        [TestMethod]
        public void SumFlowControlWaitTest()
        {
            var strategy = new FlowControlStrategy()
            {
                ID = "SumFC",
                Name = "服务总访问量限流",
                Creator = "Teld",
                LastModifier = "Teld",
                CreateTime = DateTime.Now,
                LastModifyTime = DateTime.Now,
                StrategyType = FlowControlStrategyType.Sum,
                IntThreshold = 100,
                TimeSpan = FlowControlTimespan.Second,
                IsRefusedRequest = false
            };

            FlowControlService.FlowControl(strategy, 1);

            for (int i = 0; i < 300; i++)
            {
                FlowControlService.FlowControl(strategy, 1);
            }
        }

        /// <summary>
        /// 延迟流控等待单元测试
        /// </summary>
        [TestMethod]
        public void DelayFlowControlWaitTest()
        {
            var strategy = new FlowControlStrategy()
            {
                ID = "DelayFC",
                Name = "服务总访问量限流",
                Creator = "Teld",
                LastModifier = "Teld",
                CreateTime = DateTime.Now,
                LastModifyTime = DateTime.Now,
                StrategyType = FlowControlStrategyType.Delay,
                IntThreshold = 100,
                TimeSpan = FlowControlTimespan.Second,
                IsRefusedRequest = false
            };

            FlowControlService.FlowControl(strategy, 1);

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            for (int i = 0; i < 100; i++)
            {
                FlowControlService.FlowControl(strategy, 1);
            }

            stopWatch.Stop();
            Assert.IsTrue(stopWatch.ElapsedMilliseconds > 100 * 100);
        }
    }
}
