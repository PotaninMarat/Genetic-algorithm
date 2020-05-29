using System;
using System.Collections.Generic;
using System.Numerics;
using System.IO;
namespace MatLib
{
    [Serializable]
    public class Vector
    {
        public int Length;
        public double[] elements;
        public Complex[] elementsF;
        Random random = new Random(12);
        public static Vector GaussRandom(int N)
        {
        	Vector randVect = new Vector(N);
        	randVect.GaussRandom();
        	return randVect;
        }
        public double this[int i]
        {
            get { return elements[i]; }
            set { elements[i] = value; }
        }
        public override string ToString()
        {
            string Text = "";
            for (int i = 0; i < Length; i++)
                    Text += elements[i] + " ";
            return Text;
            //return base.ToString();
        }
        public static bool operator ==(Vector a, Vector b)
        {
            if(a.Length == b.Length)
            {
                for (int i = 0; i < a.Length; i++)
                    if (a[i] != b[i]) return false;

                return true;
            }
            return false;
        }

        public static bool operator !=(Vector a, Vector b)
        {
            if (a.Length == b.Length)
            {
                for (int i = 0; i < a.Length; i++)
                    if (a[i] != b[i]) return true;

                return false;
            }
            return true;
        }
        public static Vector operator +(Vector a, Vector b)
        {
            if (a.Length == b.Length)
            {
                int N = a.Length;
                Vector result = new Vector(N);
                for (int i = 0; i < N; i++)
                    result[i] = a[i] + b[i];
                return result;
            }
            else return null;
        }
        public static Vector operator +(Vector a, double b)
        {
            Vector result = new Vector(a.Length);
            for (int i = 0; i < a.Length; i++)
                result[i] = a[i] + b;
            return result;
        }
        public static Vector operator -(Vector a, Vector b)
        {
            if (a.Length == b.Length)
            {
                Vector result = new Vector(a.Length);
                for (int i = 0; i < a.Length; i++)
                    result[i] = a[i] - b[i];
                return result;
            }
            else return null;
        }
        public static Vector operator -(Vector a, double b)
        {
            Vector result = new Vector(a.Length);
            for (int i = 0; i < a.Length; i++)
                result[i] = a[i] - b;
            return result;
        }
        public static Vector operator -(double b, Vector a)
        {
            Vector result = new Vector(a.Length);
            for (int i = 0; i < a.Length; i++)
                result[i] = b - a[i];
            return result;
        }
        public static double operator *(Vector a, Vector b)
        {
            if (a.Length == b.Length)
            {
                double result = 0.0;
                for (int i = 0; i < a.Length; i++)
                    result += a[i] * b[i];
                return result;
            }
            else
            {
                Exception exception = new Exception("Error: a.Length != b.Length");
                throw exception;
            }
        }
        public static Vector operator *(Vector a, double b)
        {
            Vector result = new Vector(a.Length);
            for (int i = 0; i < a.Length; i++)
                result[i] = a[i] * b;
            return result;
        }
        public static Vector operator /(Vector a, double b)
        {
            Vector result = new Vector(a.Length);
            for (int i = 0; i < a.Length; i++)
                result[i] = a[i] / b;
            return result;
        }
        public static Vector operator *(double b, Vector a)
        {
            Vector result = new Vector(a.Length);
            for (int i = 0; i < a.Length; i++)
                result[i] = a[i] * b;
            return result;
        }
        /// <summary>
///
/// </summary>
/// <param name="Length">Длина вектора</param>
        public Vector(int Length)
        {
            elements = new double[Length];
            this.Length = Length;
        }
        public Vector(double[] x)
        {
            this.elements = new double[x.Length];
            this.Length = x.Length;
            x.CopyTo(this.elements, 0);
        }
        public Vector(double x)
        {
            elements = new double[1];
            Length = 1;
            elements[0] = x;
        }
        public Vector(double x1, double x2)
        {
            elements = new double[2];
            Length = 2;
            elements[0] = x1;
            elements[1] = x2;
        }
        public Vector(List<double> x)
        {
            this.elements = new double[x.Count];
            this.Length = x.Count;
            x.CopyTo(this.elements, 0);
        }
        public Vector(List<int> x)
        {
            this.elements = new double[x.Count];
            this.Length = x.Count;
            for (int i = 0; i < x.Count; i++){
                this.elements[i] = x[i];
            }

        }
        public Vector(Vector x)
        {
            Length = x.Length;
            elements = new double[x.Length];
            x.elements.CopyTo(elements, 0);
        }
        public Vector(object x)
        {
            ((double[])x).CopyTo(this.elements, 0);
            Length = elements.Length;
        }
        public Vector(double[,] x)
        {
            elements = new double[x.GetLength(0) * x.GetLength(1)];
            Length = elements.Length;
            int count = 0;
            for (int i = 0; i < x.GetLength(0); i++)
                for (int j = 0; j < x.GetLength(1); j++)
                    elements[count++] = x[i, j];
        }
        public Vector MultiplyElements(Vector b)
        {
            if(Length == b.Length)
            {
                Vector result = new Vector(Length);
                for (int i = 0; i < Length; i++)
                    result[i] = elements[i] * b[i];
                return result;
            }
            return null;
        }
        public double MultiplyElements()
        {
                double result = elements[0];
                for (int i = 1; i < Length; i++)
                    result *= elements[i];
                return result;
        }
        public void Sigmoida()
        {
            for (int i = 0; i < elements.Length; i++) elements[i] = 1.0 / (1.0 + Math.Exp(-elements[i]));
        }
        public double Average()
        {
            double sum = 0.0;
            for (int i = 0; i < Length; i++)
                sum += elements[i];
            return sum / Length;
        }
        public double Dispersion()
        {
            double dispersion = 0.0;
            double averageVal = Average();
            for (int i = 0; i < Length; i++)
                dispersion += (elements[i] - averageVal) * (elements[i] - averageVal);
            dispersion /= (Length - 1.0);
            return dispersion;
        }
        public double Dispersion(double average)
        {
            double dispersion = 0.0;
            double averageVal = average;
            for (int i = 0; i < Length; i++)
                dispersion += (elements[i] - averageVal) * (elements[i] - averageVal);
            dispersion /= (Length - 1.0);
            return dispersion;
        }
       void SaveVector(string path, int fd)
        {
            File.Delete(path);
            Stream waveFileStream = File.OpenWrite(path);
            BinaryWriter br = new BinaryWriter(waveFileStream);
            br.Write((Int32)1179011410);
            br.Write((Int32)(2 * Length + 36));
            br.Write((Int32)1163280727);
            br.Write((Int32)544501094);
            br.Write((Int32)16);
            br.Write((Int16)1);
            br.Write((Int16)1);
            br.Write(fd);
            br.Write((Int32)(2 * fd));
            br.Write((Int16)2);
            br.Write((Int16)16);
            br.Write((Int32)1635017060);
            br.Write((Int32)(2 * Length));

            double max = Math.Abs(elements[0]);
            for (int i = 1; i < Length; i++)
                if (max < Math.Abs(elements[i]))
                    max = Math.Abs(elements[i]);

            for (int i = 0; i < Length; i++)
            {
                elements[i] /= max;
                br.Write((Int16)(elements[i] * 32000));
            }

            br.Close();

        }
        public Vector Reverse()
        {
            Vector res = new Vector(Length);
            for (int i = 0; i < Length; i++)
                res[i] = elements[Length - 1 - i];

            return res;
        }
       
       
        public void Swap(int i, int j)
        {
            double c = elements[i];
            elements[i] = elements[j];
            elements[j] = c;
        }
        public void DsigmoidaY()
        {
            for (int i = 0; i < elements.Length; i++) elements[i] = elements[i] * (1.0 - elements[i]);
        }

