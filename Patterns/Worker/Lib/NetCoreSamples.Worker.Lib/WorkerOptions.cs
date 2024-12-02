namespace NetCoreSamples.Worker.Lib
{
    /// <summary>
    /// Base options for a Worker
    /// </summary>
    public class WorkerOptions
    {
        /// <summary>
        /// The Worker assembly name
        /// </summary>
        public required string Assembly { get; set; }

        /// <summary>
        /// The Worker type name to run
        /// </summary>
        public required string Name { get; set; }
    }
}
