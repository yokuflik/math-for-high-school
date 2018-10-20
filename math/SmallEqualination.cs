using System;
using System.Collections.Generic;

namespace math
{
    public class SmallEqualination: EqualinationRes
    {
        CalcExercise calc = new CalcExercise();
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
                res.numbersAndX.Add(numbersAndX[i].Clone());
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
            else if (x == -1)
            {
                res += "-x";
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

        public SmallEqualination getMinus()
        {
            SmallEqualination res = new SmallEqualination();
            res.Num = -Num;
            //get the minus x list
            for (int i = 0; i <= XList.Count - 1; i++)
            {
                res.XList.Add(-XList[i]);
            }
            //get the x and numbers minus
            for (int  i=0;i<= numbersAndX.Count - 1; i++)
            {
                res.numbersAndX.Add(numbersAndX[i].getMinus());
            }
            return res;
        }

        public List<decimal> XList { get; set; }

        public List<NumbersAndX> numbersAndX { get; set; }

        public void setXList()
        {
            for (int i = XList.Count - 1; i >= 0; i--)
            {
                if (XList[i] != 0)
                {
                    return;
                }
                XList.Remove(i);
            }
        }

        public SmallEqualination Calc()
        {
            string way = "";
            return calc.getSmallerEqualinationFromSymbols(getSymbols(), false, ref way);
        }

        public List<Symbol> getSymbols(bool addParenthessis = false, bool addPlus = true, bool addNumbersAndXList = true)
        {
            List<Symbol> res = new List<Symbol>();

            //add the x list
            for (int i = 0; i <= XList.Count - 1; i++)
            {
                if (XList[i] != 0)
                {
                    if (res.Count != 0 && addPlus)
                    {
                        res.Add(new Symbol(Symbol.SymbolKinds.plus));
                    }
                    res.Add(new Symbol(Symbol.SymbolKinds.x, XList[i], i + 1));
                }
            }

            if (addNumbersAndXList)
            {
                //add the number and x list
                for (int i = 0; i <= numbersAndX.Count - 1; i++)
                {
                    if (res.Count != 0 && addPlus)
                    {
                        res.Add(new Symbol(Symbol.SymbolKinds.plus));
                    }
                    res.AddRange(numbersAndX[i].getSymbols());
                }
            }

            //add the number
            if (Num != 0 || res.Count == 0)
            {
                if (res.Count > 0 && addPlus)
                {
                    res.Add(new Symbol(Symbol.SymbolKinds.plus));
                }
                res.Add(new Symbol(Symbol.SymbolKinds.number, Num));
            }

            if (addParenthessis)
            {
                res.Insert(0, new Symbol(Symbol.SymbolKinds.parenthesisStart));
                res.Add(new Symbol(Symbol.SymbolKinds.parenthesisEnd));
            }

            return res;
        }

        public bool IsOnlyNum()
        {
            if (XList.Count == 0 && numbersAndX.Count == 0)
            {
                return true;
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            try
            {
                SmallEqualination smallEqualination = (SmallEqualination)obj;
                if (smallEqualination.ToString() == ToString())
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return base.Equals(obj);
            }
        }

        public bool IsEmpty()
        {
            setXList();
            if (Num == 0 && XList.Count==0 && numbersAndX.Count == 0)
            {
                return true;
            }
            return false;
        }
    }
}