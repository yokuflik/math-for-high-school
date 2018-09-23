using System.Collections.Generic;

namespace math
{
    public class SmallEqualination: EqualinationRes
    {
        public SmallEqualination()
        {
            Num = 0;
            XList = new List<decimal>();
            numbersAndX = new List<NumbersAndX>();
        }

        public SmallEqualination(decimal num, List<decimal> xList)
        {
            Num = num;
            XList = xList;
            numbersAndX = new List<NumbersAndX>();
        }

        public SmallEqualination(decimal num, List<decimal> xList, List<NumbersAndX> numbersAndXes)
        {
            Num = num;
            XList = xList;
            numbersAndX = numbersAndXes;
        }

        public override string ToString()
        {
            string res = "";

            //add the numbers and xes
            for (int i = 0; i <= numbersAndX.Count - 1; i++)
            {
                if (res != "")
                {
                    res += " + ";
                }
                res += numbersAndX[i].ToString();
            }

            //add all the x powers
            if (XList.Count != 0)
            {
                for (int i = XList.Count - 1; i >= 1; i--)
                {
                    addToResNumber(ref res, XList[i], i + 1, true);
                }

                //add x power one
                addToResNumber(ref res, XList[0], 1, true);
            }

            //add the number
            if (Num == 0 && res == "")
            {
                res += "0";
            }
            else
            {
                addToResNumber(ref res, Num, 1, false);
            }
            return res;
        }

        public SmallEqualination Clone()
        {
            SmallEqualination res = new SmallEqualination();
            res.Num = Num;
            res.XList = new List<decimal>(XList);
            res.numbersAndX = new List<NumbersAndX>();
            for (int i = 0; i <= numbersAndX.Count - 1; i++)
            {
                res.numbersAndX.Add(numbersAndX[i].Clone(numbersAndX[i]));
            }
            return res;
        }

        private void addToResNumber(ref string res, decimal num, int power, bool isX)
        {
            if (num == 0)
            {
                return;
            }
            if (res != "")
            {
                if (num > 0)
                {
                    res += " + ";
                }
                else
                {
                    res += " - ";
                    num = -num;
                }
            }
            if (isX) {
                res += getStringFromX(num, power, power != 1);//add the power if power!= 1
            }
            else
            {
                res += num.ToString();
            }
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

        public List<NumbersAndX> numbersAndX { get; set; }
    }
}