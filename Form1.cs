using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace math
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            draw = new DrawFuncs(funcPictureBox);
            draw.drawBasicTable(zoomTrackBar.Value);
        }

        CalcExercise calc = new CalcExercise();
        DrawFuncs draw;

        string lastFunc = "";
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                lastFunc = textBox1.Text;
                drawTheLastFunc();
                /*calc.error = false;
                EqualinationRes res = calc.calcEqualination(textBox1.Text);
                if (!calc.error)
                {
                    showTheAnswer(res);
                }*/
            }
        }

        private void showTheAnswer(EqualinationRes res)
        {
            MessageBox.Show(res.ToString());
        }

        private void zoomTrackBar_ValueChanged(object sender, EventArgs e)
        {
            drawTheLastFunc();
        }

        private void drawTheLastFunc()
        {
            draw.drawBasicTable(zoomTrackBar.Value);//50*50
            //draw.addToDraw(calc.getSymbolsFromSmallEqualination(calc.getSmallerEqualinationFromText(lastFunc)), (float)0.1);
            calc.error = false;
            SmallEqualination symbols = calc.getSmallerEqualinationFromText(lastFunc);
            if (!calc.error)
            {
                draw.addToDraw(calc.getSymbolsFromSmallEqualination(symbols), (float)0.1);
            }
            else
            {
                lastFunc = "";
                wasAnError();
            }
        }

        private void wasAnError()
        {
            MessageBox.Show("You have an error in your equalination");
        }
    }
}
