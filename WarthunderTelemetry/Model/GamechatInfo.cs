namespace WarthunderTelemetry.Model
{
    /// <summary>
    /// 游戏内聊天信息
    /// </summary>
    public struct GamechatInfo
    {
        /// <summary>
        /// 信息id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 信息内容
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
    }

}
