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

        // 1)Чтение по индексу
        // 2)Присвоение по индексу

        public void Sort(LinkedList<int> list)
        {
            //var InputArray = linkedList.ToArray();
            int len = list.Count;
            for (int i = len / 2 - 1; i >= 0; i--)
                LeonardHeap(list, len, i);
            //create binary Heap
            for (int i = len - 1; i >= 0; i--)
            {
                var temp = list.First.Value;
                list.WriteForIndex(0, list.ReadForIndex(i));
                list.WriteForIndex(i, temp);
                LeonardHeap(list, i, 0);
            }
        }


        //public int ReadForIndex(int index, LinList<int> list)
        //{
        //    int count = 0;
        //    foreach(var e in list)
        //    {
        //        if (index == count)
        //            return e;
        //        count++;
        //    }
        //    throw new NotImplementedException();
        //}

        //public int WriteForIndex(int index,int element, ref LinkedList<int> list)
        //{
        //    list.            int count = 0;
        //    for (var i = list.GetEnumerator(); i.MoveNext();)
        //    {
        //        LinkedListNode<int> e = i.Current;
        //        if (index == count)
        //            e.Value = element;
        //        count++;
        //    }

        //    throw new NotImplementedException();
        //}

        //public LinkedListNode<int> node(int index)
        //{
        //    // assert isElementIndex(index);

        //    if (index < ( >> 1))
        //    {
        //        LinkedListNode<int> x = first;
        //        for (int i = 0; i < index; i++)
        //            x = x.next;
        //        return x;
        //    }
        //    else
        //    {
        //        Node<E> x = last;
        //        for (int i = size - 1; i > index; i--)
        //            x = x.prev;
        //        return x;
        //    }
        //}


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

        void LeonardHeap(LinkedList<int> list, int len, int index)
        {
            int lar = index;
            int l = 2 * index + 1;
            int r = 2 * index + 2;
            if (l < len && list.ReadForIndex(l) > list.ReadForIndex(lar))
                lar = l;
            if (r < len && list.ReadForIndex(r) > list.ReadForIndex(lar))
                lar = r;
            if (lar != index)
            {
                int swap = list.ReadForIndex(index);
                list.WriteForIndex(index, list.ReadForIndex(lar));
                list.WriteForIndex(lar, swap);
                LeonardHeap(list, len, lar);
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