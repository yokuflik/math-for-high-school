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
            if (equalination.Count==0 || equalination[0].Num ==0) //not a func
            {
                return;
            }
            Bitmap bmp = new Bitmap(FuncPicture.Image);
            Graphics g = Graphics.FromImage(bmp);
            //check the y of x every 0.1 points
            float start = -(FuncPicture.Width / SizeOfPoint / 2);
            float end = (FuncPicture.Width / SizeOfPoint / 2);
            PointF last = convertFuncPointToPointOnTheBoard(new PointF(start, (float)putXInEqualination(equalination, start)));
            for (float x = start; x <= end; x += step)
            {
                //put the x in the equalination
                float y = (float)putXInEqualination(equalination, x);
                g.DrawLine(Pens.Black, last, convertFuncPointToPointOnTheBoard(new PointF(x, y)));
                last = convertFuncPointToPointOnTheBoard(new PointF(x, y));
            }
            bmp.RotateFlip(RotateFlipType.Rotate180FlipX);
            FuncPicture.Image = bmp;
        }

        private decimal putXInEqualination(List<Symbol> symbols, double x)
        {
            List<Symbol> symbols2 = new List<Symbol>();
            symbols2.AddRange(symbols);
            for (int i = 0; i <= symbols2.Count - 1; i++)
            {
                if (symbols2[i].Kind == Symbol.SymbolKinds.x)
                {
                    if (symbols2[i].Num < 0)
                    {
                        symbols2[i] = new Symbol(Symbol.SymbolKinds.number, -(decimal)Math.Pow(x * (double)symbols2[i].Num, symbols2[i].Pow));
                    }
                    else
                    {
                        symbols2[i] = new Symbol(Symbol.SymbolKinds.number, (decimal)Math.Pow(x * (double)symbols2[i].Num, symbols2[i].Pow));
                    }
                }
            }
            CalcExercise calc = new CalcExercise();
            return calc.getSmallerEqualinationFromSymbols(symbols2, true).Num;
        }

        private PointF convertFuncPointToPointOnTheBoard(PointF p)
        {
            //(bmp.Width / sizeOfPoint / 2) * sizeOfPoint
            //return new PointF(p.X * SizeOfPoint + FuncPicture.Width / 2, p.Y * SizeOfPoint + FuncPicture.Height / 2);
            return new PointF((p.X + (FuncPicture.Width / SizeOfPoint/ 2)) * (FuncPicture.Width / (FuncPicture.Width / SizeOfPoint)), (p.Y + (FuncPicture.Width / SizeOfPoint / 2)) * (FuncPicture.Width / (FuncPicture.Width / SizeOfPoint)));
        }

        public PictureBox FuncPicture { get; set; }

        public int SizeOfPoint { get; set; }
    }
}