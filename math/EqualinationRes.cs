using System.Collections.Generic;

namespace math
{
    public class EqualinationRes
    {
        public EqualinationRes()
        {
            Results = new List<decimal>();
        }

        public EqualinationRes(List<decimal> results)
        {
            Results = results;
        }

        public override string ToString()
        {
            if (cantSolve)
            {
                return "Can't solve this equalination";
            }
            string res = "";
            for (int i = 0; i <= Results.Count - 1; i++)
            {
                if (res != "")
                {
                    res += ", ";
                }
                res += "x" + (i+1).ToString() + " = " + Results[i].ToString();
            }

            return res;
        }

        public List<decimal> Results { get; set; }

        public bool cantSolve { get; set; }
    }
}