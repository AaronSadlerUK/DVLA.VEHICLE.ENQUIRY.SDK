using DVLA.VEHICLE.ENQUIRY.SDK.Interfaces;
using DVLA.VEHICLE.ENQUIRY.SDK.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DVLA.VEHICLE.ENQUIRY.SDK
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddDvlaVehicleEnquirySdk(this IServiceCollection services)
        {
            services.AddHttpClient<IProcessApiResponse, ProcessApiResponse>();
            services.AddScoped<IVehicleDetailsService, VehicleDetailsService>();
            return services;
        }
    }
}