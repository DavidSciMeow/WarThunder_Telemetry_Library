namespace WarthunderTelemetry.Model
{
    /// <summary>
    /// 目标记录信息
    /// </summary>
    public class ObjectiveRecord
    {
        /// <summary>
        /// 是否主要目标
        /// </summary>
        public bool Primary { get; set; }
        /// <summary>
        /// 目标状态
        /// </summary>
        public string? Status { get; set; }
        /// <summary>
        /// 显示信息
        /// </summary>
        public string? Text { get; set; }
    }

}
