using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;

namespace App.Controllers
{
    public class DataController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiUrl = "http://localhost:5047";
        public DataController(IHttpClientFactory httpClientFactory) 
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<JArray> GetObjects(string controller, string? version = null)
        {
            var client = _httpClientFactory.CreateClient();
            HttpResponseMessage response = null;

            if (!string.IsNullOrEmpty(version))
            {
                response = await client.GetAsync($"{_apiUrl}/api/v{version}/{controller}");
            }
            else
            {
                response = await client.GetAsync($"{_apiUrl}/api/{controller}");
            }

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var copies = JArray.Parse(content);
                return copies;
            }

            return new JArray();
        }

        public async Task<JObject> GetObject(string controller, string? version = null, string? string_id = null, int? int_id = null)
        {
            var client = _httpClientFactory.CreateClient();
            HttpResponseMessage response = null;

            if (!string.IsNullOrEmpty(string_id) && !string.IsNullOrEmpty(version))
            {
                response = await client.GetAsync($"{_apiUrl}/api/v{version}/{controller}/{string_id}");
            }
            else if (int_id.HasValue && !string.IsNullOrEmpty(version)) 
            { 
                response = await client.GetAsync($"{_apiUrl}/api/v{version}/{controller}/{int_id}");
            }
            else
            {
                response = await client.GetAsync($"{_apiUrl}/api/{controller}/{int_id}");
            }

            if (response != null && response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var copy = JObject.Parse(content);
                return copy;
            }

            return new JObject();
        }

        public async Task<IActionResult> BookingStatusesV1()
        {
            var statuses = await GetObjects("BookingStatus", version: "1.0");

            if (statuses != null && statuses.Count > 0) 
            {
                return View(statuses);
            }

            return View(new JArray());
        }

        public async Task<IActionResult> BookingStatusesV2()
        {
            var statuses = await GetObjects("BookingStatus", version: "2.0");

            if (statuses != null && statuses.Count > 0)
            {
                return View(statuses);
            }

            return View(new JArray());
        }

        public async Task<IActionResult> BookingStatusDetailsV1(string id)
        {
            var bookingStatus = await GetObject("BookingStatus", string_id: id, version: "1.0");

            if (bookingStatus != null  && bookingStatus.HasValues) 
            { 
                return View(bookingStatus); 
            }

            return NotFound();
        }

        public async Task<IActionResult> BookingStatusDetailsV2(string id)
        {
            var bookingStatus = await GetObject("BookingStatus", string_id: id, version: "2.0");

            if (bookingStatus != null && bookingStatus.HasValues)
            {
                return View(bookingStatus);
            }

            return NotFound();
        }

        public async Task<IActionResult> Vehicles()
        {
            var vehicles = await GetObjects("Vehicle");

            if (vehicles != null && vehicles.Count > 0)
            {
                return View(vehicles);
            }

            return View(new JArray());
        }

        public async Task<IActionResult> VehicleDetails(string regNumber)
        {
            var vehicle = await GetObject("Vehicle", string_id: regNumber);

            if (vehicle != null && vehicle.HasValues)
            {
                return View(vehicle);
            }

            return NotFound();
        }

        public async Task<IActionResult> Bookings()
        {
            var bookings = await GetObjects("Booking");

            if (bookings != null && bookings.Count > 0)
            {
                return View(bookings);
            }

            return View(new JArray());
        }

        public async Task<IActionResult> BookingDetails(int bookingId)
        {
            var booking = await GetObject("Booking", int_id: bookingId);

            if (booking != null && booking.HasValues)
            {
                return View(booking);
            }

            return NotFound();
        }


        public async Task<IActionResult> SearchBookings(DateTime? dateFrom, DateTime? dateTo, List<string> statusCodes, string? startVehicleReg = null, string? endVehicleReg = null)
        {
            var client = _httpClientFactory.CreateClient();

            var queryParam = new List<string>();

            if (dateFrom.HasValue) queryParam.Add($"dateFrom={dateFrom.Value:O}");
            if (dateTo.HasValue) queryParam.Add($"dateTo={dateTo.Value:O}");
            if (statusCodes != null && statusCodes.Any())
            {
                foreach (var code in statusCodes)
                {
                    queryParam.Add($"statusCodes={code}");
                }
            }
            if (!String.IsNullOrEmpty(startVehicleReg)) queryParam.Add($"startVehicleReg={startVehicleReg}");
            if (!String.IsNullOrEmpty(endVehicleReg)) queryParam.Add($"endVehicleReg={endVehicleReg}");

            var query = string.Join("&", queryParam);
            var response = await client.GetAsync($"{_apiUrl}/api/Booking/search?{query}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var searchResults = JArray.Parse(content);
                return View(searchResults);
            }

            return View(new JArray());
        }
    }
}
