using Newtonsoft.Json.Linq;
using System.Linq;

namespace WarthunderTelemetry
{
    /// <summary>
    /// 工具组
    /// </summary>
    public static class Util
    {
        /// <summary>
        /// 从JObject中提取动态数组
        /// </summary>
        /// <typeparam name="Type">数组类型</typeparam>
        /// <param name="data">数据</param>
        /// <param name="prefix">前缀标</param>
        /// <returns></returns>
        public static Type[] ExtractDynamicArray<Type>(this JObject data, string prefix) where Type : struct
        {
            int maxIndex = data.Properties()
                                .Where(p => p.Name.StartsWith(prefix))
                                .Select(p => int.TryParse(p.Name.Replace(p.Name, ""), out int number) ? number : 0)
                                .DefaultIfEmpty(0)
                                .Max();

            Type[] result = new Type[maxIndex];

            for (int i = 1; i <= maxIndex; i++)
            {
                string key = $"{prefix}{i}";
                result[i - 1] = data[key]?.Value<Type>() ?? default;
            }
            return result;
        }
    }
}
