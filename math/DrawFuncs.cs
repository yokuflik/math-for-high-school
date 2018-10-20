using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace math
{
    internal class DrawFuncs
    {
        public DrawFuncs(PictureBox pictureBox)
        {
            FuncPicture = pictureBox;
        }

        public void drawBasicTable(int sizeOfPoint)
        {
            SizeOfPoint = sizeOfPoint;

            Bitmap bmp = new Bitmap(FuncPicture.Width, FuncPicture.Height);
            Graphics g = Graphics.FromImage(bmp);
            Pen pen = new Pen(Color.FromArgb(70, 100, 100, 100), 1);
            for (int x = sizeOfPoint; x <= bmp.Width-1; x += sizeOfPoint) //draw the top to down lines
            {
                g.DrawLine(pen, x, 0, x, bmp.Height - 1);
            }
            for (int y = sizeOfPoint; y <= bmp.Height-1; y += sizeOfPoint) //draw the right to left lines
            {
                g.DrawLine(pen, 0, y, bmp.Width - 1, y);
            }

            //draw the big x and y lines
            g.DrawLine(Pens.Black, (bmp.Width / sizeOfPoint / 2) * sizeOfPoint, 0, (bmp.Width / sizeOfPoint / 2) * sizeOfPoint, bmp.Height - 1);
            g.DrawLine(Pens.Black, 0, (bmp.Height / sizeOfPoint / 2) * sizeOfPoint, bmp.Width - 1, (bmp.Height / sizeOfPoint / 2) * sizeOfPoint);

            FuncPicture.Image = bmp;
        }

        public void addToDraw(List<Symbol> equalination, float step)
        {
            if (equalination.Count==0) //not a func
            {
                return;
            }
            Bitmap bmp = new Bitmap(FuncPicture.Image);
            Graphics g = Graphics.FromImage(bmp);
            CalcExercise calc = new CalcExercise();
            //check the y of x every 0.1 points
            float start = -(FuncPicture.Width / SizeOfPoint / 2);
            float end = (FuncPicture.Width / SizeOfPoint / 2);
            string way = "";
            PointF last = convertFuncPointToPointOnTheBoard(new PointF(start, (float)calc.putXInEqualination(equalination, start, ref way)));
            for (float x = start; x <= end; x += step)
            {
                try
                {
                    //put the x in the equalination
                    float y = (float)calc.putXInEqualination(equalination, x, ref way);
                    PointF p = convertFuncPointToPointOnTheBoard(new PointF(x, y));
                    if (p.Y < FuncPicture.Height + 1000 && p.Y > -1000 && !calc.error)
                    {
                        g.DrawLine(Pens.Black, last, p);
                    }
                    last = p;
                }
                catch
                {

                }
                if (x > 2)
                {

                }
            }
            bmp.RotateFlip(RotateFlipType.Rotate180FlipX);
            FuncPicture.Image = bmp;
        }
        
        public void addAsymatotsToDraw(decimal asymatotWithY, List<decimal> asymatotsWithX, bool hasAsymatotWithY)
        {
            Bitmap bmp = new Bitmap(FuncPicture.Image);
            Graphics g = Graphics.FromImage(bmp);
            Pen p = new Pen(Color.FromArgb(130,Color.Blue), 1);
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            //draw the asymatot with y
            if (hasAsymatotWithY)
            {
                g.DrawLine(p, 0, convertFuncPointToPointOnTheBoard(new PointF(0, -(float)asymatotWithY)).Y, bmp.Width - 1, convertFuncPointToPointOnTheBoard(new PointF(bmp.Width - 1, -(float)asymatotWithY)).Y);
            }

            for (int i = 0; i <= asymatotsWithX.Count - 1; i++)
            {
                g.DrawLine(p, convertFuncPointToPointOnTheBoard(new PointF((float)asymatotsWithX[i], 0)).X, 0, convertFuncPointToPointOnTheBoard(new PointF((float)asymatotsWithX[i], 0)).X, bmp.Height-1);
            }
            FuncPicture.Image = bmp;
        }

        private PointF convertFuncPointToPointOnTheBoard(PointF p)
        {
            return new PointF((p.X + (FuncPicture.Width / SizeOfPoint/ 2)) * (FuncPicture.Width / (FuncPicture.Width / SizeOfPoint)), (p.Y + (FuncPicture.Width / SizeOfPoint / 2)) * (FuncPicture.Width / (FuncPicture.Width / SizeOfPoint)));
        }
        
        public PictureBox FuncPicture { get; set; }

        public int SizeOfPoint { get; set; }
    }
}