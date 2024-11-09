namespace NetCoreSamples.Worker.Lib
{
    /// <summary>
    /// Base options for a Worker
    /// </summary>
    public class WorkerBaseOptions
    {
        /// <summary>
        /// The Worker name to run
        /// </summary>
        public required string Worker { get; set; }
    }
}
