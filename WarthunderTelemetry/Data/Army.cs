using System.Threading.Tasks;
using WarthunderTelemetry.Base;
using WarthunderTelemetry.Model;

namespace WarthunderTelemetry.Data
{
    /// <summary>
    /// 载具信息
    /// </summary>
    public static class Army
    {
        /// <summary>
        /// 载具类型 0:空战 1:地面
        /// </summary>
        public static int Type { get; private set; }
        /// <summary>
        /// 空战载具信息
        /// </summary>
        public static StateInfo? StateInfo { get; private set; }
        /// <summary>
        /// 陆战载具信息
        /// </summary>
        public static IndicatorsInfo? IndicatorsInfo { get; private set; }
        /// <summary>
        /// 获取载具信息任务(非阻塞态)
        /// </summary>
        /// <returns></returns>
        public static async Task<string> GetInfoAsync()
        {
            var gii = await Get.GetIndicatorsInfo();
            if (gii?.Army == "air")
            {
                Type = 0;
                StateInfo = await Get.GetStateInfo();
                IndicatorsInfo = gii;
            }
            else
            {
                Type = 1;
                IndicatorsInfo = gii;
            }
            return Type == 0 ? IndicatorsInfo?.ToString() + StateInfo?.ToString() ?? "" : IndicatorsInfo?.ToString() ?? "";
        }
    }
}