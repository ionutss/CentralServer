using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using CentralServer.Business;
using CentralServer.Scanner;

[assembly: OwinStartup(typeof(CentralServer.Startup))]

namespace CentralServer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

           

            //FileLoader FL = new FileLoader();
            //string[] problemFileS = FL.fileLoaderSingleLineString("problem.txt");

            //string[] observationsFile = FL.fileLoaderSingleLineString("observations.txt");

            //int[] problemFile = FL.string2Int(problemFileS, observationsFile);


            //ForwardViterbi FV = new ForwardViterbi();
            //FV.Process(problemFile);

            //System.Diagnostics.Debug.WriteLine("\nThe most probable outcome would be (VPath|Vprobability):");
            //for (int i = 0; i < FV.VPath.Length; i++)
            //{
            //    if (FV.VProbs[i] > 0)
            //    {
            //        System.Diagnostics.Debug.WriteLine("Element no.{0} ({1})->{2}", i, FV.VProbs[i], i + 1);
            //        //Console.WriteLine("Element no.{0} ({1})->{2}", i, FV.VProbs[i],  i + 1);

            //    }
            //}
        }
    }
}
