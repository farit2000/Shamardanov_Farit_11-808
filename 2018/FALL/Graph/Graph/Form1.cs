using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace Graph
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DrawGraph();
        }

        private void DrawGraph()
        {
            //SmoothSort smoothSort = new SmoothSort();
            SmoothSort2 smoothSort = new SmoothSort2();
            //файл с рандомными массивами
            StreamReader sr = new StreamReader("C:/Users/farit/Documents/Projects/Graph/mas.txt");
            //файл с отсортированными массивами
            StreamReader srSort = new StreamReader("C:/Users/farit/Documents/Projects/Graph/truemas.txt");
            //файл с убывающим массивом
            StreamReader srUnsort = new StreamReader("C:/Users/farit/Documents/Projects/Graph/AbsolutlyAnsortedMas.txt");

            var rnd = new Random();
            string firstLine;
            string secondLine;
            string thirdLine;
            var watch = new Stopwatch();
            PointPairList time1 = new PointPairList();
            PointPairList time2 = new PointPairList();
            PointPairList time3 = new PointPairList();
            PointPairList time4 = new PointPairList();
            PointPairList time5 = new PointPairList();
            PointPairList time6 = new PointPairList();
            PointPairList newTimeArrayRandom = new PointPairList();
            PointPairList newTimeListRandom = new PointPairList();
            GraphPane pane = zedGraphControl1.GraphPane;

            pane.Title = "List vs SortList";
            pane.XAxis.Title = "Data weight";
            pane.YAxis.Title = "Tik numbers";
            pane.CurveList.Clear();

            GC.Collect();
            //записываем кординаты для рандомного массива
            while ((firstLine = sr.ReadLine()) != null)
            {
                var test = firstLine.Split().Select(int.Parse).ToArray();
                var linkedList = new LinkedList<int>();
                for (int i = 0; i < test.Length; i++)
                {
                    linkedList.AddLast(test[i]);
                }

                linkedList.Average();

                watch.Restart();
                smoothSort.Sort(test);
                watch.Stop();
                time1.Add(test.Length, watch.ElapsedTicks);

                watch.Restart();
                smoothSort.Sort(linkedList);
                watch.Stop();
                time2.Add(linkedList.Count, watch.ElapsedTicks);
            }
            //скидываем указатель в начало
            sr.BaseStream.Position = 0;
            GC.Collect();
            //запичываем точки для отсортированного массива
            while ((secondLine = srSort.ReadLine()) != null)
            {
                var test = secondLine.Split().Select(int.Parse).ToArray();
                var linkedList = new LinkedList<int>();
                for (int i = 0; i < test.Length; i++)
                {
                    linkedList.AddLast(test[i]);
                }

                linkedList.Average();
                watch.Restart();
                smoothSort.Sort(test);
                watch.Stop();
                newTimeArrayRandom.Add(test.Length, watch.ElapsedTicks);

                watch.Restart();
                smoothSort.Sort(linkedList);
                watch.Stop();
                newTimeListRandom.Add(linkedList.Count, watch.ElapsedTicks);
            }
            srSort.BaseStream.Position = 0;
            GC.Collect();
            //записываем точки для убывающего массива
            while ((thirdLine = srUnsort.ReadLine()) != null)
            {
                var test = thirdLine.Split().Select(int.Parse).ToArray();
                var linkedList = new LinkedList<int>();
                for (int i = 0; i < test.Length; i++)
                {
                    linkedList.AddLast(test[i]);
                }

                linkedList.Average();
                watch.Restart();
                smoothSort.Sort(test);
                watch.Stop();
                time5.Add(test.Length, watch.ElapsedTicks);

                watch.Restart();
                smoothSort.Sort(linkedList);
                watch.Stop();
                time6.Add(linkedList.Count, watch.ElapsedTicks);
            }
            srUnsort.DiscardBufferedData();
            //начинаем вывод графиков
            LineItem f1_curve = pane.AddCurve("UnsortedArray", time1, Color.Blue, SymbolType.Circle);
            LineItem f2_curve = pane.AddCurve("LinkedList", time2, Color.Red, SymbolType.Plus);
            LineItem f3_curve = pane.AddCurve("Array", time3, Color.Green, SymbolType.Circle);
            LineItem f4_curve = pane.AddCurve("UnsortedList", time4, Color.BurlyWood, SymbolType.Circle);
            LineItem f5_curve = pane.AddCurve("SortedArray", time5, Color.DarkOrange, SymbolType.Circle);
            LineItem f6_curve = pane.AddCurve("SortedList", time6, Color.Blue, SymbolType.Circle);
            LineItem f7_curve = pane.AddCurve("ArrayNewSort", newTimeArrayRandom, Color.Blue, SymbolType.Circle);
            LineItem f8_curve = pane.AddCurve("ListNewSort", newTimeListRandom, Color.Yellow, SymbolType.Circle);
            // применяем сглаживание графиков
            f1_curve.Line.IsSmooth = true;
            f1_curve.Line.SmoothTension = 0.5F;
            f2_curve.Line.IsSmooth = true;
            f2_curve.Line.SmoothTension = 0.5F;
            f3_curve.Line.IsSmooth = true;
            f3_curve.Line.SmoothTension = 0.5F;
            f4_curve.Line.IsSmooth = true;
            f4_curve.Line.SmoothTension = 0.5F;
            f5_curve.Line.IsSmooth = true;
            f5_curve.Line.SmoothTension = 0.5F;
            f6_curve.Line.IsSmooth = true;
            f6_curve.Line.SmoothTension = 0.5F;
            f7_curve.Line.IsSmooth = true;
            f7_curve.Line.SmoothTension = 0.5F;
            f8_curve.Line.IsSmooth = true;
            f8_curve.Line.SmoothTension = 0.5F;
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }

        private void Setsize()
        {
            zedGraphControl1.Location = new Point(10, 10);
            zedGraphControl1.Size = new Size(this.ClientRectangle.Width - 20, this.ClientRectangle.Height - 20);
        }
    }
}
