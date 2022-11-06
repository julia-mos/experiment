using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Experiment
{

    public static class Extensions
    {
        public static int[] GenerateArray(this TabTypes tab, int size, int minVal=0, int maxVal=0)
        {
            switch (tab)
            {
                case TabTypes.Random:
                    return Generators.GenerateRandom(size, minVal, maxVal);
                case TabTypes.Sorted:
                    return Generators.GenerateSorted(size, minVal, maxVal);
                case TabTypes.Reversed:
                    return Generators.GenerateReversed(size, minVal, maxVal);
                case TabTypes.AlmostSorted:
                    return Generators.GenerateAlmostSorted(size, minVal, maxVal);
                case TabTypes.FewUnique:
                    return Generators.GenerateFewUnique(size);
                default:
                    return Generators.GenerateRandom(size, minVal, maxVal);
            }
        }

        public static void Sort(this AlgorithmTypes algorithm, int[] array)
        {
            switch (algorithm)
            {
                case AlgorithmTypes.InsertionSort:
                    SortingAlgorithms.InsertionSort(array);
                    break;
                case AlgorithmTypes.MergeSort:
                    SortingAlgorithms.MergeSort(array, 0, array.Length-1);
                    break;
                case AlgorithmTypes.QuickSortClassical:
                    SortingAlgorithms.QuickSort(array, 0, array.Length-1);
                    break;
                case AlgorithmTypes.QuickSort:
                    Array.Sort(array);
                    break;
                default:
                    Array.Sort(array);
                    break;

            }
        }

        public static double standardDeviation(this IEnumerable<double> sequence)
        {
            double average = sequence.Average();
            double sum = sequence.Sum(d => Math.Pow(d - average, 2));
            return Math.Sqrt((sum) / sequence.Count());
        }
    }


    public enum TabTypes
    {
        Random,
        Sorted,
        Reversed,
        AlmostSorted,
        FewUnique
    }

    public enum AlgorithmTypes
    {
        InsertionSort,
        MergeSort,
        QuickSortClassical,
        QuickSort
    }

    class Program
    {
        private static int[] tabSizes = new int[] { 100, 1000, 100000 };
        private static string[] labels = new string[] { "mała", "średnia", "duża" };

        static void Main(string[] args)
        {
            for(int i=0; i<tabSizes.Length; i++)
            {
                for(int j=0; j< Enum.GetValues(typeof(TabTypes)).Length; j++)
                {
                    TabTypes tabType = (TabTypes)Enum.GetValues(typeof(TabTypes)).GetValue(j);
                    int size = tabSizes[i];


                    Console.WriteLine($"Przypadek {(i*5) +(j+1)}: próba {labels[i]} (n = {size}) {tabType}");

                    GetAttempt(tabType.GenerateArray(size, 0, 1000));
                }
            }
        }

        static void GetAttempt(int[] array)
        {
            Stopwatch stopWatch = new Stopwatch();

            for (int i = 0; i < Enum.GetValues(typeof(AlgorithmTypes)).Length; i++)
            {
                AlgorithmTypes algorithm = (AlgorithmTypes)Enum.GetValues(typeof(AlgorithmTypes)).GetValue(i);


                List<double> times = new List<double> {};

                for (int j=0; j<10; j++)
                {
                    int[] arrayClone = (int[])array.Clone();

                    stopWatch.Start();

                    algorithm.Sort(arrayClone);

                    stopWatch.Stop();

                    times.Insert(j, stopWatch.ElapsedMilliseconds);
                }

                Console.WriteLine($"{algorithm}: t = {times.Average()}ms +/- {times.standardDeviation()}ms");
            }
        }
        
    }
}
