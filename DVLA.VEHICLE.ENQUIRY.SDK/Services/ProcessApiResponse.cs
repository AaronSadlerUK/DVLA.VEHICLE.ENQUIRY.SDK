using System.Net.Http;
using DVLA.VEHICLE.ENQUIRY.SDK.Interfaces;
using DVLA.VEHICLE.ENQUIRY.SDK.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
    }
}
