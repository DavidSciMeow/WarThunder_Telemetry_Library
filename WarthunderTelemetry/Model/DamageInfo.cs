namespace WarthunderTelemetry.Model
{
    /// <summary>
    /// 信息
    /// </summary>
    public struct DamageInfo
    {
        /// <summary>
        /// 事件信息
        /// </summary>
        public object[]? Events { get; set; }
        /// <summary>
        /// 伤害事件信息
        /// </summary>
        public DamageRecord[]? Damage { get; set; }
        
    }

}
