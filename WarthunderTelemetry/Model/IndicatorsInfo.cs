using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Xml.Schema;

namespace WarthunderTelemetry.Model
{
    public class IndicatorsInfo
    {
        public IndicatorsInfo(JObject jo)
        {
            Valid = jo[nameof(Valid).ToLowerInvariant()]?.ToString() == "true";
            Army = jo[nameof(Army).ToLowerInvariant()]?.ToString() ?? "";
            Type = jo[nameof(Type).ToLowerInvariant()]?.ToString() ?? "";
            if (Army == "air")
            {
                Speed = jo[nameof(Speed).ToLowerInvariant()]?.Value<float>() ?? 0.0f;
                Stick_Elevator = jo[nameof(Stick_Elevator).ToLowerInvariant()]?.Value<float>() ?? 0.0f;
                Stick_Ailerons = jo[nameof(Stick_Ailerons).ToLowerInvariant()]?.Value<float>() ?? 0.0f;
                Vario = jo[nameof(Vario).ToLowerInvariant()]?.Value<float>() ?? 0.0f;
                AltitudeHour = jo[nameof(AltitudeHour).ToLowerInvariant()]?.Value<float>() ?? 0.0f;
                AltitudeMin = jo[nameof(AltitudeMin).ToLowerInvariant()]?.Value<float>() ?? 0.0f;
                Altitude10k = jo[nameof(Altitude10k).ToLowerInvariant()]?.Value<float>() ?? 0.0f;
                Aviahorizon_Roll = jo[nameof(Aviahorizon_Roll).ToLowerInvariant()]?.Value<float>() ?? 0.0f;
                Aviahorizon_Pitch = jo[nameof(Aviahorizon_Pitch).ToLowerInvariant()]?.Value<float>() ?? 0.0f;
                Compass = jo[nameof(Compass).ToLowerInvariant()]?.Value<float>() ?? 0.0f;
                Clock_Hour = jo[nameof(Clock_Hour).ToLowerInvariant()]?.Value<float>() ?? 0.0f;
                Clock_Min = jo[nameof(Clock_Min).ToLowerInvariant()]?.Value<float>() ?? 0.0f;
                Clock_Sec = jo[nameof(Clock_Sec).ToLowerInvariant()]?.Value<float>() ?? 0.0f;
                Rpm_Min = jo[nameof(Rpm_Min).ToLowerInvariant()]?.Value<float>() ?? 0.0f;
                Rpm_Hour = jo[nameof(Rpm_Hour).ToLowerInvariant()]?.Value<float>() ?? 0.0f;
                Oil_Pressure = jo[nameof(Oil_Pressure).ToLowerInvariant()]?.Value<float>() ?? 0.0f;
                Water_Temperature = jo[nameof(Water_Temperature).ToLowerInvariant()]?.Value<float>() ?? 0.0f;
                Fuel = jo[nameof(Fuel).ToLowerInvariant()]?.Value<float>() ?? 0.0f;
                Fuel_Consume = jo[nameof(Fuel_Consume).ToLowerInvariant()]?.Value<float>() ?? 0.0f;
                Airbrake_Lever = jo[nameof(Airbrake_Lever).ToLowerInvariant()]?.Value<float>() == 1;
                Gears = jo[nameof(Gears).ToLowerInvariant()]?.Value<float>() == 1;
                Flaps = jo[nameof(Flaps).ToLowerInvariant()]?.Value<float>() == 1;
                Throttle = jo[nameof(Throttle).ToLowerInvariant()]?.Value<float>() ?? 0.0f;
                Mach = jo[nameof(Mach).ToLowerInvariant()]?.Value<float>() ?? 0.0f;
                G_Meter = jo[nameof(G_Meter).ToLowerInvariant()]?.Value<float>() ?? 0.0f;
                Aoa = jo[nameof(Aoa).ToLowerInvariant()]?.Value<float>() ?? 0.0f;

                // 解析数组数据
                Pedals = jo.ExtractDynamicArray<float>("pedals");
                Blisters = jo.ExtractDynamicArray<int>("blister");
                GearLamps = jo.ExtractDynamicArray<int>("gear_lamp");
            }
            else if (Army == "tank")
            {
                Breech_damaged = float.Parse(jo[nameof(Breech_damaged).ToLowerInvariant()]?.ToString() ?? "0") == 1;
                Barrel_dead = float.Parse(jo[nameof(Barrel_dead).ToLowerInvariant()]?.ToString() ?? "0") == 1;
                Engine_broken = float.Parse(jo[nameof(Engine_broken).ToLowerInvariant()]?.ToString() ?? "0") == 1;
                Engine_dead = float.Parse(jo[nameof(Engine_dead).ToLowerInvariant()]?.ToString() ?? "0") == 1;
                V_drive_broken = float.Parse(jo[nameof(V_drive_broken).ToLowerInvariant()]?.ToString() ?? "0") == 1;
                H_drive_dead = float.Parse(jo[nameof(H_drive_dead).ToLowerInvariant()]?.ToString() ?? "0") == 1;
                Is_repairing_auto = float.Parse(jo[nameof(Is_repairing_auto).ToLowerInvariant()]?.ToString() ?? "0") == 1;
                Transmission_broken = float.Parse(jo[nameof(Transmission_broken).ToLowerInvariant()]?.ToString() ?? "0") == 1;
                Track_broken = float.Parse(jo[nameof(Track_broken).ToLowerInvariant()]?.ToString() ?? "0") == 1;
                Stabilizer = float.Parse(jo[nameof(Stabilizer).ToLowerInvariant()]?.ToString() ?? "0") == 1;
                Gear = int.Parse(jo[nameof(Gear).ToLowerInvariant()]?.ToString() ?? "0");
                Gear_neutral = int.Parse(jo[nameof(Gear_neutral).ToLowerInvariant()]?.ToString() ?? "0");
                Speed = float.Parse(jo[nameof(Speed).ToLowerInvariant()]?.ToString() ?? "0");
                Has_speed_warning = float.Parse(jo[nameof(Has_speed_warning).ToLowerInvariant()]?.ToString() ?? "0") == 1;
                Rpm = float.Parse(jo[nameof(Rpm).ToLowerInvariant()]?.ToString() ?? "0");
                Driving_direction_mode = float.Parse(jo[nameof(Driving_direction_mode).ToLowerInvariant()]?.ToString() ?? "0") == 1;
                Cruise_control = int.Parse(jo[nameof(Cruise_control).ToLowerInvariant()]?.ToString() ?? "0");
                Lws = int.Parse(jo[nameof(Lws).ToLowerInvariant()]?.ToString() ?? "0");
                Ircm = int.Parse(jo[nameof(Ircm).ToLowerInvariant()]?.ToString() ?? "0");
                Roll_indicators_is_available = float.Parse(jo[nameof(Roll_indicators_is_available).ToLowerInvariant()]?.ToString() ?? "0") == 1;
                First_stage_ammo = int.Parse(jo[nameof(First_stage_ammo).ToLowerInvariant()]?.ToString() ?? "0");
                Crew_total = int.Parse(jo[nameof(Crew_total).ToLowerInvariant()]?.ToString() ?? "0");
                Crew_current = int.Parse(jo[nameof(Crew_current).ToLowerInvariant()]?.ToString() ?? "0");
                Crew_distance = double.Parse(jo[nameof(Crew_distance).ToLowerInvariant()]?.ToString() ?? "0");
                Gunner_state = int.Parse(jo[nameof(Gunner_state).ToLowerInvariant()]?.ToString() ?? "0");
                Driver_state = int.Parse(jo[nameof(Driver_state).ToLowerInvariant()]?.ToString() ?? "0");
            }
        }
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool Valid { get; private set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string Army { get; private set; }
        /// <summary>
        /// 载具具体类型
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// 是否自动修理
        /// </summary>
        public bool Is_repairing_auto { get; private set; }
        /// <summary>
        /// 变速器损坏
        /// </summary>
        public bool Transmission_broken { get; private set; }
        /// <summary>
        /// 是否正在维修
        /// </summary>
        public bool Is_repairing { get; private set; }
        /// <summary>
        /// 炮闩损坏
        /// </summary>
        public bool Breech_damaged { get; private set; }
        /// <summary>
        /// 炮管损坏
        /// </summary>
        public bool Barrel_dead { get; private set; }
        /// <summary>
        /// 引擎损坏
        /// </summary>
        public bool Engine_broken { get; private set; }
        /// <summary>
        /// 引擎失效
        /// </summary>
        public bool Engine_dead { get; private set; }
        /// <summary>
        /// 高低机失效
        /// </summary>
        public bool V_drive_broken { get; private set; }
        /// <summary>
        /// 方向机失效
        /// </summary>
        public bool H_drive_dead { get; private set; }
        /// <summary>
        /// 维修时间 (秒)
        /// </summary>
        public float Repair_time { get; private set; }
        /// <summary>
        /// 炮手替换时间 (秒)
        /// </summary>
        public float Gunner_time_to_take_place { get; private set; }
        /// <summary>
        /// 驾驶员替换时间 (秒)
        /// </summary>
        public float Driver_time_to_take_place { get; private set; }
        /// <summary>
        /// 失火
        /// </summary>
        public bool Burns { get; private set; }
        /// <summary>
        /// 自动修理
        /// </summary>
        public bool Track_broken { get; private set; }
        /// <summary>
        /// 稳定器
        /// </summary>
        public bool Stabilizer { get; private set; }
        /// <summary>
        /// 当前挡位
        /// </summary>
        public int Gear { get; private set; }
        /// <summary>
        /// 空挡位置
        /// </summary>
        public int Gear_neutral { get; private set; }
        /// <summary>
        /// 速度 (根据用户设置)
        /// </summary>
        public float Speed { get; private set; }
        /// <summary>
        /// 速度超限警告 (速度大于可发射导弹速度)
        /// </summary>
        public bool Has_speed_warning { get; private set; }
        /// <summary>
        /// 引擎转速
        /// </summary>
        public float Rpm { get; private set; }
        /// <summary>
        /// 辅助驾驶
        /// </summary>
        public bool Driving_direction_mode { get; private set; }
        /// <summary>
        /// 自动驾驶 0-3自动驾驶级别
        /// </summary>
        public int Cruise_control { get; private set; }
        /// <summary>
        /// 激光指示器 (LWS) -1没有 2损坏 1警告
        /// </summary>
        public int Lws { get; private set; }
        /// <summary>
        /// 红外对抗系统 (IRCM) -1没有 2损坏 3关闭 1开启
        /// </summary>
        public int Ircm { get; private set; }
        /// <summary>
        /// 横滚指示器
        /// </summary>
        public bool Roll_indicators_is_available { get; private set; }
        /// <summary>
        /// 待发弹药架上的弹药数量
        /// </summary>
        public int First_stage_ammo { get; private set; }
        /// <summary>
        /// 成员组数量
        /// </summary>
        public int Crew_total { get; private set; }
        /// <summary>
        /// 当前成员组数量
        /// </summary>
        public int Crew_current { get; private set; }
        /// <summary>
        /// 成员平均距离
        /// </summary>
        public double Crew_distance { get; private set; }
        /// <summary>
        /// 炮手状态 2正在替换 1无人替换 0正常
        /// </summary>
        public int Gunner_state { get; private set; }
        /// <summary>
        /// 驾驶员状态 2正在替换 1无人替换 0正常
        /// </summary>
        public int Driver_state { get; private set; }
        /// <summary> 
        /// 俯仰杆位置（Elevator），控制飞机上下倾斜
        /// </summary>
        public float Stick_Elevator { get; set; }
        /// <summary> 
        /// 机翼副翼（Ailerons），控制飞机左右翻滚
        /// </summary>
        public float Stick_Ailerons { get; set; }
        /// <summary> 
        /// 垂直速度（单位未知，可能是 m/s）
        /// </summary>
        public float Vario { get; set; }
        /// <summary> 
        /// 高度（单位未知，可能是英尺） 
        /// </summary>
        public float AltitudeHour { get; set; }
        /// <summary> 
        /// 高度（单位未知，可能是英尺） 
        /// </summary>
        public float AltitudeMin { get; set; }
        /// <summary> 
        /// 高度（单位未知，可能是 10,000 英尺） 
        /// </summary>
        public float Altitude10k { get; set; }
        /// <summary> 
        /// 人工水平仪（滚转角度，单位：度） 
        /// </summary>
        public float Aviahorizon_Roll { get; set; }
        /// <summary> 
        /// 人工水平仪（俯仰角度，单位：度） 
        /// </summary>
        public float Aviahorizon_Pitch { get; set; }
        /// <summary> 
        /// 指南针方向（航向角，单位：度） 
        /// </summary>
        public float Compass { get; set; }
        /// <summary> 
        /// 仪表时钟（小时） 
        /// </summary>
        public float Clock_Hour { get; set; }
        /// <summary> 
        /// 仪表时钟（分钟）
        /// </summary>
        public float Clock_Min { get; set; }
        /// <summary> 
        /// 仪表时钟（秒） 
        /// </summary>
        public float Clock_Sec { get; set; }
        /// <summary> 
        /// 每分钟转速（最低 RPM）
        /// </summary>
        public float Rpm_Min { get; set; }
        /// <summary> 
        /// 每小时转速（RPM） 
        /// </summary>
        public float Rpm_Hour { get; set; }
        /// <summary> 
        /// 机油压力（单位未知，可能是 psi） 
        /// </summary>
        public float Oil_Pressure { get; set; }
        /// <summary> 
        /// 冷却液温度（单位未知，可能是摄氏度）
        /// </summary>
        public float Water_Temperature { get; set; }
        /// <summary> 
        /// 剩余燃油（单位未知，可能是 kg 或 gallon） 
        /// </summary>
        public float Fuel { get; set; }
        /// <summary> 
        /// 燃油消耗率（单位未知，可能是 kg/min）
        /// </summary>
        public float Fuel_Consume { get; set; }
        /// <summary> 
        /// 空气刹车杆（0 = 关闭，1 = 开启）
        /// </summary>
        public bool Airbrake_Lever { get; set; }
        /// <summary> 
        /// 起落架状态（0 = 收起，1 = 放下）
        /// </summary>
        public bool Gears { get; set; }
        /// <summary> 
        /// 襟翼状态（0 = 收起，1 = 放下） 
        /// </summary>
        public bool Flaps { get; set; }
        /// <summary> 
        /// 油门开度（0-1 之间）
        /// </summary>
        public float Throttle { get; set; }
        /// <summary> 
        /// 马赫数（Mach） 
        /// </summary>
        public float Mach { get; set; }
        /// <summary>
        /// G 力测量（重力加速度） 
        /// </summary>
        public float G_Meter { get; set; }
        /// <summary> 
        /// 迎角（AOA，单位：度） 
        /// </summary>
        public float Aoa { get; set; }
        /// <summary> 
        /// 脚踏板数据（可能是方向舵控制）
        /// </summary>
        public float[]? Pedals { get; private set; }
        /// <summary> 
        /// 机体罩（Blisters），可能用于显示舱盖或其他结构部件的状态 
        /// </summary>
        public int[]? Blisters { get; private set; }
        /// <summary> 
        /// 起落架指示灯（0 = 关闭，1 = 亮）
        /// </summary>
        public int[]? GearLamps { get; private set; }

        public override string ToString()
        {
            if (Army.ToLowerInvariant() == "air")
            {
                return $"空战模式 | {Type.Replace("_", " ").ToUpperInvariant()} | 飞机性能数据\n" +
                    //$"当前时间 {(int)Clock_Hour}:{Clock_Min}:{Clock_Sec}\n" +
                    //$"-------舵面-------\n" +
                    //$"自动配平 [{string.Join(",", Pedals)}]\n" +
                    //$"升降舵 [{Aviahorizon_Pitch:F3}] / {Stick_Elevator}\n" +
                    //$"副翼面 [{Aviahorizon_Roll:F3}] / {Stick_Ailerons}\n" +
                    //$"油门度 {Throttle*100}\n" +
                    //$"-------仪表-------\n" +
                    //$"磁航向: {Compass} \n" +
                    //$"RPM: {Rpm} [{Rpm_Min}/{Rpm_Hour}]\n" +
                    //$"速度: {Speed:F3} (Mach:{Mach:F2}) | 迎角 {Aoa:F2}° \n" +
                    //$"垂直速率 {Vario} | G力: {G_Meter:F2}\n" +
                    //$"{(Airbrake_Lever?"减速板到位": "减速板收起")} | {(Gears ? "起落架到位" : "起落架收起")} | {(Flaps ? "襟翼到位" : "襟翼收起")} \n" +
                    //$"油量: ({Fuel:F3},{Fuel_Consume:F3}) | 水温:{Water_Temperature:F3} | \n" +
                    //$"舱盖: [{string.Join(",", Blisters)}]\n" +
                    "";

            }
            else if (Army.ToLowerInvariant() == "tank")
            {
                return $"地面模式 | {Type.Replace(Type.Split("_")[0], "").Replace("_", " ").ToUpperInvariant()} | 坦克性能数据\n" +
                    $"---------损坏管制---------\n" +
                    $"{(Breech_damaged ? "炮闩损坏\n" : "")}{(Barrel_dead ? "炮闩失效\n" : "")}" +
                    $"{(Engine_broken ? "引擎损坏\n" : "")}{(Engine_dead ? "引擎失效\n" : "")}" +
                    $"{(V_drive_broken ? "高低机损坏\n" : "")}{(H_drive_dead ? "方向机损坏\n" : "")}" +
                    $"{(Transmission_broken ? "变速器损坏\n" : "")}{(Track_broken ? "履带损坏\n" : "")}" +
                    $"{(Is_repairing_auto ? $"正在自动维修,剩余{Repair_time}秒" : "")}{(Is_repairing ? $"正在维修,剩余{Repair_time}秒" : "")}\n" +
                    $"{(Gunner_state == 2 ? $"炮手{Gunner_time_to_take_place}秒后替换完成" : $"{(Gunner_state == 1 ? "炮手无人替换" : "")}")}\n" +
                    $"{(Driver_state == 2 ? $"驾驶员{Driver_time_to_take_place}秒后替换完成" : $"{(Driver_state == 1 ? "驾驶员无人替换" : "")}")}\n" +
                    $"---------基础属性----------\n" +
                    $"{(Driving_direction_mode ? "辅助驾驶已开启\n" : "")}" +
                    $"{(Roll_indicators_is_available ? "横滚指示器已开启\n" : "")}" +
                    $"{(Gear - Gear_neutral == 0 ? " 空挡 " : $"{(Cruise_control > 0 ? $"自动驾驶[{Cruise_control}]级::" : "")}挡位[{Gear - Gear_neutral}]")} [速度: {Speed}{(Has_speed_warning ? "超过导弹射击速度" : "")}/转速: {Rpm}]\n" +
                    $"{(Stabilizer ? "稳定器已启动" : "稳定器未启动")} | " +
                    $"{(Lws == -1 ? "无激光指示器" : $"{(Lws == 2 ? "激光指示器已损坏" : $"{(Lws == 1 ? "!正在被激光照射!" : "激光指示器正待命")}")}")} | " +
                    $"{(Ircm == -1 ? "无红外对抗系统" : $"{(Ircm == 2 ? "红外对抗系统损坏" : $"{(Ircm == 3 ? "红外对抗系统关闭" : $"红外对抗系统开启")}")}")}\n" +
                    $"成员: {Crew_current}/{Crew_total}[{Crew_distance}]\n" +
                    $"{(First_stage_ammo > 0 ? $"待发弹药架{First_stage_ammo}发" : "")}\n" +
                    $"";
            }
            else
            {
                return $"{(Valid ? "已验证(不支持的载具类型)" : "未验证(不支持的载具类型)")}";
            }
        }
    }
}
