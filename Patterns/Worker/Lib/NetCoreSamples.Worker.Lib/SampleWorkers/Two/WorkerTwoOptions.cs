namespace NetCoreSamples.Worker.Lib.SampleWorkers.Two
{
    public class WorkerTwoOptions
    {
        public required int DelayMiliseconds { get; set; }
        public required string TextToLog { get; set; }
    }
}
