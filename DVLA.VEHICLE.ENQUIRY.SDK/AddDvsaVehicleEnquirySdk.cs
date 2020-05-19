using DVLA.VEHICLE.ENQUIRY.SDK.Interfaces;
using DVLA.VEHICLE.ENQUIRY.SDK.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DVLA.VEHICLE.ENQUIRY.SDK
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddDvsaVehicleEnquirySdk(this IServiceCollection services)
        {
            services.AddHttpClient<IProcessApiResponse, ProcessApiResponse>();
            return services;
        }
    }
}