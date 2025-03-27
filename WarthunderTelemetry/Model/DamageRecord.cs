using System;
using System.Drawing;

namespace WarthunderTelemetry.Model
{
    /// <summary>
    /// 伤害记录
    /// </summary>
    public struct DamageRecord
    {
        /// <summary>
        /// 伤害Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 具体伤害信息
        /// </summary>
        public string? Msg { get; set; }
        /// <summary>
        /// 发送者
        /// </summary>
        public string? Sender { get; set; }
        /// <summary>
        /// 是否敌军
        /// </summary>
        public bool Enemy { get; set; }
        /// <summary>
        /// 发送模式
        /// </summary>
        public string? Mode { get; set; }
        /// <summary>
        /// 距战局开始时间
        /// </summary>
        public int Time { get; set; }
    }

}
