using System;
using System.Collections.Generic;
using System.Text;

namespace CZ.FlowControl.Spi
{
    /// <summary>
    /// 流控策略类型枚举
    /// </summary>
    public enum FlowControlStrategyType
    {
        /// <summary>
        /// TPS控制策略
        /// </summary>
        TPS,

        /// <summary>
        /// 并发控制策略
        /// </summary>
        Concurrent,

        /// <summary>
        /// 总数控制策略
        /// </summary>
        Sum,

        /// <summary>
        /// 延迟控制策略
        /// </summary>
        Delay
    }
}
