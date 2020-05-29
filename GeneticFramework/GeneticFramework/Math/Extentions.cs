using GeneticFramework.Base;
using GeneticFramework.ILayer;
using MatLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GeneticFramework.Math
{
    public static class Extentions
    {
        public static int Round(this double val)
        {
            int z = (int)val;
            double mantiss = val - z;
            return (mantiss >= 0.5) ? (z + 1) : z;
        }

        //public static Individual<Vector> Copy(this Individual<Vector> individual)
        //{
        //    var _individual = new Individual<Vector>();

        //    _individual.chromosome = new byte[individual.chromosome.Length];
        //    Array.Copy(individual.chromosome, _individual.chromosome, individual.chromosome.Length);

        //    _individual.FitnessFunc = individual.FitnessFunc;

        //    return _individual;
        //}

        public static Individual<T> Copy<T>(this Individual<T> individual)
        {
            Individual<T> result = new Individual<T>(individual.chromosome.Length, individual.FitnessFunc, individual.GetPhenotype);
            result.chromosome = individual.chromosome.Copy();

            return result;
        }

        public static byte[] Copy(this byte[] bytes)
        {
            var result = new byte[bytes.Length];
            Array.Copy(bytes, result, bytes.Length);

            return result;
        }
    }
}
