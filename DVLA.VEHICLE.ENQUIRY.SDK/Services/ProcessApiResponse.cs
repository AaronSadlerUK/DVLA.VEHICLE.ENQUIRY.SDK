using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using DVLA.VEHICLE.ENQUIRY.SDK.Interfaces;
using DVLA.VEHICLE.ENQUIRY.SDK.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace DVLA.VEHICLE.ENQUIRY.SDK.Services
{
    public class ProcessApiResponse : IProcessApiResponse
    {
        private readonly IOptions<ApiKey> _apiKey;
        private readonly ILogger<ProcessApiResponse> _logger;
        private readonly HttpClient _httpClient;

        public ProcessApiResponse(IOptions<ApiKey> apiKey, ILogger<ProcessApiResponse> logger, HttpClient httpClient)
        {
            _apiKey = apiKey;
            _logger = logger;
            _httpClient = httpClient;
        }

        /// <summary>
        /// Returns an object containing the results from the database
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        public async Task<ApiResponse> GetData(Registration registration)
        {
            var apiResponse = new ApiResponse();
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(Constants.ApiAcceptHeader));
            _httpClient.DefaultRequestHeaders.Add(Constants.ApiKeyHeader, _apiKey.Value.DVLAApiKey);

            var url = Constants.ApiRootUrl + Constants.ApiPath;
            var postRequest = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = JsonContent.Create(registration)
            };
            var request = await _httpClient.SendAsync(postRequest);
            apiResponse.ReasonPhrase = request.ReasonPhrase;
            apiResponse.StatusCode = (int)request.StatusCode;

            if (!request.IsSuccessStatusCode)
            {
                var response = await ConvertToErrorObject(request.Content);
                var sb = new StringBuilder();
                if (!string.IsNullOrEmpty(response.Title))
                {
                    sb.Append(response.Title);
                }

                if (!string.IsNullOrEmpty(response.Title) && !string.IsNullOrEmpty(response.Detail))
                {
                    sb.Append(" - ");
                }

                if (!string.IsNullOrEmpty(response.Detail))
                {
                    sb.Append(response.Detail);
                }

                var responseMessage = sb.ToString();
                _logger.Log(LogLevel.Error, sb.ToString());

                apiResponse.ResponseMessage = responseMessage;
                apiResponse.DvlaReferenceCode = response.Code;
            }

            if (request.IsSuccessStatusCode)
            {
                apiResponse.VehicleDetails = await ConvertToObject(request.Content);
            }
            return apiResponse;
        }

        /// <summary>
        /// Convert the json response to ApiResponse object
        /// </summary>
        /// <param name="httpContent">HttpClient response content</param>
        /// <returns>ApiResponse object</returns>
        private async Task<VehicleDetails> ConvertToObject(HttpContent httpContent)
        {
            try
            {
                if (httpContent == null)
                    return null;

                var json = await httpContent.ReadAsStringAsync();
                if (string.IsNullOrEmpty(json))
                    return null;

                var vehicleDetailsResponse = JsonConvert.DeserializeObject<VehicleDetails>(json);
                return vehicleDetailsResponse;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Convert the json response to ApiErrorResponse object
        /// </summary>
        /// <param name="httpContent">HttpClient response content</param>
        /// <returns>ApiResponse object</returns>
        private async Task<ApiErrorResponse> ConvertToErrorObject(HttpContent httpContent)
        {
            try
            {
                if (httpContent == null)
                    return null;

                var json = await httpContent.ReadAsStringAsync();
                if (string.IsNullOrEmpty(json))
                    return null;

                var vehiclesDetailsErrorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(json);
                return vehiclesDetailsErrorResponse;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, ex.Message);
                return null;
            }
        }
    }
}
