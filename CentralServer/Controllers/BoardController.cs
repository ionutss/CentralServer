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
            var definition = new { type = "", room = "" , date = ""};

            string jsonString = request.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeAnonymousType(jsonString, definition);

            System.Diagnostics.Debug.WriteLine("BPOST: " + result.room);

            if(result.type == "room")
            {
                switch (result.room)
                {
                    case "1":
                        activity.BoardFact = "Hall";
                        break;
                    case "2":
                        activity.BoardFact = "Kitchen";
                        break;
                    case "3":
                        activity.BoardFact = "Bedroom";
                        break;
                    case "4":
                        activity.BoardFact = "Bathroom";
                        break;
                }
            }
            else if(result.type == "bed")
            {
                if(result.room == "1")
                {
                    activity.BoardFact = "BedOn";
                }
                else
                {
                    //activity.BoardFact = "BedOff";
                }
            }

           // activity.BoardFact = result.Name;

        }

    }
}
