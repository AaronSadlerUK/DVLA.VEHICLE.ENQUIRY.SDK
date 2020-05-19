using System.Threading.Tasks;
using DVLA.VEHICLE.ENQUIRY.SDK.Models;

namespace DVLA.VEHICLE.ENQUIRY.SDK.Interfaces
{
    public interface IVehicleDetailsService
    {
        Task<ApiResponse> GetVehicleDetails(string registration);
    }
}
