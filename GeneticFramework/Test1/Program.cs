using System;
using System.Collections.Generic;
using System.Linq;
using GeneticFramework;
namespace Test1
{
    class Program
    {
        static Population<double> population;
        private static double FitnessFunc(double x, object[] objs)
        {
            var result = 1.0/(x*x-2*x-3);

            return result;
        }

        static double f(double x)
        {
            return Math.Pow(x, 3) / 3.0 - 3.0 - Math.Sin(5 * x) * x;
        }

        private static double GetPhenotype(byte[] arr)
        {
            var x = new byte[10];
            for (int i = 0; i < x.Length; i++)
            {
                x[i] = arr[i];
            }

            //var y = new byte[5];
            //for (int i = 0; i < y.Length; i++)
            //{
            //    y[i] = arr[x.Length+i];
            //}

            double z = 0.0;
            for (int i = x.Length - 1; i >= 0; i--)
            {
                z += x[i] * (int)Math.Pow(2, x.Length - i - 1);
            }

            //double d = 0.0;
            //for (int i = y.Length - 1; i >= 0; i--)
            //{
            //    d += y[i] * (int)Math.Pow(2, y.Length - i - 1);
            //}
            //d = d * Math.Pow(10, -("" + d).Length);
            //if (d >= 1.0) throw new Exception("adsdas");
            //z += d;

            return z;
        }
        static void Main(string[] args)
        {
            population = new Population<double>(100, 10, FitnessFunc, GetPhenotype);

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("i=" + i);
                Epoch();
            }
        }

        static double maxScore, maxPhen;
        static void Epoch()
        {
            GetMaxScore();
            population.Reproduction(null);
            population.Crossingover();
            population.Mutation();

            Console.WriteLine(maxPhen);
            Console.WriteLine(maxScore);
            if (Math.Abs(maxPhen - 3.0) < 0.01)
            {
                Console.ReadKey();
            }
        }

        private static void GetMaxScore()
        {
            population.Sort();
            var er = population.GetIndividuals().Last();
            var cost = er.GetCost(null);
            if (maxScore < cost)
            {
                maxScore = cost;
                maxPhen = er.GetPhenotype(er.chromosome);
            }

            foreach (var item in population.GetIndividuals())
            {
                Console.WriteLine(item.GetCost(null));
            }
        }
    }
}
