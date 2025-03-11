using System;

namespace WarthunderTelemetry
{

    public class StateInfo
    {
        public bool valid { get; set; }
        public int aileron { get; set; }
        public int elevator { get; set; }
        public int rudder { get; set; }
        public int flaps { get; set; }
        public int gear { get; set; }
        public int airbrake { get; set; }
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
        public int throttle1 { get; set; }
        public float power1hp { get; set; }
        public int RPM1 { get; set; }
        public float manifoldpressure1atm { get; set; }
        public int oiltemp1C { get; set; }
        public int thrust1kgs { get; set; }
        public int efficiency1 { get; set; }
        public int throttle2 { get; set; }
        public float power2hp { get; set; }
        public int RPM2 { get; set; }
        public float manifoldpressure2atm { get; set; }
        public int oiltemp2C { get; set; }
        public int thrust2kgs { get; set; }
        public int efficiency2 { get; set; }
    }

    public class MissionInfo
    {
        public class Objective
        {
            public bool primary { get; set; }
            public string status { get; set; }
            public string text { get; set; }
        }
        public Objective[] objectives { get; set; }
        public string status { get; set; }
    }
    public class DamageInfo
    {
        public object[] events { get; set; }
        public Damage[] damage { get; set; }
        public class Damage
        {
            public int id { get; set; }
            public string msg { get; set; }
            public string sender { get; set; }
            public bool enemy { get; set; }
            public string mode { get; set; }
            public int time { get; set; }
        }
    }
    public class GamechatInfo
    {
        public int id { get; set; }
        public string msg { get; set; }
        public string sender { get; set; }
        public bool enemy { get; set; }
        public string mode { get; set; }
    }
    public class MapObjInfo
    {
        public string type { get; set; }
        public string color { get; set; }
        public int[] color_rgb { get; set; }
        public int blink { get; set; }
        public string icon { get; set; }
        public string icon_bg { get; set; }
        public float sx { get; set; }
        public float sy { get; set; }
        public float ex { get; set; }
        public float ey { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public float dx { get; set; }
        public float dy { get; set; }
    }

}
