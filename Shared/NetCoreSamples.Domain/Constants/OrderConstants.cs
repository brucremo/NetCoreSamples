namespace NetCoreSamples.Domain.Constants
{
    /// <summary>
    /// Constants related to order processing and management.
    /// </summary>
    public static class OrderConstants
    {
        /// <summary>
        /// Default status for newly created orders.
        /// </summary>
        public const string DefaultOrderStatus = "Pending";

        /// <summary>
        /// Status indicating that an order has been shipped.
        /// </summary>
        public const string ShippedStatus = "Shipped";

        /// <summary>
        /// Status indicating that an order has been delivered.
        /// </summary>
        public const string DeliveredStatus = "Delivered";

        /// <summary>
        /// Status indicating that an order is in transit.
        /// </summary>
        public const string InTransitStatus = "In Transit";
    }
}
