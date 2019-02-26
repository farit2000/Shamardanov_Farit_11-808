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
            SmoothSort smoothsort = new SmoothSort();
            StreamReader sr = new StreamReader("C:/Users/farit/Documents/Projects/Graph/mas.txt");
            var rnd = new Random();
            string line;
            var watch = new Stopwatch();
            PointPairList time1 = new PointPairList();
            PointPairList time2 = new PointPairList();
            GraphPane pane = zedGraphControl1.GraphPane;

            pane.Title = "Array vs LinkedList";
            pane.XAxis.Title = "Data weight";
            pane.YAxis.Title = "Tik numbers";
            pane.CurveList.Clear();
            PointPairList f1_list = new PointPairList();
            PointPairList f2_list = new PointPairList();

            while ((line = sr.ReadLine()) != null)
            {
                var test = line.Split().Select(int.Parse).ToArray();
                var linkedList = new LinkedList<int>();
                for (int i = 0; i < test.Length; i++)
                {
                    linkedList.AddLast(test[i]);
                }
                watch.Stop();
                watch.Restart();
                smoothsort.Sort(test);
                watch.Stop();
                time1.Add(test.Length, watch.ElapsedTicks);

                watch.Stop();
                watch.Restart();
                smoothsort.Sort(linkedList);
                watch.Stop();
                time2.Add(linkedList.Count, watch.ElapsedTicks);
            }
                LineItem f1_curve = pane.AddCurve("Array", time1, Color.Blue, SymbolType.Circle);
                LineItem f2_curve = pane.AddCurve("Lin", time2, Color.Red, SymbolType.Plus);
                zedGraphControl1.AxisChange();
                zedGraphControl1.Invalidate();
        }

        private void Setsize()
        {
            zedGraphControl1.Location = new Point(10,10);

            zedGraphControl1.Size = new Size(this.ClientRectangle.Width - 20, this.ClientRectangle.Height - 20);
        }
    }
}
