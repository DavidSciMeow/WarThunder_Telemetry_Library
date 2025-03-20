using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using WarthunderTelemetry.Data;
using WarthunderTelemetry.Model;

namespace WarthunderTelemetry.Base
{
    public static class Get
    {
        public static string? _prevmapjson;
        public static byte[] map = new byte[0];
        public static async Task<IndicatorsInfo?> GetIndicatorsInfo() => new IndicatorsInfo(JObject.Parse(await BaseGet.GetIndicators()));
        public static async Task<byte[]?> GetMapImgAsync()
        {
            var _strmap = await BaseGet.GetMapInfo();
            var nowmap = JObject.Parse(_strmap);
            if (nowmap["valid"]?.ToString().ToLowerInvariant().Equals("false") ?? false) return null;
            if (_prevmapjson != _strmap)
            {
                _prevmapjson = _strmap;
                map = await BaseGet.GetMapImg();
            }
            return Map.Initialize(nowmap, JArray.Parse(await BaseGet.GetMapObjInfo()), map);
        }

        public static async Task<StateInfo?> GetStateInfo() => new StateInfo(JObject.Parse(await BaseGet.GetState()));
    }

}
