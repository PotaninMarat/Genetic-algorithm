using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticFramework.Math
{
    public class MathHelper
    {
        public static int[] Shuffle(int N)
        {
            int[] indexes = new int[N];
            for (int i = 0; i < indexes.Length; i++)
            {
                indexes[i] = i;
            }
            // создаем экземпляр класса Random для генерирования случайных чисел
            Random rand = new Random();

            for (int i = indexes.Length - 1; i >= 1; i--)
            {
                int j = rand.Next(i + 1);

                int tmp = indexes[j];
                indexes[j] = indexes[i];
                indexes[i] = tmp;
            }


            return indexes;
        }
    }
}
