using WarthunderTelemetry;
using WarthunderTelemetry.Base;
using WarthunderTelemetry.Data;

class Program
{
    private static CancellationTokenSource updateCts = new CancellationTokenSource();
    private static CancellationTokenSource saveCts = new CancellationTokenSource();
    private static int _mapupdate = 0;

    static async Task Main()
    {
        Console.CancelKeyPress += (sender, e) =>
        {
            e.Cancel = true;
            updateCts.Cancel();
            saveCts.Cancel();
        };

        Task updateTask = UpdateData(updateCts.Token);
        Task saveTask = SaveMapData(saveCts.Token);

        await Task.WhenAll(updateTask, saveTask);
    }

    static async Task UpdateData(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            var data = new Army();
            Console.Clear();
            Console.WriteLine($"mapupdate:{_mapupdate}\n{data}");
            await Task.Delay(500, token);
        }
    }

    static async Task SaveMapData(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            var dt = Get.GetMapImgAsync();
            if (dt != null)
            {
                File.WriteAllBytes("./data.png", dt.Data);
                _mapupdate = _mapupdate < 10 ? _mapupdate + 1 : 0;
            }
            await Task.Delay(500, token);
        }
    }
}
