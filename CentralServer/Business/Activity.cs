using CentralServer.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralServer.Scanner
{
    public class Activity
    {
        private static Activity instance;
        private int hour = 8;
        private string accFact;
        private string boardFact;
        private string[] observationsFile;
        List<string> problemArrayS = new List<string>();
        FileLoader FL = new FileLoader();
        ForwardViterbi FV;
        public string AccFact
        {
            get
            {
                return this.accFact;
            }
            set
            {
                this.accFact = value;

                if(DateTime.Now.Hour != hour)
                {
                    hour = DateTime.Now.Hour;
                    problemArrayS.Clear();
                    problemArrayS.Add(boardFact);
                    problemArrayS.Add(accFact);
                }

                problemArrayS.Add(accFact);

                if (boardFact == "BedOn")
                {
                    FL = new FileLoader();
                    observationsFile = FL.fileLoaderSingleLineString("Bedroom\\observations.txt");
                }
                else if (boardFact == "Hall"){
                    System.Diagnostics.Debug.WriteLine("Currently in HALL and " + accFact);
                }
                else
                {
                    FL = new FileLoader();
                    observationsFile = FL.fileLoaderSingleLineString(boardFact + "\\observations.txt");

                }


                int[] problemArray = FL.string2Int(problemArrayS.ToArray(), observationsFile);

                System.Diagnostics.Debug.WriteLine("ACC FACT POST:");
                foreach (var z in problemArray)
                {
                    System.Diagnostics.Debug.WriteLine(z);


                }
                FV.Process(problemArray);
            }
        }
        public string BoardFact
        {
            get
            {
                return this.boardFact;
            }
            set
            {
                this.boardFact = value;
            
                problemArrayS.Clear();
                problemArrayS.Add(boardFact);

                if(boardFact == "BedOn")
                {
                    FL = new FileLoader();
                    observationsFile = FL.fileLoaderSingleLineString("Bedroom\\observations.txt");
                }
                else
                {
                    FL = new FileLoader();
                    observationsFile = FL.fileLoaderSingleLineString(boardFact + "\\observations.txt");

                }

                int[] problemArray = FL.string2Int(problemArrayS.ToArray(), observationsFile);

                System.Diagnostics.Debug.WriteLine("BOARD FACT POST:");
                foreach(var z in problemArray)
                {
                    System.Diagnostics.Debug.WriteLine(z);

                }
                FV = new ForwardViterbi(boardFact);
                FV.Process(problemArray);

            }
        }
        public int TimeStamp;

        private Activity() {

            FV = new ForwardViterbi("Bedroom");
            observationsFile = FL.fileLoaderSingleLineString("Bedroom\\observations.txt");

            this.boardFact = "BedOn";

        }
       

        public static Activity Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new Activity();
                   
                }
                return instance;
            }
        }


        public string MorningFilter()
        {
            string ret = "";

            if(TimeStamp >= 8 && TimeStamp < 9)
            {
                if(boardFact == "BedOff")
                {
                    return "Waking Up";
                }
                if(boardFact == "Bathroom" && accFact == "Nonperiodic")
                {
                    return "Probably doing morning toilet";
                }
            }
            if (TimeStamp >= 9 && TimeStamp < 10)
            {

            }
            if (TimeStamp >= 10 && TimeStamp < 12)
            {

            }

            return ret;
        }

        public string LunchFilter()
        {
            string ret = "";

            return ret;
        }

        public string AfternoonFilter()
        {
            string ret = "";

            return ret;
        }

        public string EveningFilter()
        {
            string ret = "";

            return ret;
        }

        public string NightFilter()
        {
            string ret = "";

            return ret;
        }
    }
}