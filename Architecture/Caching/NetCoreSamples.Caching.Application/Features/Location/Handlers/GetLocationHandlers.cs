using MediatR;
using NetCoreSamples.Caching.Application.DTO;
using NetCoreSamples.Caching.Application.Features.Location.Requests;
using NetCoreSamples.Caching.Application.Interfaces;
using NetCoreSamples.Caching.Lib;
using NetCoreSamples.Caching.Application.Enumerations;

namespace NetCoreSamples.Caching.Application.Features.Location.Handlers
{
    /// <summary>
    /// The handlers for the GetLocation requests.
    /// </summary>
    public class GetLocationHandlers
    {
        /// <summary>
        /// Handles the request to get all countries and their states.
        /// </summary>
        public class AllCountryStates :
            IRequestHandler<GetLocation.AllCountryStates, IEnumerable<LocationDTO>>
        {
            /// <summary>
            /// The cache manager.
            /// </summary>
            readonly RedisCacheManager cache;

            /// <summary>
            /// The country data access object.
            /// </summary>
            readonly ICountryDAO countryDAO;

            public AllCountryStates(RedisCacheManager cache, ICountryDAO countryDao)
            {
                this.cache = cache;
                this.countryDAO = countryDao;
            }

            /// <summary>
            /// Handles the request to get all countries and their states.
            /// The data is first retrieved from the cache, if it exists. If not, it is retrieved from the database and then stored in the cache.
            /// </summary>
            /// <param name="request">The <see cref="GetLocation.AllCountryStates"/> request.</param>
            /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
            /// <returns>A list of <see cref="LocationDTO"/></returns>
            public async Task<IEnumerable<LocationDTO>> Handle(GetLocation.AllCountryStates request, CancellationToken cancellationToken)
            {
                IEnumerable<LocationDTO> result = null!;
                DataSource dataSource = DataSource.Cache;

                var cacheData = await cache
                    .GetSerializableDataAsync<IEnumerable<LocationDTO>>("CountryStates");

                if (cacheData != null)
                {
                    result = cacheData;
                }
                else
                {
                    var countries = await countryDAO.GetAllWithStatesAsync();

                    dataSource = DataSource.Database;
                    result = countries.Select(c =>
                        new LocationDTO
                        {
                            Country = c.Name,
                            StateProvinces = c.StateProvinces.Select(s => s.Name)
                        });

                    await cache.SetSerializableDataAsync("CountryStates", result);
                }

                foreach (var location in result)
                {
                    location.DataSource = dataSource;
                }

                return result;
            }
        }
    }
}
