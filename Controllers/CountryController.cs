using System;
using System.Net;
using System.Linq;
using intproj.Utils;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace intproj.Controllers
{
    [ApiController]
    public class CountryController : ControllerBase
    {

        private readonly ILogger<CountryController> _logger;

        public CountryController(ILogger<CountryController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("{countryCode}")]
        public IActionResult Get(string countryCode)
        {
            countryCode = countryCode.ToUpper();

            if (!Countries.IsAvailable(countryCode))
            {
                return ResponseUtils.JsonResponse(HttpStatusCode.NotFound, new ErrorResponseBody().error = "Invalid country code!");
            }

            Country destination = Countries.GetCountry(countryCode);

            var isDest = (destination.Code == "USA") ? true : false;
            int currDepth = destination.Depth;
            string prevCnt = destination.Code;

            ResponseBody resp = new ResponseBody();
            resp.destination = destination.Code;
            resp.fullName = destination.Name;
            resp.path = new List<string>(new string[destination.Depth + 1]);
            resp.path[0] = "USA"; //since we're starting @ USA
            resp.path[currDepth] = prevCnt;

            while (!isDest)
            {
                List<Country> tmpCntr = Countries.GetCountries(--currDepth, prevCnt);

                if (tmpCntr.Count == 0 || tmpCntr == null)
                {
                    isDest = true;
                }
                else if (tmpCntr.Find(x => x.Code == "USA") != null)
                {
                    isDest = true;
                }
                else if (tmpCntr.Count == 1)
                {
                    prevCnt = tmpCntr[0].Code;
                    currDepth = tmpCntr[0].Depth;
                    resp.path[currDepth] = prevCnt;
                }
            }

            return ResponseUtils.JsonResponse(HttpStatusCode.OK, resp);
        }
    }
}

