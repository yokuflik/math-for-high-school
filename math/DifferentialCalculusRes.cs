using System.Collections.Generic;
using System.Drawing;

namespace math
{
    public class DifferentialCalculusRes
    {
        public DifferentialCalculusRes()
        {
            CutsWithX = new List<PointF>();
            CutWithY = new PointF();

            CuttedFunc = new SmallEqualination();

            MinimumPoints = new List<PointF>();
            MaximumPoints = new List<PointF>();
        }

        public List<PointF> CutsWithX { get; set; }
        public string CutsWithXWay { get; set; }

        public PointF CutWithY { get; set; }

        public List<FuncDomainDefinition> DomainDefinition { get; set; }
        public string DomainDefinitionWay { get; set; }

        public List<decimal> AsymatotsWithX { get; set; }

        public decimal AsymatotWithY { get; set; }
        public string AsymatotWithYWay { get; set; }

        public SmallEqualination CuttedFunc { get; set; }
        public string CuttedFuncAndMinimumMaximumWay { get; set; }

        public List<PointF> MinimumPoints { get; set; }
        public List<PointF> MaximumPoints { get; set; }
    }
}