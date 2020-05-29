using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticFramework.ILayer
{
    public interface IPopulation<T>
    {
        //List<IIndividual<T>> individuals { get; set; }

        Func<byte[], T> GetPhenotype { get; set; }

        Func<T, object[], double> FitnessFunc { get; set; }

        void Reproduction(object[] objs);

        void Crossingover(double Pc);

        void Mutation(double Pm);

        void RandomInit();

        List<IIndividual<T>> GetIndividuals();
    }
}
