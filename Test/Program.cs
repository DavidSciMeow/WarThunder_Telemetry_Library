namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(new WarthunderTelemetry.IndicatorsInfo().ToString());
                Task.Delay(500).GetAwaiter().GetResult();
            }
        }
    }

}
