using MediatR;
using NetCoreSamples.Caching.Application.DTO;
using NetCoreSamples.Caching.Application.Features.Location.Requests;
using NetCoreSamples.Caching.Application.Interfaces;
using NetCoreSamples.Caching.Lib;
using NetCoreSamples.Caching.Application.Enumerations;

namespace NetCoreSamples.Caching.Application.Features.Location.Handlers
{
    public class GetLocationHandlers
    {
        public class AllCountryStates :
            IRequestHandler<GetLocation.AllCountryStates, IEnumerable<LocationDTO>>
        {
            private RedisCacheManager Cache { get; }
            private ICountryDAO CountryDAO { get; }

            public AllCountryStates(RedisCacheManager cache, ICountryDAO countryDao)
            {
                this.Cache = cache;
                this.CountryDAO = countryDao;
            }

            /// <summary>
            /// Handles the request to get all countries and their states.
            /// The data is first retrieved from the cache, if it exists. If not, it is retrieved from the database and then stored in the cache.
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<IEnumerable<LocationDTO>> Handle(GetLocation.AllCountryStates request, CancellationToken cancellationToken)
            {
                IEnumerable<LocationDTO> result = null!;
                DataSource dataSource = DataSource.Cache;

                var cacheData = await this.Cache
                    .GetSerializableDataAsync<IEnumerable<LocationDTO>>("CountryStates");

                if (cacheData != null)
                {
                    result = cacheData;
                }
                else
                {
                    var countries = await this.CountryDAO.GetAllWithStatesAsync();

                    dataSource = DataSource.Database;
                    result = countries.Select(c =>
                        new LocationDTO
                        {
                            Country = c.Name,
                            StateProvinces = c.StateProvinces.Select(s => s.Name)
                        });

                    await this.Cache.SetSerializableDataAsync("CountryStates", result);
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