        public void DhyperbolicTangentY()
        {
            for (int i = 0; i < elements.Length; i++) elements[i] = 1.0 - Math.Pow(elements[i], 2.0);
        }
        public double EuclidNorm()
        {
            double result = 0.0;
            foreach (double value in elements)
                result += Math.Pow(value, 2.0);
            result = Math.Sqrt(result);
            return result;
        }
        public void Normalize(double degree)
        {
            double sum = 0.0;
            foreach (double value in elements)
                sum += Math.Pow(value, degree);
            sum = Math.Pow(sum, 1.0 / degree);
            for (int i = 0; i < Length; i++)
                elements[i] = elements[i] / sum;
        }
        static public double Cov(Vector x, Vector y)
        {
            double cov = 0.0;
            double averX = x.Average();
            double averY = y.Average();
            for (int i = 0; i < x.Length; i++)
            {
                cov += (x[i] - averX) * (y[i] - averY);
            }
            cov /= (double)x.Length - 1;
            return cov;
        }
        public void Resize(int N)
        {
            Length = N;
            Array.Resize(ref elements, N);
        }
        public void Fourier()
        {
            int N = Length;
            Complex i = new Complex(0.0, -1.0);
            Vector result = new Vector(N);
            elementsF = new Complex[N];
                for (int k = 0; k < N; k++)
                {
                    Complex q = new Complex(0, 0);
                    for (int n = 0; n < N; n++)
                        q += elements[n] * Complex.Exp((-2.0 * Math.PI * i * k * n) / N);
                    result.elements[k] = q.Magnitude;
                    elementsF[k] = q;
                }
                elements = result.elements;
        }
        public void InverseFourier()
        {
            int N = Length;
            Complex i = new Complex(0.0, -1.0);
            for (int k = 0; k < N; k++)
            {
                Complex q = new Complex(0, 0);
                for (int n = 0; n < N; n++)
                    q += elementsF[n] * Complex.Exp((2.0 * Math.PI * i * k * n) / (double)N);
                q /= (double)N;
                elements[k] = q.Magnitude;
            }
        }
        public bool Exist(double x)
        {
            for (int i = 0; i < Length; i++)
            {
                if (x == elements[i]) return true;
            }
            return false;
        }
        public int IndexMax()
        {
            int indMax = 0;
            for (int i = 1; i < Length; i++)
                if (elements[indMax] < elements[i]) indMax = i;
            

            return indMax;
        }
        public int IndexMin()
        {
            int indMin = 0;
            for (int i = 1; i < Length; i++)
                if (elements[indMin] > elements[i]) indMin = i;


            return indMin;
        }
        public Vector ElementsPow(double a)
        {
            Vector res = new Vector(elements.Length);
            for (int i = 0; i < elements.Length; i++) res.elements[i] = Math.Pow(elements[i], a);
            return res;
        }
        public Vector Round(int a)
        {
            Vector result = new Vector(elements.Length);
            for (int i = 0; i < elements.Length; i++) result.elements[i] = Math.Round(elements[i], a);
            return result;
        }
        public double EuclideanDistance(Vector x)
        {
            double result = 0.0;
            for (int i = 0; i < elements.Length; i++) result += Math.Pow(elements[i] - x[i], 2.0);
            result = Math.Sqrt(result);
            return result;
        }
        public double Distance(Vector b)
        {
            double result = 0.0;
            for (int i = 0; i < elements.Length; i++)
                result += Math.Abs(elements[i] - b[i]);

            result /= (double)elements.Length;

            return result;
        }
        public bool IsNaN()
        {
            for (int i = 0; i < Length; i++)
                if (Double.IsNaN(elements[i]))
                    return true;
            return false;
        }
        public bool IsInfinity()
        {
            for (int i = 0; i < Length; i++)
                if (Double.IsInfinity(elements[i]))
                    return true;
            return false;
        }
        public void Set(double[] x)
        {
            this.elements = new double[x.Length];
            this.Length = x.Length;
            x.CopyTo(this.elements, 0);
        }
        public void GaussRandom()
        {
            for (int i = 0; i < Length; i++)
                elements[i] = Gauss();
        }

