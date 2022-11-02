using System;
namespace Experiment
{
    public class Generators
    {
        public Generators()
        {
        }

        public static int[] GenerateRandom(int size, int minVal, int maxVal)
        {
            Random rnd = new Random();

            int[] a = new int[size];
            for (int i = 0; i < size; i++)
            {
                a[i] = rnd.Next(minVal, maxVal);
            }
            return a;
        }

        public static int[] GenerateSorted(int size, int minVal, int maxVal)
        {
            int[] a = GenerateRandom(size, minVal, maxVal);
            Array.Sort(a);
            return a;
        }

        public static int[] GenerateReversed(int size, int minVal, int maxVal)
        {
            int[] a = GenerateSorted(size, minVal, maxVal);
            Array.Reverse(a);
            return a;
        }

        public static int[] GenerateFewUnique(int size)
        {
            int maxVal = size / 10;
            int[] i = GenerateRandom(size, 0, maxVal);
            return i;
        }

        public static int[] GenerateAlmostSorted(int size, int minVal, int maxVal)
        {
            int[] a = GenerateRandom(size, minVal, maxVal);
            Array.Sort(a);

            Random rnd = new Random();


            for (int i=0; i<size; i += size / 10)
            {
                a[i] = rnd.Next(minVal, maxVal);
            }

            return a;
        }
    }
}
