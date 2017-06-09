using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Simulate
{
    internal class Memory
    {
        private readonly List<Block> _blocks;

        public Memory()
        {
            Size = 1200;
            _blocks = new List<Block>
            {
                new Block {Free = 190, PercentFilled = 0},
                new Block {Free = 210, PercentFilled = 0},
                new Block {Free = 190, PercentFilled = 0},
                new Block {Free = 210, PercentFilled = 0},
                new Block {Free = 190, PercentFilled = 0},
                new Block {Free = 210, PercentFilled = 0}
            };
        }

        public int X { get; set; }
        public int Y { get; set; }
        public long Size { get; set; }
        public int BarWidth { get; set; }
        public int BarHeight { get; set; }

        public void AssignMemory(int size, MangerType type)
        {
            Block x;
            switch (type)
            {
                case MangerType.FirstFit:
                    x = _blocks
                        .FirstOrDefault(t => t.Free >= size);
                    if (x != null)
                    {
                        x.Free -= size;
                        x.Used += size;
                        x.PercentFilled = x.Used / Size * 500;
                    }
                    else
                        MessageBox.Show(@"No Memory Fit Available" + Environment.NewLine + @"Maybe Fragmentation");
                    break;
                case MangerType.FirstFit2:
                    x = _blocks
                        .Where(t => !t.Touched)
                        .FirstOrDefault(t => t.Free >= size);
                    if (x != null)
                    {
                        x.Free -= size;
                        x.Used += size;
                        x.PercentFilled = x.Used / Size * 500;
                        x.Touched = !x.Touched;
                    }
                    else
                        MessageBox.Show(@"No Memory Fit Available" + Environment.NewLine + @"Maybe Fragmentation");
                    break;
                case MangerType.BestFit:
                    x = _blocks.Where(t => !t.Touched)
                        .Where(t => t.Free >= size)
                        .OrderBy(t => t.Free)
                        .FirstOrDefault();
                    if (x != null)
                    {
                        x.Free -= size;
                        x.Used += size;
                        x.PercentFilled = x.Used / Size * 500;
                        x.Touched = !x.Touched;
                    }
                    else
                        MessageBox.Show(@"No Memory Fit Available" + Environment.NewLine + "Maybe Fragmentation");
                    break;
                case MangerType.LastFit:
                    x = _blocks
                        .OrderByDescending(t => t.Index)
                        .Where(t => !t.Touched).FirstOrDefault(t => t.Free >= size);
                    if (x != null)
                    {
                        x.Free -= size;
                        x.Used += size;
                        x.PercentFilled = x.Used / Size * 500;
                        x.Touched = !x.Touched;
                    }
                    break;
                case MangerType.NextFit:
                    break;
            }
        }

        public void DrawBricks(PaintEventArgs e)
        {
            float x = X;
            foreach (var item in _blocks)
            {
                e.Graphics.FillRectangle(Brushes.Fuchsia,
                    new RectangleF(x, Y, (item.Free + item.Used) / Size * 500 - 1, BarHeight));
                e.Graphics.FillRectangle(Brushes.Bisque, new RectangleF(x, Y, item.PercentFilled - 1, 40));
                e.Graphics.DrawString(item.Used + "KB Used", new Font("Arial", 08f, FontStyle.Regular), Brushes.Black,
                    x, Y + 50);
                e.Graphics.DrawString(item.Free + "KB Free", new Font("Arial", 08f, FontStyle.Regular), Brushes.Black,
                    x, Y + 60);
                x += (item.Free + item.Used) / Size * 500;
            }
        }
    }
}