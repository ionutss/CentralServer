using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace CentralServer.Business
{
    public class FileLoader
    {
        private string Moment;

        //Constructor
        public FileLoader() {
            System.DateTime moment = System.DateTime.Now;
            int hour = moment.Hour;

            if(hour >= 22 && hour <= 7)
            {
                Moment = "Night";
            }
            else if(hour >= 14 && hour <= 18)
            {
                Moment = "Afternoon";
            }
            else if(hour >= 19 && hour <= 21)
            {
                Moment = "Evening";
            }
            else
            {
                Moment = hour.ToString();

            }

        }


        //Public Methods--------------------------------------------------------

        //This method reads a file with more than one line and returns its values in a double ary
        public double[,] fileLoaderDouble(string fileName)
        {

            // create reader & open file
            string path = System.Web.HttpContext.Current.Request.MapPath("~\\HiddenMarkovModels\\"+ Moment + "\\" + fileName);
            TextReader tr = new StreamReader(path);
            string read = null;
            int lineCounter = 0;

            //Lets create an ary and give it some space in memory, later we trim it down
            int[] fileInfo = getLinesAndRowsInFile(fileName);
            double[,] ary = new double[fileInfo[0], fileInfo[1]];
            Console.WriteLine("File " + fileName + " has {0} lines and {1} columns", fileInfo[0], fileInfo[1]);

            while ((read = tr.ReadLine()) != null)
            {
                if (read.Contains("#"))
                {
                    //This is a comment line, we should't do nothing
                }
                else
                {

                    string[] resultS = read.Split(' ');
                    double[] result = stringAry2Double(resultS);

                    //Console.WriteLine("Result Length = {0}", resultS.Length);
                    for (int i = 0; i < result.Length; i++)
                    {
                        //Console.WriteLine("...Processing line {0} with {1} colums", lineCounter, i);
                        ary[lineCounter, i] = result[i];
                    }
                    lineCounter++;
                }

            }

            return (ary);
        }

        //----------------------------------------------------------------------
        //This method reads a file that has a single line with its attributes separated by a space and returns a string ary

        public string[] fileLoaderSingleLineString(string fileName)
        {

            // create reader & open file
            string path = System.Web.HttpContext.Current.Request.MapPath("~\\HiddenMarkovModels\\" + Moment + "\\" + fileName);
            TextReader tr = new StreamReader(path);
            string read = null;

            //Lets create an ary and give it some space in memory, later we trim it down
            int[] fileInfo = getLinesAndRowsInFile(fileName);
            string[] ary = new string[fileInfo[1]];
            //Console.WriteLine("File has {0} lines and {1} columns", fileInfo[0], fileInfo[1]);

            while ((read = tr.ReadLine()) != null)
            {
                if (read.Contains("#"))
                {
                    //This is a comment line, we should't do nothing
                }
                else
                {

                    string[] resultS = read.Split(' ');

                    //Console.WriteLine("Result Length = {0}", resultS.Length);
                    for (int i = 0; i < resultS.Length; i++)
                    {
                        //Console.WriteLine("...Processing line {0} with {1} colums", lineCounter, i);
                        ary[i] = resultS[i];
                    }
                }

            }

            return (ary);
        }

        //----------------------------------------------------------------------

        public double[] stringAry2Double(string[] aryIn)
        {
            double[] returnAry = new double[aryIn.Length];

            for (int i = 0; i < aryIn.Length; i++)
            {
                returnAry[i] = double.Parse(aryIn[i]);
            }

            return (returnAry);
        }

        //----------------------------------------------------------------------

        public int[] stringAry2Int(string[] aryIn)
        {
            int[] returnAry = new int[aryIn.Length];

            for (int i = 0; i < aryIn.Length; i++)
            {
                returnAry[i] = int.Parse(aryIn[i]);
            }

            return (returnAry);
        }

        public int[] string2Int(string[] problem, string[] obs)
        {
            int[] returnAry = new int[problem.Length];
            int k = 0;

            for(var i = 0; i<problem.Length; i++)
            {
                for (var j = 0; j < obs.Length; j++)
                {
                    if (problem[i] == obs[j])
                    {
                        returnAry[k] = j;
                        k++;
                    }
                }
            }

            return (returnAry);
        }
        //Private Methods-------------------------------------------------------


        //This could be futurely optimized				
        private int[] getLinesAndRowsInFile(string fileName)
        {
            int[] answer = new int[2];

            string read;
            // create reader & open file
            string path = System.Web.HttpContext.Current.Request.MapPath("~\\HiddenMarkovModels\\" + Moment + "\\" + fileName);
            TextReader tr = new StreamReader(path);

            while ((read = tr.ReadLine()) != null)
            {
                if (read.Contains("#"))
                {
                    //This is a comment line, we should't do nothing
                }
                else
                {
                    string[] resultS = read.Split(' ');
                    answer[0]++;
                    answer[1] = resultS.Length;

                }

            }
            return (answer);
        }

    };
}