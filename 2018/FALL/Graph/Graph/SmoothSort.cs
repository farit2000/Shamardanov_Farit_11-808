using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    internal class SmoothSort
    {
        //input array to sort
        public void Sort(int[] InputArray)
        {
            int len = InputArray.Length;
            for (int i = len / 2 - 1; i >= 0; i--)
                LeonardHeap(InputArray, len, i);
            //create binary Heap
            for (int i = len - 1; i >= 0; i--)
            {
                int temp = InputArray[0];
                InputArray[0] = InputArray[i];
                InputArray[i] = temp;
                LeonardHeap(InputArray, i, 0);
            }
        }

        public void Sort(LinkedList<int> linkedList)
        {
            var InputArray = linkedList.ToArray();
            int len = InputArray.Length;
            for (int i = len / 2 - 1; i >= 0; i--)
                LeonardHeap(InputArray, len, i);
            //create binary Heap
            for (int i = len - 1; i >= 0; i--)
            {
                int temp = InputArray[0];
                InputArray[0] = InputArray[i];
                InputArray[i] = temp;
                LeonardHeap(InputArray, i, 0);
            }
        }

        //to compare the childs with root to make the max-heap
        void LeonardHeap(int[] arr, int len, int index)
        {
            int lar = index;
            int l = 2 * index + 1;
            int r = 2 * index + 2;
            if (l < len && arr[l] > arr[lar])
                lar = l;
            if (r < len && arr[r] > arr[lar])
                lar = r;
            if (lar != index)
            {
                int swap = arr[index];
                arr[index] = arr[lar];
                arr[lar] = swap;
                LeonardHeap(arr, len, lar);
            }
        }

        static void PrintSortedArray(int[] array)
        {
            int len = array.Length;
            for (int i = 0; i < len; ++i)
                Console.WriteLine(array[i] + "");
            Console.Read();
        }
    }
}