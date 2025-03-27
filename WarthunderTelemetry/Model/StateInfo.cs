using Newtonsoft.Json.Linq;

namespace WarthunderTelemetry.Model
{
    /// <summary>
    /// 飞行载具状态指标
    /// </summary>
    public struct StateInfo
    {
        /// <summary>
        /// 初始化飞行载具状态指标
        /// </summary>
        /// <param name="jo">相关的JObject</param>
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

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool Valid { get; set; }
        /// <summary>
        /// 副翼, %
        /// </summary>
        public int Aileron { get; set; }
        /// <summary>
        /// 升降舵, %
        /// </summary>
        public int Elevator { get; set; }
        /// <summary>
        /// 方向舵, %
        /// </summary>
        public int Rudder { get; set; }
        /// <summary>
        /// 襟翼, %
        /// </summary>
        public int Flaps { get; set; }
        /// <summary>
        /// 起落架, %
        /// </summary>
        public int Gear { get; set; }
        /// <summary>
        /// 减速板, %
        /// </summary>
        public int Airbrake { get; set; }
        /// <summary>
        /// 高度, m
        /// </summary>
        public int Hm { get; set; }
        /// <summary>
        /// 真空速, km/h
        /// </summary>
        public int TASkmh { get; set; }
        /// <summary>
        /// 指示速度, km/h
        /// </summary>
        public int IASkmh { get; set; }
        /// <summary>
        /// 马赫数
        /// </summary>
        public float M { get; set; }
        /// <summary>
        /// 攻角, deg
        /// </summary>
        public float AoAdeg { get; set; }
        /// <summary>
        /// 侧滑角, deg
        /// </summary>
        public float AoSdeg { get; set; }
        /// <summary>
        /// 垂直G力 (G倍)
        /// </summary>
        public float Ny { get; set; }
        /// <summary>
        /// 垂直速率, m/s
        /// </summary>
        public float Vyms { get; set; }
        /// <summary>
        /// 切向弧度, deg/s
        /// </summary>
        public int Wxdegs { get; set; }
        /// <summary>
        /// 油量, kg
        /// </summary>
        public int Mfuelkg { get; set; }
        /// <summary>
        /// 油量0, kg
        /// </summary>
        public int Mfuel0kg { get; set; }
        /// <summary>
        /// 油门1, %
        /// </summary>
        public int Throttle1 { get; set; }
        /// <summary>
        /// 功率1, hp
        /// </summary>
        public float Power1hp { get; set; }
        /// <summary>
        /// 引擎转速1, rpm
        /// </summary>
        public int RPM1 { get; set; }
        /// <summary>
        /// 进气压力1, atm
        /// </summary>
        public float Manifoldpressure1atm { get; set; }
        /// <summary>
        /// 油温1, C
        /// </summary>
        public int Oiltemp1C { get; set; }
        /// <summary>
        /// 推力1, kgs
        /// </summary>
        public int Thrust1kgs { get; set; }
        /// <summary>
        /// 燃油效率1, %
        /// </summary>
        public int Efficiency1 { get; set; }
        /// <summary>
        /// 油门2, %
        /// </summary>
        public int Throttle2 { get; set; }
        /// <summary>
        /// 功率2, hp
        /// </summary>
        public float Power2hp { get; set; }
        /// <summary>
        /// 引擎转速2, rpm
        /// </summary>
        public int RPM2 { get; set; }
        /// <summary>
        /// 进气压力2, atm
        /// </summary>
        public float Manifoldpressure2atm { get; set; }
        /// <summary>
        /// 油温2, C
        /// </summary>
        public int Oiltemp2C { get; set; }
        /// <summary>
        /// 推力2, kgs
        /// </summary>
        public int Thrust2kgs { get; set; }
        /// <summary>
        /// 燃油效率2, %
        /// </summary>
        public int Efficiency2 { get; set; }
        /// <inheritdoc/>
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
