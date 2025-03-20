using System.Threading.Tasks;
using WarthunderTelemetry.Base;
using WarthunderTelemetry.Model;

namespace WarthunderTelemetry.Data
{
    public static class Army
    {
        public static int Type { get; private set; }
        public static StateInfo? StateInfo { get; private set; }
        public static IndicatorsInfo? IndicatorsInfo { get; private set; }

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