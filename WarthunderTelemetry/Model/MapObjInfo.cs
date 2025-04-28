using Newtonsoft.Json.Linq;
using System.Runtime.CompilerServices;

namespace WarthunderTelemetry.Model
{
    /// <summary>
    /// 地图对象信息
    /// </summary>
    public struct MapObjInfo
    {
        /// <summary>
        /// 初始化地图对象信息
        /// </summary>
        /// <param name="i">相关的JToken</param>
        public MapObjInfo(JToken i)
        {
            Type = i[nameof(Type).ToLowerInvariant()]?.ToString() ?? "";
            Color = i[nameof(Color).ToLowerInvariant()]?.ToString() ?? "";
            Blink = i[nameof(Blink).ToLowerInvariant()]?.ToObject<int>() == 1;
            Icon = i[nameof(Icon).ToLowerInvariant()]?.ToString() ?? "";
            Icon_bg = i[nameof(Icon_bg).ToLowerInvariant()]?.ToString() ?? "";
            Sx = i[nameof(Sx).ToLowerInvariant()]?.ToObject<float>() ?? 0;
            Sy = i[nameof(Sy).ToLowerInvariant()]?.ToObject<float>() ?? 0;
            Ex = i[nameof(Ex).ToLowerInvariant()]?.ToObject<float>() ?? 0;
            Ey = i[nameof(Ey).ToLowerInvariant()]?.ToObject<float>() ?? 0;
            X = i[nameof(X).ToLowerInvariant()]?.ToObject<float>() ?? 0;
            Y = i[nameof(Y).ToLowerInvariant()]?.ToObject<float>() ?? 0;
            Dx = i[nameof(Dx).ToLowerInvariant()]?.ToObject<float>() ?? 0;
            Dy = i[nameof(Dy).ToLowerInvariant()]?.ToObject<float>() ?? 0;
        }
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 颜色(#XXXXXX)
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// 是否闪烁
        /// </summary>
        public bool Blink { get; set; }
        /// <summary>
        /// 图标类型
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 图标背景
        /// </summary>
        public string Icon_bg { get; set; }
        /// <summary>
        /// Start X (启始坐标轴X)
        /// </summary>
        public float Sx { get; set; }
        /// <summary>
        /// Start Y (启始坐标轴Y)
        /// </summary>
        public float Sy { get; set; }
        /// <summary>
        /// End X (结束坐标轴X)
        /// </summary>
        public float Ex { get; set; }
        /// <summary>
        /// End Y (结束坐标轴Y)
        /// </summary>
        public float Ey { get; set; }
        /// <summary>
        /// 当前X轴
        /// </summary>
        public float X { get; set; }
        /// <summary>
        /// 当前Y轴
        /// </summary>
        public float Y { get; set; }
        /// <summary>
        /// Delta X (X轴变化量)
        /// </summary>
        public float Dx { get; set; }
        /// <summary>
        /// Delta Y (Y轴变化量)
        /// </summary>
        public float Dy { get; set; }
        /// <inheritdoc/>
        public override readonly string ToString() => $"[{Type}] {X}:{Y} **({Sx}:{Dx}/{Sy}:{Dy})";
    }
}
