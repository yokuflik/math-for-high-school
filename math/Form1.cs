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
            draw.drawBasicTable(zoomTrackBar.Value*10);

            /*List<FuncDomainDefinition> domainDefinition = new List<FuncDomainDefinition>();
            domainDefinition.Add(new FuncDomainDefinition(FuncDomainDefinition.DomainDefinitionKinds.xBetweenThen, FuncDomainDefinition.XBetweenKinds.notEquals, 3, 5));//x<3
            domainDefinition.Add(new FuncDomainDefinition(FuncDomainDefinition.DomainDefinitionKinds.xBigThen, 6));//x>5
            DifferentialCalculusRes dcr = new DifferentialCalculusRes();
            dcr.DomainDefinition = domainDefinition;
            dcr.setDomainDefinition();
            List<decimal> xes = differentialCalculus.getXFromAnyDomain(domainDefinition);

            string res = "";
            for (int i = 0; i <= dcr.DomainDefinition.Count - 1; i++)
            {
                res += dcr.DomainDefinition[i].ToString()+", ";
            }*/
            //MessageBox.Show(res);
            string way = "";
            SmallEqualination small = calc.getSmallerEqualinationFromText("1/root(x, 2)-1", ref way);
            EqualinationRes res = calc.calcRegularEqualinationFromSmallEqualinations(small, new SmallEqualination(), ref way);
            //MessageBox.Show(res.ToString());
        }

        CalcExercise calc = new CalcExercise();
        DrawFuncs draw;
        DifferentialCalculus differentialCalculus = new DifferentialCalculus();

        FuncsViewAndDrawPanel funcsViewAndDrawPanel;

        string lastFunc = "";
        DifferentialCalculusRes diffLastFunc = new DifferentialCalculusRes();
        
        private void calcButton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                lastFunc = textBox1.Text;

                diffLastFunc =  findAllPointsInFuncs();

                drawTheLastFunc();
                /*calc.error = false;
                EqualinationRes res = calc.calcEqualination(textBox1.Text);
                if (!calc.error)
                {
                    showTheAnswer(res);
                }*/
            }
        }

        private DifferentialCalculusRes findAllPointsInFuncs()
        {
            string way = "";
            SmallEqualination smallEqualination = calc.getSmallerEqualinationFromText(lastFunc, ref way);
            DifferentialCalculusRes res = differentialCalculus.getAllPointsInFunc(smallEqualination);

            //putt the points in the place
            CutsTheXLabel.Text = getStringfromListOfPoints(res.CutsWithX);
            CutsWithXWayLabel.Text = res.CutsWithXWay+"\n";
            if (res.CutWithY.IsEmpty)
            {
                CutsTheYLabel.Text = "None";
            }
            else
            {
                CutsTheYLabel.Text = calc.pointFToString(res.CutWithY);
            }

            DomainDefinitionLabel.Text = getStringFromDomainDefinition(res.DomainDefinition);
            DomainDefinitionWayLabel.Text = res.DomainDefinitionWay;

            AsymatotsWithXLabel.Text = getStringFromAsymatotsWithX(res.AsymatotsWithX);

            if (res.HasAsymatotWithY)
            {
                AsymatotWithYLabel.Text = "y = " + calc.numberToString(res.AsymatotWithY);
            }
            else
            {
                AsymatotWithYLabel.Text = "None";
            }
            AsymatotWithYWayLabel.Text = res.AsymatotWithYWay;

            CuttedFuncLabel.Text = res.CuttedFunc.ToString();
            MinimumPointsLabel.Text = getStringfromListOfPoints(res.MinimumPoints);
            MaximumPointsLabel.Text = getStringfromListOfPoints(res.MaximumPoints);
            minAndMaxWayLabel.Text = res.CuttedFuncAndMinimumMaximumWay+"\n";
            return res;
        }

        private string getStringFromAsymatotsWithX(List<decimal> asymatotsWithX)
        {
            if (asymatotsWithX.Count == 0)
            {
                return "None";
            }
            string res = "";
            for (int i = 0; i <= asymatotsWithX.Count - 1; i++)
            {
                if (res != "")
                {
                    res += ", ";
                }
                res += "x = " + calc.numberToString(asymatotsWithX[i]);
            }
            return res;
        }

        private string getStringFromDomainDefinition(List<FuncDomainDefinition> domainDefinition)
        {
            string res = "";
            for (int i = 0; i <= domainDefinition.Count - 1; i++)
            {
                if (res != "")
                {
                    res += ", ";
                }
                res += domainDefinition[i].ToString();
            }
            return res;
        }

        private string getStringfromListOfPoints(List<PointF> points)
        {
            if (points.Count == 0)
            {
                return "None";
            }
            string res = "";
            for (int i = 0; i <= points.Count - 1; i++)
            {
                if (res != "")
                {
                    res += ", ";
                }
                //res += "(" + points[i].X.ToString("0.000") +", "+ points[i].Y.ToString("0.000") + ")";
                //res += "(" + string.Format("{0:0.####}", points[i].X) + ", "+ string.Format("{0:0.####}", points[i].Y) + ")";
                //res += points[i].ToString();
                res += calc.pointFToString(points[i]);
            }
            return res;
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
            draw.drawBasicTable(zoomTrackBar.Value*10);//50*50
            //draw.addToDraw(calc.getSymbolsFromSmallEqualination(calc.getSmallerEqualinationFromText(lastFunc)), (float)0.1);
            calc.error = false;
            string way = "";
            SmallEqualination symbols = calc.getSmallerEqualinationFromText(lastFunc, ref way);
            if (!calc.error)
            {
                draw.addToDraw(symbols.getSymbols(), (float)0.1);
                draw.addAsymatotsToDraw(diffLastFunc.AsymatotWithY, diffLastFunc.AsymatotsWithX, diffLastFunc.HasAsymatotWithY);
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

        private void showWayCutsWithXButton_Click(object sender, EventArgs e)
        {
            showAndHideWay(CutsWithXWayLabel, showWayCutsWithXButton, resultsPanel);
        }

        private void showWayCuttecdFuncButton_Click(object sender, EventArgs e)
        {
            showAndHideWay(minAndMaxWayLabel, showWayCuttecdFuncButton, resultsPanel);
        }

        private void DomainDefintionShowWayButton_Click(object sender, EventArgs e)
        {
            showAndHideWay(DomainDefinitionWayLabel, DomainDefintionShowWayButton, resultsPanel);
        }

        private void AsymatotsWithYShowWayButton_Click(object sender, EventArgs e)
        {
            showAndHideWay(AsymatotWithYWayLabel, AsymatotsWithYShowWayButton, resultsPanel);
        }

        private void showAndHideWay(Label way, PictureBox button, Panel p)
        {
            if (way.Text != "")
            {
                if (way.Visible)
                {
                    //get the controls back to the last way
                    moveAllControlsDown(p, -way.Size.Height + 5, button.Bottom - 20);
                    way.Visible = false;
                }
                else
                {
                    moveAllControlsDown(p, way.Size.Height - 5, button.Bottom - 20);
                    way.Location = new Point(button.Left + 35, button.Bottom);
                    way.Visible = true;
                }
            }
        }

        private void moveAllControlsDown(Panel panel, int height, int end)
        {
            for (int i = 0; i <= panel.Controls.Count - 1; i++)
            {
                Control l = panel.Controls[i];
                if (l.Top > end)
                {
                    l.Top += height;
                }
            }
        }

    }
}
