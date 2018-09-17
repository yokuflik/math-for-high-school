using System.Collections.Generic;

namespace math
{
    public class SmallEqualination: EqualinationRes
    {
        public SmallEqualination()
        {
            Num = 0;
            XList = new List<decimal>();
        }

        public SmallEqualination(decimal num, List<decimal> xList)
        {
            Num = num;
            XList = xList;
        }

        public override string ToString()
        {
            string res = "";

            //add all the x powers
            if (XList.Count != 0)
            {
                for (int i = XList.Count - 1; i >= 1; i--)
                {
                    string xString = getStringFromX(XList[i], i + 1, true);
                    if (xString != "")
                    {
                        addToResNumber(ref res, xString);
                    }
                }

                //add x power one
                string XString = getStringFromX(XList[0], 1, false);
                if (XString != "")
                {
                    addToResNumber(ref res, XString);
                }
            }

            //add the number
            if (Num != 0)
            {
                addToResNumber(ref res, Num.ToString());
            }

            return res;
        }

        private void addToResNumber(ref string res, string add)
        {
            if (res != "")
            {
                res += " + ";
            }
            res += add;
        }

        private string getStringFromX(decimal x, int power, bool putPower)
        {
            string res = "";
            if (x == 1)
            {
                res += "x";
                if (putPower)
                {
                    res += "^" + power.ToString();
                }
            }
            else if (x == 0)
            {
                return res;
            }
            else
            {
                res += x.ToString() + "x";
                if (putPower)
                {
                    res += "^" + power.ToString();
                }
            }
            return res;
        }

        public decimal Num { get; set; }
        public List<decimal> XList { get; set; }
    }
}