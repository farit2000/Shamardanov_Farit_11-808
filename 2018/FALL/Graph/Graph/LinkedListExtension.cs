using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public static class LinkedListExtesion
    {
        public static void WriteForIndex<T>(this LinkedList<T> list, int index, T data)
        {
            LinkedListNode<T> currentNode = list.First;
            for (int i = 0; i <= index && currentNode != null; i++)
            {
                if (i != index)
                {
                    currentNode = currentNode.Next;
                    continue;
                }
                list.AddAfter(currentNode, data);
                list.Remove(currentNode);
            }
        }

        public static int ReadForIndex(this LinkedList<int> list, int index)
        {
            LinkedListNode<int> currentNode = list.First;
            for (int i = 0; i <= index && currentNode != null; i++)
            {
                if (i != index)
                {
                    currentNode = currentNode.Next;
                    continue;
                }
                return currentNode.Value;
            }
            throw new IndexOutOfRangeException();
        }
    }
}
