namespace NetCoreSamples.Domain.Models
{
    /// <summary>
    /// Model representing an order in the system.
    /// </summary>
    public record class OrderModel
    {
        /// <summary>
        /// Unique identifier for the order.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Identifier for the user who placed the order.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Order tracking number.
        /// </summary>
        public string? TrackingNumber { get; set; }

        /// <summary>
        /// Status of the order, e.g., "Pending", "Shipped", "Delivered".
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        /// Date and time when the order was created.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Date and time when the order was shipped, if applicable.
        /// </summary>
        public DateTime? ShippedOn { get; set; }

        /// <summary>
        /// Date and time when the order was delivered, if applicable.
        /// </summary>
        public DateTime? DeliveredOn { get; set; }

        /// <summary>
        /// Date and time when the order was last updated.
        /// </summary>
        public DateTime LastUpdatedOn { get; set; }
    }
}
