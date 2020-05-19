namespace DVLA.VEHICLE.ENQUIRY.SDK
{
    internal static class Constants
    {
        public static string ApiRootUrl => "https://driver-vehicle-licensing.api.gov.uk/";
        public static string ApiPath => "vehicle-enquiry/v1/vehicles";
        public static string ApiAcceptHeader => "application/json";
        public static string ApiKeyHeader => "x-api-key";

        public class Parameters
        {
            public static string Registration => "registrationNumber";
        }

        public class LanguageStrings
        {
            public static string NullRegistrationException => "The registration number is missing";
        }
    }
}