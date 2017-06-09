#region

using System;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace Simulate
{
    public partial class Form1 : Form
    {
        private Memory _x = new Memory {X = 20, Y = 250, BarWidth = 500, BarHeight = 40};

        public Form1()
        {
            InitializeComponent();
            panel1.Paint += Drawer;
            numericUpDown1.Maximum = 1200;
            comboBox1.Items.AddRange(
                new[]
                {
                    MangerType.FirstFit.ToString(),
                    MangerType.FirstFit2.ToString(),
                    MangerType.BestFit.ToString(),
                    MangerType.LastFit.ToString(),
                    MangerType.NextFit.ToString()
                });
        }

        private void Drawer(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString("1200KB", new Font("Verdana", 13, FontStyle.Regular), Brushes.Black, 50, 230);
            e.Graphics.DrawRectangle(Pens.Black, new Rectangle(_x.X, _x.Y, _x.BarWidth, _x.BarHeight));
            _x.DrawBricks(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var w = MangerType.FirstFit2;
            switch (comboBox1.SelectedItem.ToString())
            {
                case "FirstFit":
                    w = MangerType.FirstFit;
                    break;
                case "FirstFit2":
                    w = MangerType.FirstFit2;
                    break;
                case "BestFit":
                    w = MangerType.BestFit;
                    break;
                case "LastFit":
                    w = MangerType.LastFit;
                    break;
                case "NextFit":
                    w = MangerType.NextFit;
                    break;
            }
            _x.AssignMemory((int) numericUpDown1.Value, w);
            panel1.Invalidate();
            using (var g = new Timer())
            {
                g.Tick += (x, r) =>
                {
                    /*animation to be implemented*/
                };
                g.Interval = 50;
                g.Start();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _x = new Memory {X = 20, Y = 250, BarWidth = 500, BarHeight = 40};
            panel1.Invalidate();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _x = new Memory {X = 20, Y = 250, BarWidth = 500, BarHeight = 40};
            panel1.Invalidate();
        }
    }
}