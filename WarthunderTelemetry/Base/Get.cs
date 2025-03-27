using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using WarthunderTelemetry.Data;
using WarthunderTelemetry.Model;

namespace WarthunderTelemetry.Base
{
    /// <summary>
    /// Get请求库
    /// </summary>
    public static class Get
    {
        private static string? _prevmapjson;
        private static byte[] map = Map.GenerateDefaultMapImage();
        /// <summary>
        /// 获取Indicators资源任务(非阻塞态)
        /// </summary>
        /// <returns></returns>
        public static async Task<IndicatorsInfo?> GetIndicatorsInfo() => new IndicatorsInfo(JObject.Parse(await BaseGet.GetIndicators()));
        /// <summary>
        /// 获取MapImg资源任务(非阻塞态)<br/>
        /// 优化模式缓存*
        /// </summary>
        /// <returns></returns>
        public static async Task<byte[]?> GetMapImgAsync()
        {
            var _strmap = await BaseGet.GetMapInfo();
            var nowmap = JObject.Parse(_strmap);
            if (nowmap["valid"]?.ToString().ToLowerInvariant().Equals("false") ?? false) return Map.GenerateDefaultMapImage();
            if (_prevmapjson != _strmap)
            {
                _prevmapjson = _strmap;
                map = await BaseGet.GetMapImg();
            }
            return Map.Initialize(nowmap, JArray.Parse(await BaseGet.GetMapObjInfo()), map);
        }
        /// <summary>
        /// 获取State资源任务(非阻塞态)
        /// </summary>
        /// <returns></returns>
        public static async Task<StateInfo?> GetStateInfo() => new StateInfo(JObject.Parse(await BaseGet.GetState()));
    }

}
