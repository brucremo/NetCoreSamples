using JasperFx.Events.Projections;
using Marten;
using Marten.Events.Aggregation;

namespace NetCoreSamples.Domain.Projections.Base
{
    /// <summary>
    /// Base class for single stream projections that represent the current state of a model based on events.
    /// </summary>
    /// <typeparam name="TProjection">The projection type.</typeparam>
    /// <typeparam name="TModel">The model type.</typeparam>
    /// <typeparam name="TId">The model ID's type.</typeparam>
    public class SingleStreamProjectionBase<TProjection, TModel, TId> : SingleStreamProjection<TModel, TId>
        where TModel : notnull 
        where TId : notnull
        where TProjection : SingleStreamProjection<TModel, TId>, new()
    {
        /// <summary>
        /// Registers the projection and model type with the Marten's store options.
        /// </summary>
        /// <param name="options">The <see cref="StoreOptions"/>.</param>
        /// <param name="projectionLifecycle">The <see cref="ProjectionLifecycle"/>. Defaults to Inline.</param>
        public static void RegisterOptions(StoreOptions options, ProjectionLifecycle projectionLifecycle = ProjectionLifecycle.Inline)
        {
            options.RegisterDocumentType<TModel>();
            options.Projections.Add(new TProjection(), projectionLifecycle);
        }
    }
}
