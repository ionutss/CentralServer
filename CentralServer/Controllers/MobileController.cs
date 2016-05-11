using CentralServer.Scanner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CentralServer.Controllers
{

    public class MobileController : ApiController
    {
        Activity activity = Activity.Instance;

        // POST: api/Mobile
        [HttpPost]
        public void Post(HttpRequestMessage request)
        {
            var definition = new { Name = "", Timestamp = ""};

            string jsonString = request.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeAnonymousType(jsonString, definition);

            System.Diagnostics.Debug.WriteLine("MPOST: " + result.Name);

            if (activity.BoardFact != "BedOn")
            {
                activity.AccFact = result.Name;

            }

           
        }
    }
}
