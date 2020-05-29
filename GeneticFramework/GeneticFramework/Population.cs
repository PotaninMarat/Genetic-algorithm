using System;
using System.Linq;
using System.Collections.Generic;
using GeneticFramework.ILayer;
using MatLib;
using GeneticFramework.Math;
using GeneticFramework.Base;

namespace GeneticFramework
{
    public class Population<T> : IPopulation<T>
    {
        List<Individual<T>> individuals { get; set; }
        public Func<T, object[], double> FitnessFunc { get; set; }
        public Func<byte[], T> GetPhenotype { get; set; }
        public int N;
        public int M;
        Random random;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="N">Количество особей</param>
        /// <param name="M">Количество хромосом</param>
        public Population(int N, int M, Func<T, object[], double> fitnessFunc, Func<byte[], T> GetPhenotype)
        {
            this.N = N;
            this.M = M;
            this.FitnessFunc = fitnessFunc;
            this.GetPhenotype = GetPhenotype;
            individuals = new List<Individual<T>>();
            random = new Random(2);

            RandomInit();
        }
        public void RandomInit()
        {
            for (int i = 0; i < N; i++)
            {
                var individual = new Individual<T>(M, FitnessFunc, GetPhenotype, random);
                individuals.Add(individual);
            }
        }

        public void Crossingover(double Pc = 0.5)
        {
            var indexes = MathHelper.Shuffle(individuals.Count);
            for (int i = 0; i < indexes.Length-1; i++)
            {
                if(random.NextDouble() < Pc)
                {
                    var first = individuals[indexes[i]];
                    var second = individuals[indexes[i + 1]];

                    var chromosome1 = new byte[first.chromosome.Length];
                    var chromosome2 = new byte[second.chromosome.Length];

                    var k = random.Next(1, chromosome1.Length);

                    for (int j = 0; j < k; j++)
                    {
                        chromosome1[j] = first.chromosome[j];
                        chromosome2[j] = second.chromosome[j];
                    }

                    for (int j = k; j < chromosome1.Length; j++)
                    {
                        chromosome1[j] = second.chromosome[j];
                        chromosome2[j] = first.chromosome[j];
                    }

                    first.chromosome = chromosome1;
                    second.chromosome = chromosome2;
                }
            }
        }

        public void Mutation(double Pm = 0.01)
        {
            foreach(var individ in individuals)
            {
                if(random.NextDouble() < Pm)
                {
                    var k = random.Next(0, individ.chromosome.Length);
                    individ.chromosome[k] = (individ.chromosome[k] == 0) ? (byte)1 : (byte)0;
                }
            }
        }


        int start;
        public void Reproduction(params object[] objs)
        {
            int N = this.individuals.Count;
            //if (start == 0)
            //{
            //    N /= 2;
            //    start = N;
            //}

            var individuals = new List<Individual<T>>();
            List<double> costs = new List<double>();
            double sumCosts = 0.0;
            #region Get costs
            foreach (var individual in this.individuals)
            {
                var cost = individual.GetCost(objs);
                costs.Add(cost);
            }

            var min = costs.Min();
            for (int i = 0; i < costs.Count; i++)
            {
                costs[i] -= min;
                sumCosts += costs[i];
            }
            #endregion

            double[] p = new double[N];
            p[0] = costs[0] / sumCosts;
            for (int i = 1; i < N; i++)
            {
                p[i] = p[i - 1] + costs[i] / sumCosts;
            }

            for (int i = 0; i < N; i++)
            {
                double rnd = random.NextDouble();
                int index = 0;
                for (int j = 1; j < N; j++)
                {
                    if (rnd > p[j-1] && rnd <= p[j])
                    {
                        index = j;
                        break;
                    }
                }

                var individ = this.individuals[index];

                individuals.Add(individ.Copy());
            }

            this.individuals = individuals;
        }

        public List<IIndividual<T>> GetIndividuals()
        {
            var result = new List<IIndividual<T>>();
            foreach(var individ in individuals)
            {
                var elem = (IIndividual<T>)individ;
                result.Add(elem);
            }

            return result;
        }


        public void Sort()
        {
            individuals = individuals.OrderBy(x => x.GetCost(null)).ToList();
        }
    }
}
