using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    internal class SmoothSort
    {
        static int q, r, p, b, c, r1, b1, c1;
        //Comparator function
        static bool IsAscending(int A, int B)
        {
            return A < B;
        }
        //Perform an "Up" operation on the actual number
        static void Up(ref int IA, ref int IB, ref int temp)
        {
            temp = IA;
            IA += IB + 1;
            IB = temp;
        }
        //Perform a "Down" operation on the actual number
        private static void Down(ref int IA, ref int IB, ref int temp)
        {
            temp = IB;
            IB = IA - IB - 1;
            IA = temp;
        }
        //Sifts up the root of the stretch in question
        static void Sift(ref LinkedList<int> A)
        {
            int r0, r2, temp = 0;
            int t;
            r0 = r1;
            t = A.ReadForIndex(r0);
            while (b1 >= 3)
            {
                r2 = r1 - b1 + c1;
                if (!IsAscending(A.ReadForIndex(r1 - 1), A.ReadForIndex(r2)))
                {
                    r2 = r1 - 1;
                    Down(ref b1, ref c1, ref temp);
                }
                if (IsAscending(A.ReadForIndex(r2), t))
                    b1 = 1;
                else
                {
                    A.WriteForIndex(r1, A.ReadForIndex(r2));
                    r1 = r2;
                    Down(ref b1, ref c1, ref temp);
                }
            }
            if (Convert.ToBoolean(r1 - r0))
                A.WriteForIndex(r1, t);
        }
        //Sifts up the root of the stretch in question
        static void Sift(ref int[] A)
        {
            int r0, r2, temp = 0;
            int t;
            r0 = r1;
            t = A[r0];
            while (b1 >= 3)
            {
                r2 = r1 - b1 + c1;
                if (!IsAscending(A[r1 - 1], A[r2]))
                {
                    r2 = r1 - 1;
                    Down(ref b1, ref c1, ref temp);
                }
                if (IsAscending(A[r2], t))
                    b1 = 1;
                else
                {
                    A[r1] = A[r2];
                    r1 = r2;
                    Down(ref b1, ref c1, ref temp);
                }
            }
            if (Convert.ToBoolean(r1 - r0))
                A[r1] = t;
        }
        //Trinkles the roots of the stretches of a given LinkedList and root
        static void Trinkle(ref LinkedList<int> A)
        {
            int p1, r2, r3, r0, temp = 0;
            int t;
            p1 = p;
            b1 = b;
            c1 = c;
            r0 = r1;
            t = A.ReadForIndex(r0);
            while (p1 > 0)
            {
                while ((p1 & 1) == 0)
                {
                    p1 >>= 1;
                    Up(ref b1, ref c1, ref temp);
                }
                r3 = r1 - b1;
                if ((p1 == 1) || IsAscending(A.ReadForIndex(r3), t))
                    p1 = 0;
                else
                {
                    --p1;
                    if (b1 == 1)
                    {
                        A.WriteForIndex(r1, A.ReadForIndex(r3));
                        r1 = r3;
                    }
                    else
                    {
                        if (b1 >= 3)
                        {
                            r2 = r1 - b1 + c1;
                            if (!IsAscending(A.ReadForIndex(r1 - 1), A.ReadForIndex(r2)))
                            {
                                r2 = r1 - 1;
                                Down(ref b1, ref c1, ref temp);
                                p1 <<= 1;
                            }
                            if (IsAscending(A.ReadForIndex(r2), A.ReadForIndex(r3)))
                            {
                                A.WriteForIndex(r1, A.ReadForIndex(r3));
                                r1 = r3;
                            }
                            else
                            {
                                A.WriteForIndex(r1, A.ReadForIndex(r2));
                                r1 = r2;
                                Down(ref b1, ref c1, ref temp);
                                p1 = 0;
                            }
                        }
                    }
                }
            }
            if (Convert.ToBoolean(r0 - r1))
                A.WriteForIndex(r1, t);
            Sift(ref A);
        }
        //Trinkles the roots of the stretches of a given LinbkedList and root when the adjacent stretches are trusty
        static void SemiTrinkle(ref LinkedList<int> A)
        {
            int T;
            r1 = r - c;
            if (!IsAscending(A.ReadForIndex(r1), A.ReadForIndex(r)))
            {
                T = A.ReadForIndex(r);
                A.WriteForIndex(r, A.ReadForIndex(r1));
                A.WriteForIndex(r1, T);
                Trinkle(ref A);
            }
        }
        //Trinkles the roots of the stretches of a given array and root
        static void Trinkle(ref int[] A)
        {
            int p1, r2, r3, r0, temp = 0;
            int t;
            p1 = p;
            b1 = b;
            c1 = c;
            r0 = r1;
            t = A[r0];
            while (p1 > 0)
            {
                while ((p1 & 1) == 0)
                {
                    p1 >>= 1;
                    Up(ref b1, ref c1, ref temp);
                }
                r3 = r1 - b1;
                if ((p1 == 1) || IsAscending(A[r3], t))
                    p1 = 0;
                else
                {
                    --p1;
                    if (b1 == 1)
                    {
                        A[r1] = A[r3];
                        r1 = r3;
                    }
                    else
                    {
                        if (b1 >= 3)
                        {
                            r2 = r1 - b1 + c1;
                            if (!IsAscending(A[r1 - 1], A[r2]))
                            {
                                r2 = r1 - 1;
                                Down(ref b1, ref c1, ref temp);
                                p1 <<= 1;
                            }
                            if (IsAscending(A[r2], A[r3]))
                            {
                                A[r1] = A[r3];
                                r1 = r3;
                            }
                            else
                            {
                                A[r1] = A[r2];
                                r1 = r2;
                                Down(ref b1, ref c1, ref temp);
                                p1 = 0;
                            }
                        }
                    }
                }
            }
            if (Convert.ToBoolean(r0 - r1))
                A[r1] = t;
            Sift(ref A);
        }
        //Trinkles the roots of the stretches of a given array and root when the adjacent stretches are trusty
        static void SemiTrinkle(ref int[] A)
        {
            int T;
            r1 = r - c;
            if (!IsAscending(A[r1], A[r]))
            {
                T = A[r];
                A[r] = A[r1];
                A[r1] = T;
                Trinkle(ref A);
            }
        }
        //The main SmoothSort funktion for array
        public void Sort(int[] array)
        {
            //Start of main function
            int temp = 0;
            q = 1;
            r = 0;
            p = 1;
            b = 1;
            c = 1;
            //building the tree
            while (q < array.Length)
            {
                r1 = r;
                if ((p & 7) == 3)
                {
                    b1 = b;
                    c1 = c;
                    Sift(ref array);
                    p = (p + 1) >> 2;
                    Up(ref b, ref c, ref temp);
                    Up(ref b, ref c, ref temp);
                }
                else if ((p & 3) == 1)
                {
                    if (q + c < array.Length)
                    {
                        b1 = b;
                        c1 = c;
                        Sift(ref array);
                    }
                    else
                        Trinkle(ref array);
                    Down(ref b, ref c, ref temp);
                    p <<= 1;
                    while (b > 1)
                    {
                        Down(ref b, ref c, ref temp);
                        p <<= 1;
                    }
                    ++p;
                }
                ++q;
                ++r;
            }
            r1 = r;
            Trinkle(ref array);
            //building the sorted array
            while (q > 1)
            {
                --q;
                if (b == 1)
                {
                    --r;
                    --p;
                    while ((p & 1) == 0)
                    {
                        p >>= 1;
                        Up(ref b, ref c, ref temp);
                    }
                }
                else
                {
                    if (b >= 3)
                    {
                        --p;
                        r = r - b + c;
                        if (p > 0)
                            SemiTrinkle(ref array);
                        Down(ref b, ref c, ref temp);
                        p = (p << 1) + 1;
                        r = r + c;
                        SemiTrinkle(ref array);
                        Down(ref b, ref c, ref temp);
                        p = (p << 1) + 1;
                    }
                }
            }
            /*
             * element q is done
             * element 0 is done
             */
        }
        //The main SmoothSort funktion for LinkedList
        public void Sort(LinkedList<int> array)
        {
            //Start of main function
            int temp = 0;
            q = 1;
            r = 0;
            p = 1;
            b = 1;
            c = 1;
            //building the tree
            while (q < array.Count)
            {
                r1 = r;
                if ((p & 7) == 3)
                {
                    b1 = b;
                    c1 = c;
                    Sift(ref array);
                    p = (p + 1) >> 2;
                    Up(ref b, ref c, ref temp);
                    Up(ref b, ref c, ref temp);
                }
                else if ((p & 3) == 1)
                {
                    if (q + c < array.Count)
                    {
                        b1 = b;
                        c1 = c;
                        Sift(ref array);
                    }
                    else
                        Trinkle(ref array);
                    Down(ref b, ref c, ref temp);
                    p <<= 1;
                    while (b > 1)
                    {
                        Down(ref b, ref c, ref temp);
                        p <<= 1;
                    }
                    ++p;
                }
                ++q;
                ++r;
            }
            r1 = r;
            Trinkle(ref array);
            //building the sorted LinkedList
            while (q > 1)
            {
                --q;
                if (b == 1)
                {
                    --r;
                    --p;
                    while ((p & 1) == 0)
                    {
                        p >>= 1;
                        Up(ref b, ref c, ref temp);
                    }
                }
                else
                {
                    if (b >= 3)
                    {
                        --p;
                        r = r - b + c;
                        if (p > 0)
                            SemiTrinkle(ref array);
                        Down(ref b, ref c, ref temp);
                        p = (p << 1) + 1;
                        r = r + c;
                        SemiTrinkle(ref array);
                        Down(ref b, ref c, ref temp);
                        p = (p << 1) + 1;
                    }
                }
            }
            /*
             * element q is done
             * element 0 is done
             */
        }
    }
}
