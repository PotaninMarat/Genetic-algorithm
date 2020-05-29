using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticFramework.ILayer
{
    public interface IIndividual<T>
    {
        byte[] chromosome { get; set; }
        
        //Random random { get; set; }
        Func<T, object[], double> FitnessFunc { get; set; }

        Func<byte[], T> GetPhenotype { get; set; }

        double GetCost(object[] objs);

        void RandomInit(Random rand);
    }
}
