using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace WindowsFormsApplication4
{
    public partial class Form1 : Form
    {
        int[] list_1 = new int[] {2,10,8,3,5,4};
        int[] list_2 = new int[] {8,10,7,2,6};
        List<int> numbers = new List<int>();

        int count = 0; 
        
          
        public Form1()
        {
            InitializeComponent();

        }

        private int findGCI(int[] list1, int[] list2)
        {
            int gci = 0;

            for (int i = 0; i < list1.Count(); i++)
            {
                for (int j = 0; j < list2.Count(); j++)
                {
                    if (list1[i] == list2[j]){
                       if (list1[i] > gci)
                        gci = list1[i];
                    }
                }
            }
            return gci;
        }

        private void generateRndLists() 
        {
            list_1 = generateRndList(Convert.ToInt32(numericUpDown1.Value));
            list_2 = generateRndList(Convert.ToInt32(numericUpDown2.Value));
        }

        private int[] generateRndList(int rnd)
        {
            int[] rndList = new int[rnd];

            generateNumbers();
            Random rng = new Random();

            for (int i = 0; i < rnd; i++)
            {
                int rdx = rng.Next(numbers.Count);
                rndList[i] = numbers[rdx];
                numbers.RemoveAt(rdx);
            }

                return rndList;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void startBtn_Click(object sender, EventArgs e)
        {

            Stopwatch timer = new Stopwatch();
            List<long> listCPUTimes = new List<long>();
            int loops = Convert.ToInt32(numericUpDown3.Value);
            int n = 0;

            while (n != loops)
            {
                generateRndLists();
                timer.Start();
                int gci = findGCI(list_1, list_2);
                timer.Stop();
                listCPUTimes.Add(timer.ElapsedTicks);

                textBox1.AppendText("\r\nList 1: " + string.Join(",", list_1));
                textBox1.AppendText("\r\nList 2: " + string.Join(",", list_2));


                textBox1.AppendText("\r\nGreatest Common Integer is " + gci);
                textBox1.AppendText("\r\nDuration " + timer.ElapsedTicks + " ticks");
                
                timer.Reset();

                n++;
            }

            double mean = getMean(listCPUTimes);
            double SD = getSD(listCPUTimes);

            textBox1.AppendText("\r\nMean is " + mean);
            textBox1.AppendText("\r\nStandard Deviation is " + SD);

            chart1.ChartAreas[0].AxisY.Maximum = 100;

            chart1.Series.Add("Mean" + count);
            chart1.Series["Mean" + count].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            chart1.Series.Add("SD" + count);
            chart1.Series["SD" + count].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.ErrorBar;

            chart1.Series["Mean" + count].Points.AddXY(count, mean);
            chart1.Series["SD" + count].Points.AddXY(count, mean, mean - SD / 2, mean + SD / 2);
            count++;

        }

        private void generateNumbers()
        {
            for (int i = 0; i < Convert.ToInt32(numericUpDown4.Value); i++)
                numbers.Add(i);
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        public static double getMean(List<long> resultset)
        {
            //ToDo: your implementation
            ulong number = Convert.ToUInt64(resultset.Count);
            ulong x = 0;
            foreach (ulong time in resultset)
            {
                x += time;
            }

            return x / number;
        }

        public static double getSD(List<long> resultSet)
        {
            //ToDo: your implementation
            int v = 0;
            int n = resultSet.Count();
            int m = (int)getMean(resultSet);
            foreach (int x in resultSet)
            {
                v += (x - m) * (x - m);
            }
            return Math.Sqrt(v / n);

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }


    }

}
