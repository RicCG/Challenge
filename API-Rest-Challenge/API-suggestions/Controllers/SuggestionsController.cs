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
        public ActionResult<List<Citie>> Get()
        {

            string error = "";
            bool res = false;

            List<Citie> cities = ProcessCities.getCities("q=Beau&lat=53.35013&long=-113.41871", out res, out error);
            return cities;
        }
    }
}
