using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace TimeZoneApp.Controllers
{
    [ApiController]
    [Route("api/")]
    public class TimeZoneAppController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;


        public TimeZoneAppController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        [Route("datetime")]
        public async Task<IActionResult> GetTime()
        {
            var timezone = _configuration.GetValue<string>("TimeZoneWebAPI:timezone");
            var URL = $"timezone/{timezone}";

            var httpClient = _httpClientFactory.CreateClient("TimeZoneAPI");
            var response = await httpClient.GetAsync(URL);
            var responseString = await response.Content.ReadAsStringAsync();
            var deserializedTimeZoneData = JsonConvert.DeserializeObject<TimeZoneData>(responseString);
            if (deserializedTimeZoneData != null)
                return Ok(deserializedTimeZoneData.Datetime);
            else
                return NotFound();

        }


        [HttpGet]
        [Route("timezone/{area}")]
        public async Task<List<string>> GetAllTimeZonesInArea(string area)
        {
            var URL = $"timezone/{area}";
            var httpClient = _httpClientFactory.CreateClient("TimeZoneAPI");
            var response = await httpClient.GetAsync(URL);
            var responseString = await response.Content.ReadAsStringAsync();
            var deserializedList = JsonConvert.DeserializeObject<List<string>>(responseString);

            return deserializedList;
        }

        [HttpGet]
        [Route("timezone/{location}/{region}")]
        public async Task<IActionResult> GetAllTimeZonesInArea(string location, string region)
        {
            var timezone = _configuration.GetValue<string>("TimeZoneWebAPI:timezone");
            var URL = $"timezone/{location}/{region}";
            var httpClient = _httpClientFactory.CreateClient("TimeZoneAPI");
            var response = await httpClient.GetAsync(URL);
            var responseString = await response.Content.ReadAsStringAsync();
            var deserializedList = JsonConvert.DeserializeObject<TimeZoneData>(responseString);

            if (deserializedList != null)
                return Ok(deserializedList.Datetime);
            else
                return NotFound();
        }
    }
}