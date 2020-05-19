namespace DVLA.VEHICLE.ENQUIRY.SDK.Models
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string ResponseMessage { get; set; }
        public string ReasonPhrase { get; set; }
        public VehicleDetails VehicleDetails { get; set; }
        public string DvlaReferenceCode { get; set; }
    }
}