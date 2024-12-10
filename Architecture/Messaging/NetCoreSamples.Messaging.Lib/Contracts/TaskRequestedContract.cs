namespace NetCoreSamples.Messaging.Lib.Contracts
{
    /// <summary>
    /// The task requested contract
    /// </summary>
    public class TaskRequestedContract : BaseContract<TaskRequestedContract>
    {
        /// <summary>
        /// The task content
        /// </summary>
        public string? Content { get; set; }
    }
}
