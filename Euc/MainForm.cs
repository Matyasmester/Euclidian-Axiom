using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Euc
{
    public partial class MainForm : Form
    {
        private Point[] _3Points = new Point[3];

        private int clicks = 0;

        private int GlobalPointCount = 0;

        public Graphics graphics;

        Pen pen = new Pen(Color.Black) { Width = 6 };

        public MainForm()
        {
            InitializeComponent();
            this.MouseClick += onMouseClick;
            graphics = this.CreateGraphics();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void onMouseClick(object sender, MouseEventArgs e)
        {

            if (clicks >= 3)
            {
                clicks = 0;
            }

            Rectangle rect = new Rectangle(e.Location, new Size(1, 1));

            pen.Color = Color.Red;

            graphics.DrawRectangle(pen, rect);

            pen.Color = Color.Black;

            Point CurrentPoint = e.Location;
            _3Points[clicks] = CurrentPoint;

            if (clicks == 1) CompleteLine();

            if (clicks == 2) DrawParrallelLine();
            clicks++;

            CreateLabelForPoint(CurrentPoint, GlobalPointCount);

            GlobalPointCount++;
        }

        private void CompleteLine()
        {
            graphics.DrawLine(pen, _3Points[0], _3Points[1]);
        }

        private void DrawParrallelLine()
        {
            Point BeginPoint = _3Points[0];
            Point EndPoint = _3Points[1];
            Point ShouldCrossPoint = _3Points[2];

            float AverageYDistanceFromPoint3 = ((BeginPoint.Y - ShouldCrossPoint.Y) + (EndPoint.Y - ShouldCrossPoint.Y)) / 2;
            float AverageXDistanceFromPoint3 = ((BeginPoint.X - ShouldCrossPoint.X) + (EndPoint.X - ShouldCrossPoint.X)) / 2;

            PointF shiftedBegin = new PointF(BeginPoint.X - AverageXDistanceFromPoint3, BeginPoint.Y - AverageYDistanceFromPoint3);
            PointF shiftedEnd = new PointF(EndPoint.X - AverageXDistanceFromPoint3, EndPoint.Y - AverageYDistanceFromPoint3);

            Pen BluePen = new Pen(Color.Blue) { Width = 6 };
            graphics.DrawLine(BluePen, shiftedBegin, shiftedEnd);
        }

        private void CreateLabelForPoint(Point p, int index)
        {
            Point location = new Point(p.X - 5, p.Y - 20);
            Label label = new Label() { Location = location, Text = GetPointName(index), ForeColor = Color.Black, Size = new Size(20, 15)};
            this.Controls.Add(label);
        }

        private string GetPointName(int index)
        {
            int ReducedIndex = index % 26;
            char c = Convert.ToChar(ReducedIndex + 96 + 1);
            int AdditionalNumber = index / (26);
            string s = c.ToString();
            if (AdditionalNumber > 0)
            {
                s += AdditionalNumber;
            }
            return s;
        }
    }

}
