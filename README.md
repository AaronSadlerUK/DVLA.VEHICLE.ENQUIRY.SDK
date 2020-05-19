# DVLA.VEHICLE.ENQUIRY.SDK
![nuget badge](https://img.shields.io/nuget/v/AjsWebDesign.DVSA.MOT.SDK)

A simple SDK for access to the [DVLA Vehicle Enquiry API](https://developer-portal.driver-vehicle-licensing.api.gov.uk/availableapis.html)

### Installation

To be able to use this SDK, you will require an Api Key, which you can apply for from the [DVLA](https://developer-portal.driver-vehicle-licensing.api.gov.uk/apis/vehicle-enquiry-service/Register-For-VES-API.html).

DVSA.MOT.SDK is available from [NuGet](https://www.nuget.org/packages/AjsWebDesign.DVLA.VEHICLE.ENQUIRY.SDK) or as a manual download directly from GitHub.

## Usage

You will need to add the following key to your `appsettings.json` or `secrets.json`:

      {
        "DvlaVehicleEnquiryApi": {
          "DVLAApiKey": "YOUR KEY HERE"
        }
      }
     
Then you will then need to register the apikey and SDK in your `Startup.cs` file

      services.Configure<ApiKey>(Configuration.GetSection("DvlaVehicleEnquiryApi"));
      services.AddDvlaVehicleEnquirySdk();
      services.AddOptions();
      services.AddLogging();


### Documentation

Documentation and examples on how the parameters are used to access the api can be found on the dvla vehicle enquiry api website:
[https://developer-portal.driver-vehicle-licensing.api.gov.uk/apis/vehicle-enquiry-service/v1.1.0-vehicle-enquiry-service.html#vehicle-enquiry-api](https://developer-portal.driver-vehicle-licensing.api.gov.uk/apis/vehicle-enquiry-service/v1.1.0-vehicle-enquiry-service.html#vehicle-enquiry-api)

### Contribution guidelines

To raise a new bug, create an issue on the GitHub repository. To fix a bug or add new features, fork the repository and send a pull request with your changes. Feel free to add ideas to the repository's issues list if you would to discuss anything related to the package.

## License

Copyright &copy; 2020 [Aaron Sadler](https://aaronsadler.uk/).

Licensed under the [MIT License](https://opensource.org/licenses/MIT).

The DVSA API is Licensed under the [Open Government Licence v3.0](https://www.nationalarchives.gov.uk/doc/open-government-licence/version/3/)