        private double Gauss()
        {
            double u, v, s;
            do
            {
                u = 2.0 * random.NextDouble() - 1.0;
                v = 2.0 * random.NextDouble() - 1.0;
                s = u * u + v * v;
            } while (s == 0);
            return v * Math.Sqrt(Math.Abs(2.0 * Math.Log(s) / s));
        }
        public Vector GetInterval(int a, int b)
        {
            if (b >= Length) 
                b = Length - 1;
            Vector result = new Vector(b - a + 1);
            for (int i = a; i <= b; i++)
                result[i - a] = elements[i];
            return result;
        }
        public static Vector GetVectorIndexes(int n, int startPos = 1)
        {
            Vector vec = new Vector(n);
            for (int i = 0; i < n; i++)
            {
                vec[i] = startPos++;
            }
            return vec;
        }
        public void SortAscending()
        {
            Array.Sort(elements);
        }
        public void SortDecreasing()
        {
            Array.Sort(elements);
            double[] arr = new double[Length];
            elements.CopyTo(arr, 0);
            for (int i = 0; i < Length; i++)
                elements[i] = arr[Length - i - 1];
        }

        public static Vector Join(Vector a, Vector b)
        {
            Vector c = new Vector(a.Length + b.Length);

            for (int i = 0; i < a.Length; i++) {
                c[i] = a[i];
            }

            for (int i = 0; i < b.Length; i++){
                c[a.Length + i] = b[i];
            }

            return c;
        }
        public void Remove(int index)
        {
            double[] newArr = new double[Length - 1];
            int i_ = 0;
            for (int i = 0; i < Length; i++)
            if(i != index)
                newArr[i_++] = elements[i];
            Length--;
            elements = newArr;
        }


        private static bool ExistVal(Vector x, double v, int n)
        {
            for (int i = 0; i < n; i++)
            {
                if (x[i] == v)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Генерирует вектор в котором все значения целые и отличны друг от друга
        /// </summary>
        /// <returns></returns>
        public static Vector GenVecInd(int n, int max)
        {
            Vector res = new Vector(n);
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
            {
                int v = (int)(max * rnd.NextDouble());
                while (ExistVal(res, v, i)) { v = (int)(max * rnd.NextDouble()); }
                res[i] = v;
            }
            return res;
        }
    }
}
