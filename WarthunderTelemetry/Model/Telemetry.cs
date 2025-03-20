using System;
using System.Drawing;

namespace WarthunderTelemetry.Model
{
    public class MissionInfo
    {
        public class Objective
        {
            public bool Primary { get; set; }
            public string? Status { get; set; }
            public string? Text { get; set; }
        }
        public Objective[]? Objectives { get; set; }
        public string? Status { get; set; }
    }
    public class DamageInfo
    {
        public object[]? Events { get; set; }
        public DamageInf[]? Damage { get; set; }
        public class DamageInf
        {
            public int Id { get; set; }
            public string? Msg { get; set; }
            public string? Sender { get; set; }
            public bool Enemy { get; set; }
            public string? Mode { get; set; }
            public int Time { get; set; }
        }
    }
    public class GamechatInfo
    {
        public int Id { get; set; }
        public string? Msg { get; set; }
        public string? Sender { get; set; }
        public bool Enemy { get; set; }
        public string? Mode { get; set; }
    }

}
