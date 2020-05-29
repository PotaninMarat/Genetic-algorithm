using System;
using System.Collections.Generic;
using System.Text;
using GeneticFramework.ILayer;
using MatLib;

namespace GeneticFramework.Base
{
    public class Individual<T> : IIndividual<T>
    {
        public byte[] chromosome { get; set; }
        public Func<T, object[], double> FitnessFunc { get; set; }
        public Func<byte[], T> GetPhenotype { get; set; }
        //public Random random { get; set; }

        public Individual(int N, Func<T, object[], double> FitnessFunc, Func<byte[], T> GetPhenotype, Random rand)
        {
            //this.random = rand;
            this.FitnessFunc = FitnessFunc;
            this.GetPhenotype = GetPhenotype;
            chromosome = new byte[N];
            RandomInit(rand);
        }

        public Individual(int N, Func<T, object[], double> FitnessFunc, Func<byte[], T> GetPhenotype)
        {
            this.FitnessFunc = FitnessFunc;
            this.GetPhenotype = GetPhenotype;
            chromosome = new byte[N];
        }

        public double GetCost(object[] objs)
        {
            return FitnessFunc(GetPhenotype(chromosome), objs);
        }

        public void RandomInit(Random rand)
        {
            for (int i = 0; i < chromosome.Length; i++)
                chromosome[i] = (rand.NextDouble() > 0.5) ? (byte)1 : (byte)0;
        }
    }
}
