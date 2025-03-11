using System.Net.Http;
using System.Threading.Tasks;

namespace WarthunderTelemetry
{
    public static class Get
    {
        private static readonly HttpClient hc = new HttpClient();
        private static Task<string> GetAsync(string url)
        {
            try
            {
                return hc.GetStringAsync(url);
            }
            catch
            {
                return Task.FromResult("");
            }
        }

        private static Task<byte[]> GetByteAsync(string url)
        {
            try
            {
                return hc.GetByteArrayAsync(url);
            }
            catch
            {
                return Task.FromResult(new byte[0]);
            }
        }

        public static Task<string> GetIndicators() => GetAsync("http://localhost:8111/indicators");
        public static Task<string> GetState() => GetAsync("http://localhost:8111/state");
        public static Task<string> GetMission() => GetAsync("http://localhost:8111/mission");
        public static Task<string> GetHudmsg(int lastEvt = 0, int lastDmg = 0) => GetAsync($"http://localhost:8111/hudmsg?lastEvt={lastEvt}&lastDmg={lastDmg}");
        public static Task<string> GetGamechat(int lastId = 0) => GetAsync($"http://localhost:8111/gamechat?lastId={lastId}");
        public static Task<byte[]> GetMapImg() => GetByteAsync($"http://localhost:8111/map.img");
        public static Task<string> GetMapInfo() => GetAsync($"http://localhost:8111/map_info.json");
        public static Task<string> GetMapObjInfo() => GetAsync($"http://localhost:8111/map_obj.json");
    }

}
