using Newtonsoft.Json.Linq;
using System.Linq;

namespace WarthunderTelemetry
{
    public static class Util
    {
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
