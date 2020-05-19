using System.Collections.Generic;
using System.Threading.Tasks;
using DVLA.VEHICLE.ENQUIRY.SDK.Models;

namespace DVLA.VEHICLE.ENQUIRY.SDK.Interfaces
{
    public interface IProcessApiResponse
    {
        /// <summary>
        /// Returns an object containing the results from the database
        /// </summary>
        /// <param name="parameters">Query string parameters</param>
        /// <returns></returns>
        Task<ApiResponse> GetData(IEnumerable<KeyValuePair<string, string>> parameters);
    }
}
