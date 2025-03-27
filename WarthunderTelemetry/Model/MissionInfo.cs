namespace WarthunderTelemetry.Model
{
    /// <summary>
    /// 任务记录
    /// </summary>
    public class MissionInfo
    {
        /// <summary>
        /// 目标列表
        /// </summary>
        public ObjectiveRecord[]? Objectives { get; set; }
        /// <summary>
        /// 任务状态
        /// </summary>
        public string? Status { get; set; }
    }

}
