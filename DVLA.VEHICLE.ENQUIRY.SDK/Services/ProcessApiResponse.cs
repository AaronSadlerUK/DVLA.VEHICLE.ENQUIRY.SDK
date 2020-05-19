using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
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
        /// <param name="parameters">Query string parameters</param>
        /// <returns></returns>
        public async Task<ApiResponse> GetData(IEnumerable<KeyValuePair<string, string>> parameters)
        {
            var apiResponse = new ApiResponse();
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(Constants.ApiAcceptHeader));
            _httpClient.DefaultRequestHeaders.Add(Constants.ApiKeyHeader, _apiKey.Value.DVLAApiKey);

            var url = GenerateUrl(parameters);
            var request = await _httpClient.GetAsync(url);
            apiResponse.ReasonPhrase = request.ReasonPhrase;
            apiResponse.StatusCode = (int)request.StatusCode;

            if (!request.IsSuccessStatusCode)
            {
                var response = await ConvertToErrorObject(request.Content);
                var responseMessage = $"{response.Title} - {response.Detail}";
                _logger.Log(LogLevel.Error, responseMessage);

                apiResponse.ResponseMessage = responseMessage;
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

                var motTestResponses = JsonConvert.DeserializeObject<VehicleDetails>(json);
                return motTestResponses;
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

                var motTestResponses = JsonConvert.DeserializeObject<ApiErrorResponse>(json);
                return motTestResponses;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Generate a url with parameters for the HttpClient request
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private static string GenerateUrl(IEnumerable<KeyValuePair<string, string>> parameters)
        {
            var baseURl = $"{Constants.ApiRootUrl}{Constants.ApiPath}";
            var uriBuilder = new UriBuilder(baseURl);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            foreach (var param in parameters)
            {
                query.Add(param.Key, param.Value);
            }
            uriBuilder.Query = query.ToString();
            baseURl = uriBuilder.ToString();
            return baseURl;
        }
    }
}
