using System;
using System.Threading;

namespace Lab7_Threading_RecursiveSort
{
    struct Array
    {
        public int[] array;
        public int start;
        public int end;

        public Array(int[] array, int start, int end)
        {
            this.array = array;
            this.start = start;
            this.end = end;
        }
    }
    
    class Program
    {
        static private ParameterizedThreadStart mergeSortStart = new ParameterizedThreadStart(MergeSort);
        static private ParameterizedThreadStart mergeStart = new ParameterizedThreadStart(Merge);

        static void printArray(string caption, int[] array, int start, int end)
        {
            Console.Write(caption);
            for (int i = start; i <= end; i++)
            {
                Console.Write(array[i].ToString() + " ");
            }
            Console.Write("\r\n");
        }

        static void MergeSort(object param)
        {
            Array arrayObj = (Array)param;

            MergeSort(arrayObj.array, arrayObj.start, arrayObj.end);
        }

        static void MergeSort(int[] array, int start, int end)
        {
            if ((end - start) == 0)
            {
                return;
            }
            else
            {
                Array arrayObj;
                int middle = (start + end) / 2;

                Thread threadLeft = new Thread(mergeSortStart);
                Thread threadRight = new Thread(mergeSortStart);
                Thread threadMerge = new Thread(mergeStart);

                printArray("MergeSort Left: ", array, start, middle);
                arrayObj = new Array(array, start, middle);
                threadLeft.Start(arrayObj);

                printArray("MergeSort Right: ", array, middle + 1, end);
                arrayObj = new Array(array, middle + 1, end);
                threadRight.Start(arrayObj);

                threadLeft.Join();
                threadRight.Join();

                printArray("Merge: ", array, start, end);
                arrayObj = new Array(array, start, end);
                threadMerge.Start(arrayObj);
            }
        }

        static void Merge(object param)
        {
            Array arrayObj = (Array)param;
            Merge(arrayObj.array, arrayObj.start, arrayObj.end);
        }

        static void Merge(int[] array, int start, int end)
        {     
            int start2 = (start + end) / 2 + 1;
            int[] toMerge = new int[end - start + 1];

            int k = 0;
            int i = start;
            int j = start2;            

            while (i < start2 && j <= end)
            {
                if (array[i] <= array[j])
                {
                    toMerge[k] = array[i];
                    k = k + 1;
                    i = i + 1;
                }
                else
                {
                    toMerge[k] = array[j];
                    k = k + 1;
                    j = j + 1;
                }
            }

            while (i < start2)
            {
                toMerge[k] = array[i];
                k = k + 1;
                i = i + 1;
            }
            while (j <= end)
            {
                toMerge[k] = array[j];
                k = k + 1;
                j = j + 1;
            }

            int n = 0;
            for (int m = start; m <= end; m++)
            {
                array[m] = toMerge[n];
                n = n + 1;
            }

            printArray("Result Merge: ", array, start, end);
        }

        static void Main(string[] args)
        {
            int[] array = { 5, 3, 2, 6, 4, 1, 9, 7 };
            // 1 2 3 4 5 6 7 9

            Array arrayObj = new Array(array, 0, array.Length - 1);
            MergeSort(arrayObj);
            Console.ReadKey();
        }
    }
}
