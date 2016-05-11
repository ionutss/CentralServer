using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CentralServer.Scanner;
using Newtonsoft.Json;

namespace CentralServer.Controllers
{
    public class BoardController : ApiController
    {
        Activity activity = Activity.Instance;

        // POST: api/Board
        [HttpPost]
        public void Post(HttpRequestMessage request)
        {
            var definition = new { Name = "", Timestamp = "" };

            string jsonString = request.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeAnonymousType(jsonString, definition);

            System.Diagnostics.Debug.WriteLine("BPOST: " + result.Name);

            activity.BoardFact = result.Name;

            Console.WriteLine(result.Name);
        }

    }
}
