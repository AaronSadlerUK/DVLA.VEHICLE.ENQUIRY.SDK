using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DVLA.VEHICLE.ENQUIRY.SDK.Interfaces;
using DVLA.VEHICLE.ENQUIRY.SDK.Models;
using Microsoft.Extensions.Logging;

namespace DVLA.VEHICLE.ENQUIRY.SDK.Services
{
    public class VehicleDetailsService : IVehicleDetailsService
    {
        private readonly IProcessApiResponse _processApiResponse;
        private readonly ILogger<VehicleDetailsService> _logger;

        public VehicleDetailsService(IProcessApiResponse processApiResponse, ILogger<VehicleDetailsService> logger)
        {
            _processApiResponse = processApiResponse;
            _logger = logger;
        }

        /// <summary>
        /// To request the MOT test history for registration
        /// </summary>
        /// <param name="registration">Registration of vehicle</param>
        /// <returns>A single vehicles details and mot history</returns>
        public async Task<ApiResponse> GetVehicleDetails(string registration)
        {
            try
            {
                var apiResponse = new ApiResponse();
                if (!string.IsNullOrEmpty(registration))
                {
                    var parameters = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>(Constants.Parameters.Registration, registration)
                    };
                    return await _processApiResponse.GetData(parameters);
                }

                var responseMessage = Constants.LanguageStrings.SingleVehicleMotHistory.NullRegistrationException;
                _logger.Log(LogLevel.Error, responseMessage);
                apiResponse.ResponseMessage = responseMessage;
                return apiResponse;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, ex.Message);
                return null;
            }
        }
    }
}
