using System;
using System.Collections.Generic;
using System.Text;

namespace CZ.FlowControl.Spi
{
    /// <summary>
    /// 流控时间单位
    /// 0：秒
    /// 1：分
    /// 2：小时
    /// 3：天
    /// </summary>
    public enum FlowControlTimespan
    {
        /// <summary>
        /// 秒
        /// </summary>
        Second,
        /// <summary>
        /// 分钟
        /// </summary>
        Minute,
        /// <summary>
        /// 小时
        /// </summary>
        Hour,
        /// <summary>
        /// 天
        /// </summary>
        Day
    }
}
