using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WarthunderTelemetry.Base
{
    /// <summary>
    /// 基础Get请求库
    /// </summary>
    public static class BaseGet
    {
        private static readonly HttpClient hc = new HttpClient
        {
            DefaultRequestHeaders =
            {
                CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue
                {
                    NoCache = true, // 禁用缓存
                    NoStore = true  // 不存储缓存
                }
            }
        };
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
        /// <summary>
        /// 获取Indicators资源
        /// </summary>
        /// <returns></returns>
        public static Task<string> GetIndicators() => GetAsync("http://localhost:8111/indicators");
        /// <summary>
        /// 获取State资源
        /// </summary>
        /// <returns></returns>
        public static Task<string> GetState() => GetAsync("http://localhost:8111/state");
        /// <summary>
        /// 获取Mission资源
        /// </summary>
        /// <returns></returns>
        public static Task<string> GetMission() => GetAsync("http://localhost:8111/mission");
        /// <summary>
        /// 获取Hudmsg资源 (id自增, 连续读取注意存储)
        /// </summary>
        /// <param name="lastEvt">要读取的最后id事件(基本不可读)</param>
        /// <param name="lastDmg">要读取的最后id伤害</param>
        /// <returns></returns>
        public static Task<string> GetHudmsg(int lastEvt = 0, int lastDmg = 0) => GetAsync($"http://localhost:8111/hudmsg?lastEvt={lastEvt}&lastDmg={lastDmg}");
        /// <summary>
        /// 获取Gamechat资源 (id自增, 连续读取注意存储)
        /// </summary>
        /// <param name="lastId">最后的id信息</param>
        /// <returns></returns>
        public static Task<string> GetGamechat(int lastId = 0) => GetAsync($"http://localhost:8111/gamechat?lastId={lastId}");
        /// <summary>
        /// 获取Map资源
        /// </summary>
        /// <returns></returns>
        public static Task<byte[]> GetMapImg() => GetByteAsync($"http://localhost:8111/map.img");
        /// <summary>
        /// 获取MapInfo资源
        /// </summary>
        /// <returns></returns>
        public static Task<string> GetMapInfo() => GetAsync($"http://localhost:8111/map_info.json");
        /// <summary>
        /// 获取MapObjInfo资源
        /// </summary>
        /// <returns></returns>
        public static Task<string> GetMapObjInfo() => GetAsync($"http://localhost:8111/map_obj.json");
    }
}
