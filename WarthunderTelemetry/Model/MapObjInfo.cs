using Newtonsoft.Json.Linq;

namespace WarthunderTelemetry.Model
{
    public class MapObjInfo
    {
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
        public string Type { get; set; }
        public string Color { get; set; }
        public bool Blink { get; set; }
        public string Icon { get; set; }
        public string Icon_bg { get; set; }
        public float Sx { get; set; }
        public float Sy { get; set; }
        public float Ex { get; set; }
        public float Ey { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Dx { get; set; }
        public float Dy { get; set; }

        public override string ToString() => $"[{Type}] {X}:{Y} **({Sx}:{Dx}/{Sy}:{Dy})";
    }

}
