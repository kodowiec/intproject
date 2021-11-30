using System;
using System.Net;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace intproj.Utils
{
    public class ResponseBody
    {
        public string destination;
        public string fullName;
        public List<string> path;
    }

    public class ErrorResponseBody
    {
        public string error;
    }

    public class ResponseUtils
    {
        public static IActionResult JsonResponse(HttpStatusCode status, object content)
        {
            return new ContentResult
            {
                StatusCode = (int)status,
                Content = Newtonsoft.Json.JsonConvert.SerializeObject(content),
                ContentType = "application/json"
            };
        }
    }
}

