using Newtonsoft.Json.Linq;

namespace WarthunderTelemetry.Model
{
    public struct StateInfo
    {
        public StateInfo(JObject jo)
        {
            Valid = jo["valid"]?.ToString() == "true";
            Aileron = jo["aileron, %"]?.ToObject<int>() ?? 0;
            Elevator = jo["elevator, %"]?.ToObject<int>() ?? 0;
            Rudder = jo["rudder, %"]?.ToObject<int>() ?? 0;
            Flaps = jo["flaps, %"]?.ToObject<int>() ?? 0;
            Gear = jo["gear, %"]?.ToObject<int>() ?? 0;
            Airbrake = jo["airbrake, %"]?.ToObject<int>() ?? 0;
            Hm = jo["H, m"]?.ToObject<int>() ?? 0;
            TASkmh = jo["TAS, km/h"]?.ToObject<int>() ?? 0;
            IASkmh = jo["IAS, km/h"]?.ToObject<int>() ?? 0;
            M = jo["M"]?.ToObject<float>() ?? 0.0f;
            AoAdeg = jo["AoA, deg"]?.ToObject<float>() ?? 0.0f;
            AoSdeg = jo["AoS, deg"]?.ToObject<float>() ?? 0.0f;
            Ny = jo["Ny"]?.ToObject<float>() ?? 0.0f;
            Vyms = jo["Vy, m/s"]?.ToObject<float>() ?? 0.0f;
            Wxdegs = jo["Wx, deg/s"]?.ToObject<int>() ?? 0;
            Mfuelkg = jo["Mfuel, kg"]?.ToObject<int>() ?? 0;
            Mfuel0kg = jo["Mfuel0, kg"]?.ToObject<int>() ?? 0;
            Throttle1 = jo["throttle 1, %"]?.ToObject<int>() ?? 0;
            Power1hp = jo["power 1, hp"]?.ToObject<float>() ?? 0.0f;
            RPM1 = jo["RPM 1"]?.ToObject<int>() ?? 0;
            Manifoldpressure1atm = jo["manifold pressure 1, atm"]?.ToObject<float>() ?? 0.0f;
            Oiltemp1C = jo["oil temp 1, C"]?.ToObject<int>() ?? 0;
            Thrust1kgs = jo["thrust 1, kgs"]?.ToObject<int>() ?? 0;
            Efficiency1 = jo["efficiency 1, %"]?.ToObject<int>() ?? 0;
            Throttle2 = jo["throttle 2, %"]?.ToObject<int>() ?? 0;
            Power2hp = jo["power 2, hp"]?.ToObject<float>() ?? 0.0f;
            RPM2 = jo["RPM 2"]?.ToObject<int>() ?? 0;
            Manifoldpressure2atm = jo["manifold pressure 2, atm"]?.ToObject<float>() ?? 0.0f;
            Oiltemp2C = jo["oil temp 2, C"]?.ToObject<int>() ?? 0;
            Thrust2kgs = jo["thrust 2, kgs"]?.ToObject<int>() ?? 0;
            Efficiency2 = jo["efficiency 2, %"]?.ToObject<int>() ?? 0;
        }

        public bool Valid { get; set; }
        public int Aileron { get; set; }
        public int Elevator { get; set; }
        public int Rudder { get; set; }
        public int Flaps { get; set; }
        public int Gear { get; set; }
        public int Airbrake { get; set; }
        public int Hm { get; set; }
        public int TASkmh { get; set; }
        public int IASkmh { get; set; }
        public float M { get; set; }
        public float AoAdeg { get; set; }
        public float AoSdeg { get; set; }
        public float Ny { get; set; }
        public float Vyms { get; set; }
        public int Wxdegs { get; set; }
        public int Mfuelkg { get; set; }
        public int Mfuel0kg { get; set; }
        public int Throttle1 { get; set; }
        public float Power1hp { get; set; }
        public int RPM1 { get; set; }
        public float Manifoldpressure1atm { get; set; }
        public int Oiltemp1C { get; set; }
        public int Thrust1kgs { get; set; }
        public int Efficiency1 { get; set; }
        public int Throttle2 { get; set; }
        public float Power2hp { get; set; }
        public int RPM2 { get; set; }
        public float Manifoldpressure2atm { get; set; }
        public int Oiltemp2C { get; set; }
        public int Thrust2kgs { get; set; }
        public int Efficiency2 { get; set; }

        public override readonly string ToString() => $"\n" +
            $"IAS:{TASkmh:F2} km/h ({M:F2}Mach)\n" +
            $"攻角:{AoAdeg:F2}°\n" +
            $"侧滑角:{AoSdeg:F2}°\n" +
            $"垂直G力:{Ny:F2}G\n" +
            $"垂直速率:{Vyms:F2}m/s \n" +
            $"切向弧度:{Wxdegs:F2}deg/s\n" +
            $"---------------\n" +
            $"起落架释放量:{Gear:F2}% \n" +
            $"襟翼释放量:{Flaps:F2}% \n" +
            $"减速板释放量:{Airbrake:F2}%\n" +
            $"---------------\n" +
            $"油门变距杆:{Throttle1:F2}/{Throttle2:F2}% \n" +
            $"引擎转速(rpm):{RPM1:F2}/{RPM2:F2} \n" +
            $"推力:{Thrust1kgs:F2}/{Thrust2kgs:F2} kg/N\n" +
            $"油量:{Mfuelkg:F2}/{Mfuel0kg:F2} kg \n" +
            $"油温:{Oiltemp1C:F2}/{Oiltemp2C:F2}°C \n" +
            $"功率量:{Power1hp:F2}/{Power2hp:F2} hp \n" +
            $"燃油效率:{Efficiency1:F2}/{Efficiency2:F2}%\n";
    }
}
