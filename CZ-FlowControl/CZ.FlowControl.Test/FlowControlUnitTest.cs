using System;

namespace CZ.FlowControl.Test
{
    using CZ.FlowControl.Service;
    using CZ.FlowControl.Spi;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Diagnostics;

    /// <summary>
    /// ���ص�Ԫ����
    /// </summary>
    [TestClass]
    public class FlowControlUnitTest
    {
        /// <summary>
        /// TPS���ص�Ԫ����
        /// </summary>
        [TestMethod]
        public void TPSFlowControlTest()
        {
            var strategy = new FlowControlStrategy()
            {
                ID = "TPSFC",
                Name = "����TPS����",
                Creator = "User",
                LastModifier = "User",
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
                Assert.AreEqual(e.Message, "�������أ�");
            }
        }

        /// <summary>
        /// TPS���صȴ���Ԫ����
        /// </summary>
        [TestMethod]
        public void TPSFlowControlWaitTest()
        {
            var strategy = new FlowControlStrategy()
            {
                ID = "TPSFC",
                Name = "����TPS����",
                Creator = "User",
                LastModifier = "User",
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
        /// �����������صȴ���Ԫ����
        /// </summary>
        [TestMethod]
        public void SumFlowControlTest()
        {
            var strategy = new FlowControlStrategy()
            {
                ID = "SumFC",
                Name = "�����ܷ���������",
                Creator = "User",
                LastModifier = "User",
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
                Assert.AreEqual(e.Message, "�������أ�");
            }
        }

        /// <summary>
        /// �����������صȴ���Ԫ����
        /// </summary>
        [TestMethod]
        public void SumFlowControlWaitTest()
        {
            var strategy = new FlowControlStrategy()
            {
                ID = "SumFC",
                Name = "�����ܷ���������",
                Creator = "User",
                LastModifier = "User",
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
        /// �ӳ����صȴ���Ԫ����
        /// </summary>
        [TestMethod]
        public void DelayFlowControlWaitTest()
        {
            var strategy = new FlowControlStrategy()
            {
                ID = "DelayFC",
                Name = "�����ܷ���������",
                Creator = "User",
                LastModifier = "User",
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
