namespace NetCoreSamples.Worker.Lib
{
    /// <summary>
    /// Base options for a Worker
    /// </summary>
    public class WorkerOptions
    {
        /// <summary>
        /// The Worker type name to run
        /// </summary>
        public required string Name { get; set; }
    }
}
