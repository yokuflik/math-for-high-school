using System;
using System.Collections.Generic;
using System.Drawing;

namespace math
{
    public class DifferentialCalculusRes
    {
        CalcExercise calc = new CalcExercise();

        public DifferentialCalculusRes()
        {
            CutsWithX = new List<PointF>();
            CutWithY = new PointF();
            
            CuttedFunc = new SmallEqualination();

            MinimumPoints = new List<PointF>();
            MaximumPoints = new List<PointF>();
        }

        public SmallEqualination Func { get; set; }

        public List<PointF> CutsWithX { get; set; }
        public string CutsWithXWay { get; set; }

        public PointF CutWithY { get; set; }

        public List<FuncDomainDefinition> DomainDefinition { get; set; }
        public string DomainDefinitionWay { get; set; }

        public List<decimal> AsymatotsWithX { get; set; }

        public decimal AsymatotWithY { get; set; }
        public bool HasAsymatotWithY { get; set; }
        public string AsymatotWithYWay { get; set; }

        public SmallEqualination CuttedFunc { get; set; }
        public string CuttedFuncAndMinimumMaximumWay { get; set; }

        public List<PointF> MinimumPoints { get; set; }
        public List<PointF> MaximumPoints { get; set; }

        public void setDomainDefinition()
        {
            //sets the domain definition in the right order from small to big
            for (int i = 0; i <= DomainDefinition.Count - 1; i++)
            {
                for (int j = i; j <= DomainDefinition.Count - 1; j++)
                {
                    if (DomainDefinition[j].getSmallX() > DomainDefinition[i].getSmallX())
                    {
                        FuncDomainDefinition fd = DomainDefinition[j];
                        DomainDefinition[j] = DomainDefinition[i];
                        DomainDefinition[i] = fd;
                    }
                }
            }

            //check any part if it is in the others domain definition
            for (int i = 0; i <= DomainDefinition.Count - 1; i++)
            {
                for (int j = 0; j <= DomainDefinition.Count - 1; j++)
                {
                    //if (calc.checkIfIsInTheDomainDefinition(new List<FuncDomainDefinition>{ DomainDefinition[j] })
                }
            }
        }
        
    }
}