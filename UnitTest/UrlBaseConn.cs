using WarthunderTelemetry.Base;

namespace UnitTest
{
    public class UrlBaseConn
    {

        [Fact] public async Task GetIndicators() => Assert.NotNull(await BaseGet.GetIndicators());
        [Fact] public async Task GetState() => Assert.NotNull(await BaseGet.GetState());
        [Fact] public async Task GetMission() => Assert.NotNull(await BaseGet.GetMission());
        [Fact] public async Task GetHudmsg() => Assert.NotNull(await BaseGet.GetHudmsg());
        [Fact] public async Task GetGamechat() => Assert.NotNull(await BaseGet.GetGamechat());
        [Fact] public async Task GetMapImg() => Assert.NotNull(await BaseGet.GetMapImg());
        [Fact] public async Task GetMapInfo() => Assert.NotNull(await BaseGet.GetMapInfo());
        [Fact] public async Task GetMapObjInfo() => Assert.NotNull(await BaseGet.GetMapObjInfo());
    }
}
