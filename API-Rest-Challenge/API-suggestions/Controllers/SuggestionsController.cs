using BusinessLayer;
using DataModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_suggestions.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SuggestionsController : Controller
    {
        [HttpGet]
        public ActionResult<Suggestions> Get([FromQuery] string q, double? latitude = null, double? longitude = null)
        {

            string error = "";
            bool res = false;
            string uri = "..\\API-suggestions\\bin\\Debug\\netcoreapp3.1\\cities_canada-usa.tsv";
            Suggestions cities = ProcessCities.getCities(uri, q, out error, out res, latitude, longitude);
            if (!res || cities.suggestions.Count == 0)
            {
                return cities = new Suggestions() { suggestions = new List<object>() };
            }
            return cities;
        }
    }
}
