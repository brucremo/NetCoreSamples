namespace NetCoreSamples.Messaging.Lib.Contracts
{
    /// <summary>
    /// Contract representing a task completed
    /// </summary>
    public class TaskCompletedContract : BaseContract<TaskCompletedContract>
    {
        /// <summary>
        /// The contract message
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// The contract success flag
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// The contract title
        /// </summary>
        public string? Title { get; set; }
    }
}
