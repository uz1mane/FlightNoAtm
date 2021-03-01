using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flight
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            btStart.Enabled = true;
            btPause.Enabled = false;

            btStart.Text = "Старт";
            btPause.Text = "Пауза";
        }

        const double dt = 0.01;
        const double g = 9.81;

        double a;
        double v0;
        double y0;

        double t;
        double x;
        double y;

        double l;
        double h;

        bool isLive = false;
        private void btStart_Click(object sender, EventArgs e)
        {
            a = (double)edAngle.Value;
            v0 = (double)edSpeed.Value;
            y0 = (double)edHeight.Value;

            t = 0;
            x = 0;
            y = y0;

            l = (Math.Pow(v0, 2) * Math.Sin(2 * a * Math.PI / 180)) / g;
            h = (Math.Pow(v0, 2) * Math.Pow(Math.Sin(a * Math.PI / 180), 2)) / (2 * g);

            if (l > h)
            {
                chart1.ChartAreas[0].AxisX.Maximum = (int)l + 1;
                chart1.ChartAreas[0].AxisY.Maximum = (int)l + 1;
            }
            else
            {
                chart1.ChartAreas[0].AxisX.Maximum = (int)h + 1;
                chart1.ChartAreas[0].AxisY.Maximum = (int)h + 1;
            }

            chart1.Series[0].Points.Clear();
            chart1.Series[0].Points.AddXY(x, y);

            isLive = true;
            btStart.Text = "Рестарт";
            btPause.Text = "Пауза";

            if (isLive)
            {
                timer1.Start();
                btPause.Enabled = true;
            }
            else
                return;
                
        }

        private void btPause_Click(object sender, EventArgs e)
        {
            if (isLive)
            {
                isLive = false;
                btPause.Text = "Продолжить";
            }
            else
            {
                isLive = true;
                btPause.Text = "Пауза";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbTime.Text = "Время : " + Math.Round(t, 2).ToString();
            if (isLive)
            {
                t += dt;
                x = v0 * Math.Cos(a * Math.PI / 180) * t;
                y = y0 + v0 * Math.Sin(a * Math.PI / 180) * t - g * t * t / 2;
                chart1.Series[0].Points.AddXY(x, y);
                if (y <= 0) timer1.Stop();
            }
            else
                return;
        }

    }
}
