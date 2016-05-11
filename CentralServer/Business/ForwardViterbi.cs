using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralServer.Business
{
    public class ForwardViterbi
    {

        // The Class Globals
        string[] states;
        string[] observations;
        double[] startProbability;
        double[,] transitionProbability;
        double[,] emissionProbability;
        double scaleFactor;
        string HMM = "";

        string[] observationsFile;
        string[] statesFile;

        //Computed Variables
        int[] vPath; //The Viterbi Path
        double[] vProbs;

        //----------------------------------------------------------------------
        // The Getters or Accessors

        public int[] VPath
        {
            get { return vPath; }
        }

        public double[] VProbs
        {
            get { return vProbs; }
        }

        //----------------------------------------------------------------------
        //Constructor
        public ForwardViterbi(string[] states, string[] observations, double[] startProbability, double[,] transitionProbability, double[,] emissionProbability, double scaleFactor)
        {

            this.states = states;
            this.observations = observations;
            this.startProbability = startProbability;
            this.transitionProbability = transitionProbability;
            this.emissionProbability = emissionProbability;
            this.scaleFactor = scaleFactor;

        }

        public ForwardViterbi(string hmm)
        {
            double scaleFactor = 10;

            //Fire it up
            //Read some stuff from disk
            FileLoader FL = new FileLoader();
            double[,] emissionFile = FL.fileLoaderDouble(hmm + "\\emissionProbability.txt");
            double[,] transitionFile = FL.fileLoaderDouble(hmm + "\\transitionProbability.txt");

            string[] startFileS = FL.fileLoaderSingleLineString(hmm + "\\startProbability.txt");
            double[] startFile = FL.stringAry2Double(startFileS);

            statesFile = FL.fileLoaderSingleLineString(hmm + "\\states.txt");
            observationsFile = FL.fileLoaderSingleLineString(hmm + "\\observations.txt");

            //string[] problemFileS = FL.fileLoaderSingleLineString("problem.txt");
            //int[] problemFile = FL.stringAry2Int(problemFileS);

            foreach(var x in observationsFile)
            {
                System.Diagnostics.Debug.WriteLine(x);

            }



            this.states = statesFile;
            this.observations = observationsFile;
            this.startProbability = startFile;
            this.transitionProbability = transitionFile;
            this.emissionProbability = emissionFile;
            this.scaleFactor = scaleFactor;


        }

        //----------------------------------------------------------------------
        //The Methods

        public void Process(int[] problem)
        {

            double[,] T = new double[states.Length, 3];  //We will store the probability sequence for the Viterbi Path
            vPath = new int[problem.Length];
            vProbs = new double[problem.Length];

            //initialize T
            //------------------------------------------------------------------	
            for (int state = 0; state < states.Length; state++)
            {
                T[state, 0] = startProbability[state];
                T[state, 1] = state;
                T[state, 2] = startProbability[state];
            }

            for (int output = 0; output < problem.Length; output++)
            {

                double[,] U = new double[states.Length, 3];  //We will use this array to calculate the future probabilities

                double highest = 0;

                for (int nextState = 0; nextState < states.Length; nextState++)
                {
                    double total = 0;
                    double argMax = 0;
                    double valMax = 0;

                    for (int state = 0; state < states.Length; state++)
                    {
                        double prob = T[state, 0];
                        double v_path = T[state, 1];
                        double v_prob = T[state, 2];
                        double p = emissionProbability[state, problem[output]] * transitionProbability[state, nextState] * scaleFactor;
                        prob *= p;
                        v_prob *= p;
                        total += prob;

                        if (v_prob > valMax)
                        {
                            valMax = v_prob;
                            argMax = nextState;
                        }

                        if (v_prob > highest)
                        {
                            highest = v_prob;
                            vPath[output] = nextState;
                            vProbs[output] = v_prob;
                        }
                    }

                    U[nextState, 0] = total;
                    U[nextState, 1] = argMax;
                    U[nextState, 2] = valMax;
                }
                T = U;

                System.Diagnostics.Debug.WriteLine("\nThe most probable outcome would be (VPath|Vprobability):");
                for (int i = 0; i < VPath.Length; i++)
                {
                    if (VProbs[i] > 0)
                    {
                        //System.Diagnostics.Debug.WriteLine("Element no.{0} ({1})->{2}", i, VProbs[i], i + 1);
                        //Console.WriteLine("Element no.{0} ({1})->{2}", i, FV.VProbs[i],  i + 1);
                        System.Diagnostics.Debug.WriteLine("Element no.{0} ({1})->{2}\t\t{3}\t/{4}^{5}", i, observationsFile[problem[i]], statesFile[VPath[i]], VProbs[i], scaleFactor, i + 1);
                    }
                }
            }


            //Apply SumMax
            double Total = 0;
            double ValMax = 0;

            for (int state = 0; state < states.Length; state++)
            {
                double prob = T[state, 0];
                double v_path = T[state, 1];
                double v_prob = T[state, 2];

                Total += prob;
                if (v_prob > ValMax)
                {
                    ValMax = v_prob;
                }
            }

            Console.WriteLine("\nAnalysis: Total probability (sum of all paths) for the given state is :: {0}\nThe Viterbi Path Probability is :: {1}", Total, ValMax);
            Console.WriteLine("The above results are presented with a scale factor of {0}^{1}", scaleFactor, problem.Length);

        }
    }; // end Forward Viterbi Class
}