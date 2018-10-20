using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace math
{
    class CalcExercise
    {
        public bool error { get; set; }

        #region things to string

        public string pointFToString(PointF p)
        {
            return "(" + numberToString(p.X) + ", " + numberToString(p.Y) + ")";
        }

        public string numberToString(float num)
        {
            return string.Format("{0:0.####}", num);
        }

        public string numberToString(double num)
        {
            return string.Format("{0:0.####}", num);
        }

        public string numberToString(decimal num)
        {
            return string.Format("{0:0.####}", num);
        }

        public string getStringFromListOfSymbols(List<Symbol> symbols)
        {
            string res = "";
            for (int i = 0; i <= symbols.Count - 1; i++)
            {
                res += symbols[i].ToString();
            }
            return res;
        }

        private string getStringOfXListMinusStartIndex(List<decimal> xList, int startIndex, int endIndex)
        {
            SmallEqualination res = new SmallEqualination();
            for (int i = startIndex + 1; i <= endIndex; i++) //add all the x list minus the start power
            {
                res.XList.Add(xList[i]);
            }
            res.Num = xList[startIndex];
            return res.ToString();
        }

        public string removeDuplicateLinesFromWay(string way)
        {
            List<string> list = new List<string>(way.Split(new string[] { "\n" }, StringSplitOptions.None));
            //if a line equals to the lsat line so remove it
            for (int i = 1; i <= list.Count - 2; i++)
            {
                if (list[i-1] == list[i] && list[i-1] != "\n")
                {
                    list.RemoveAt(i);
                    i--;
                }
            }
            string res = "";
            for (int i = 0; i <= list.Count - 1; i++)
            {
                if (res != "")
                {
                    res += "\n";
                }
                res += list[i];
            }
            return res;
        }

        #endregion

        #region for displaying and clacing funcs

        public decimal putXInEqualination(SmallEqualination smallEqualination, double x, ref string way)
        {
            return putXInEqualination(smallEqualination.getSymbols(), x, ref way);
        }

        public decimal putXInEqualination(List<Symbol> symbols, double x, ref string way)
        {
            error = false;
            List<Symbol> symbols2 = new List<Symbol>();
            symbols2.AddRange(symbols);
            for (int i = 0; i <= symbols2.Count - 1; i++)
            {
                if (symbols2[i].Kind == Symbol.SymbolKinds.x)
                {
                    try
                    {
                        symbols2[i] = new Symbol(Symbol.SymbolKinds.number, symbols2[i].Num * (decimal)Math.Pow(x, symbols2[i].Pow));
                    }
                    catch
                    {
                        //check if the value is plus or minus
                        bool minus = false;
                        if (symbols2[i].Num > 0 && x > 0)
                        {
                            minus = false;
                        }
                        else
                        {
                            if (symbols2[i].Pow % 2 == 0) //if the power is double
                            {
                                if (symbols2[i].Num < 0)
                                {
                                    minus = true;
                                }
                            }
                            else
                            {
                                if (symbols2[i].Num < 0 && x < 0)
                                {
                                    minus = false;
                                }
                                else if (symbols2[i].Num < 0 || x< 0)
                                {
                                    minus = true;
                                }
                            }
                        }

                        if (minus)
                        {
                            return decimal.MinValue;
                        }
                        else
                        {
                            return decimal.MaxValue;
                        }
                    }
                }
            }
            way += "\n" + getStringFromListOfSymbols(symbols2);
            return getSmallerEqualinationFromSymbols(symbols2, true, ref way).Num;
        }

        public bool checkIfIsInTheDomainDefinition(List<FuncDomainDefinition> domainDefinition, decimal x)
        {
            for (int i = 0; i <= domainDefinition.Count - 1; i++)
            {
                if (domainDefinition[i].Kind == FuncDomainDefinition.DomainDefinitionKinds.xNotEquals && domainDefinition[i].X == x)
                {
                    return false;
                }
            }
            return true;
        }

        public int getBiggestPowerFormXList(List<decimal> xList)
        {
            for (int i = xList.Count - 1; i >= 0; i--)
            {
                if (xList[i] != 0)
                {
                    return i;
                }
            }
            return 0;
        }

        #endregion

        public NumbersAndX checkIfCanGetASmallerDevide(Devide devide)
        {
            if (devide.Counter.IsEmpty())
            {
                return new NumbersAndX(new List<Symbol> { new Symbol(Symbol.SymbolKinds.number, 0) });
            }
            if (devide.Denominator.IsEmpty())
            {
                error = true;
                return new NumbersAndX(new List<Symbol> { new Symbol(Symbol.SymbolKinds.number, 0) });
            }
            if (devide.Counter.IsOnlyNum() && devide.Denominator.IsOnlyNum())
            {
                return new NumbersAndX(new List<Symbol> { new Symbol(Symbol.SymbolKinds.number, devide.Counter.Num/devide.Denominator.Num) });
            }

            //check if is all in the same power
            string way = "";
            devide.Counter = devide.Counter.Calc();
            devide.Denominator = devide.Denominator.Calc();
            //try to move out if cannet get smaller devide
            bool cBigThenZero = devide.Counter.Num != 0;
            bool dBigThenZero = devide.Denominator.Num != 0;
            if (cBigThenZero != dBigThenZero)//like 2+x/x
            {
                return devide;
            }

            devide.Counter.setXList();
            devide.Denominator.setXList();

            int startX = getStartX(devide.Counter.XList);
            int DstartX = getStartX(devide.Denominator.XList);
            int startX2 = startX;

            if (devide.Counter.Num == 0)
            {
                if (getNumOfXes(devide.Denominator.XList) == 1 && devide.Denominator.numbersAndX.Count==0)
                {
                    if (startX >= devide.Denominator.XList.Count - 1)
                    {
                        SmallEqualination resEqual = new SmallEqualination();
                        addNumOrXToSmallEqualination(ref resEqual, devide.Counter.XList[startX] / devide.Denominator.XList[devide.Denominator.XList.Count - 1], startX, DstartX);
                        for (int i = startX + 1; i <= devide.Counter.XList.Count - 1; i++)
                        {
                            resEqual.XList.Add(devide.Counter.XList[i] / devide.Denominator.XList[devide.Denominator.XList.Count - 1]);
                        }
                        NumbersAndX numbersAndX = new NumbersAndX();
                        numbersAndX.Symbols = resEqual.getSymbols();
                        return numbersAndX;
                    }
                }
                else if (devide.Denominator.numbersAndX.Count != 0)
                {
                    if (devide.Counter.numbersAndX.Count == 0)
                    {
                        return devide;
                    }
                    else
                    {
                        if (devide.Denominator.numbersAndX[0].Type == NumbersAndX.NumbersAndXTypes.root)
                        {
                            Root Droot = (Root)devide.Denominator.numbersAndX[0];
                            //move an the numbers and x of the counter and if has any root that can get small with it
                            for (int i = 0; i <= devide.Counter.numbersAndX.Count - 1; i++)
                            {
                                if (devide.Counter.numbersAndX[i].Type == NumbersAndX.NumbersAndXTypes.root)
                                {
                                    Root CRoot = (Root)devide.Counter.numbersAndX[i];
                                    if (Droot.Expression.ToString() == CRoot.Expression.ToString() && Droot.Power == CRoot.Power)
                                    {
                                        Devide newDev = new Devide();
                                        newDev.Counter = CRoot.Multiplay;
                                        newDev.Denominator = Droot.Multiplay;
                                        return checkIfCanGetASmallerDevide(newDev);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (startX >= DstartX)
                    {
                        decimal devStart = devide.Counter.XList[startX] / devide.Denominator.XList[DstartX];
                        SmallEqualination smallEqualination = new SmallEqualination();
                        if (startX == DstartX)
                        {
                            smallEqualination.Num = devStart;
                        }
                        else
                        {
                            smallEqualination.XList = addXToTheList(new Symbol(Symbol.SymbolKinds.x, devStart, startX - DstartX + 1), new List<decimal>());
                        }
                        for (int i = startX; i <= devide.Counter.XList.Count - 1; i++)
                        {

                        }
                    }
                }
            }


            if (devide.Counter.XList.Count != devide.Denominator.XList.Count)//like x^2+x/x^3
            {
                return devide;
            }

            for (int i = 0; i <= devide.Counter.XList.Count - 1; i++)
            {
                cBigThenZero = devide.Counter.XList[i] != 0;
                dBigThenZero = devide.Denominator.XList[i] != 0;
                if (cBigThenZero != dBigThenZero) //like 2x+x^2/x^2
                {
                    return devide;
                }
            }

            decimal dev;
            if (devide.Counter.Num > 0)
            {
                dev = devide.Counter.Num / devide.Denominator.Num;
            }
            else
            {
                dev = devide.Counter.XList[startX] / devide.Denominator.XList[startX];
                startX++;
            }
            for (; startX <= devide.Counter.XList.Count - 1; startX++)
            {
                if (devide.Counter.XList[startX] / devide.Denominator.XList[startX] != dev) //like (2+2x^2)/(1+2x^2)
                {
                    return devide;
                }
            }

            NumbersAndX res = new NumbersAndX();
            //you can get a smaller devide
            //like (2x+2)/(1+x) = 2(x+1)/1(x+1) = 2/1 = 2 or (1+x)/(2+2x) = 1(1+x)/2(1+x) = 1/2 = 0.5
            res.Symbols.Add(new Symbol(Symbol.SymbolKinds.number, dev));
            return res;
        }

        private void addNumOrXToSmallEqualination(ref SmallEqualination smallEqualination, decimal num, int startXOne, int startXTwo)
        {
            if (startXOne == startXTwo)
            {
                smallEqualination.Num = num;
            }
            else
            {
                smallEqualination.XList = addXToTheList(new Symbol(Symbol.SymbolKinds.x, num, startXOne - startXTwo), smallEqualination.XList);
            }
        }

        private int getNumOfXes(List<decimal> xList)
        {
            int res = 0;
            for (int i = 0; i <= xList.Count - 1; i++)
            {
                if (xList[i] != 0)
                {
                    res++;
                }
            }
            return res;
        }
        
        private int getStartX(List<decimal> xList)
        {
            if (xList.Count == 0)
            {
                return 0;
            }
            for (int i = 0; i <= xList.Count - 1; i++)
            {
                if (xList[i] != 0)
                {
                    return i;
                }
            }
            return xList.Count -1;
        }

        #region calc not equals

        public FuncDomainDefinition calcNotEqualsEqualination(string sideBigger, string sideSmaller, ref string way)
        {
            //List
            return new FuncDomainDefinition(FuncDomainDefinition.DomainDefinitionKinds.xNotEquals, 0);
        }

        public List<FuncDomainDefinition> calcNotEqualsEqualinationFromSmallEqualination(SmallEqualination smallEqualination)
        {
            List<FuncDomainDefinition> res = new List<FuncDomainDefinition>();
            DifferentialCalculus differentialCalculus = new DifferentialCalculus();
            return differentialCalculus.getPositiveDomain(smallEqualination);
        }

        #endregion

        #region get equalination res and end res funcs

        public EqualinationRes calcEqualination(string text, ref string way)
        {
            //check the kind of the equalination
            int equalSignIndex = text.IndexOf('=');
            if (equalSignIndex == 0 || equalSignIndex == text.Length - 1) //its an error
            {
                wasOnError();
                return new EqualinationRes();
            }
            if (equalSignIndex != -1) //if has the sign in the string
            {
                string sideOneText = text.Substring(0, equalSignIndex);
                string sideTwoText = text.Substring(equalSignIndex+1, text.Length - equalSignIndex - 1);
                return calcRegularEqualinationFromText(sideOneText, sideTwoText, ref way);
            }
            else
            {
                return getSmallerEqualinationFromText(text, ref way);
            }
        }

        public EqualinationRes calcRegularEqualinationFromText(string sideOne, string sideTwo, ref string way)
        {
            string way2 = "";
            SmallEqualination sideOneSymbols = getSmallerEqualinationFromText(sideOne, ref way2);
            SmallEqualination sideTwoSymbols = getSmallerEqualinationFromText(sideTwo, ref way2);
            way2 = way2.Remove(0, 1);
            way += way2.Replace("\n", " = 0\n");
            return calcRegularEqualinationFromSmallEqualinations(sideOneSymbols, sideTwoSymbols, ref way);
        }

        public EqualinationRes calcRegularEqualinationFromSmallEqualinations(SmallEqualination sideOne, SmallEqualination sideTwo, ref string way)
        {
            SmallEqualination smallRes = subtractTwoSmallEqualinations(sideOne, sideTwo);
            way += "\n" + smallRes.ToString() + " = 0";

            //check what kind of equalination is it
            if (smallRes.numbersAndX.Count == 0)
            {
                return calcEqualinationFromSmallResWithoutNumberAndXList(smallRes, ref way);
            }
            else
            {
                return calcEqualinationFromSmallResWithNumberAndXList(smallRes, ref way);
            }
        }

        #region calc with numbers and x list

        public EqualinationRes calcEqualinationFromSmallResWithNumberAndXList(SmallEqualination smallRes, ref string way)
        {
            //calc the devides
            findCommonGroundAndMultiplayTheResWithIt(ref smallRes, ref way);
            way += "\n" + smallRes.ToString() + " = 0";

            //calc the roots
            putPowerToTheEqualination(ref smallRes, ref way);
            way += "\n" + smallRes.ToString() + " = 0";

            return calcEqualinationFromSmallResWithoutNumberAndXList(smallRes, ref way);
        }

        public void findCommonGroundAndMultiplayTheResWithIt(ref SmallEqualination smallRes, ref string way)
        {
            SmallEqualination commonGround = new SmallEqualination(1, new List<decimal>());
            List<SmallEqualination> denominators = new List<SmallEqualination>();
            for (int i = 0; i <= smallRes.numbersAndX.Count - 1; i++)
            {
                //find all the denominator
                if (smallRes.numbersAndX[i].Type == NumbersAndX.NumbersAndXTypes.devide)
                {
                    Devide dev = (Devide)smallRes.numbersAndX[i];
                    denominators.Add(dev.Denominator.Clone());
                }
            }
            if (denominators.Count == 0)
            {
                return;
            }
            commonGround = denominators[0];
            string way2 = "";
            //find the common ground
            for (int i = 1; i <= denominators.Count - 1; i++)
            {
                commonGround = findCommonDenominatorBetweenTwoSmallEqualinations(commonGround, denominators[i], ref way2);
            }
            commonGround = getSmallerEqualinationFromSymbols(commonGround.getSymbols(), false, ref way2);
            way += "  \\ " + commonGround.ToString();
            //get out the numbers and x
            List<NumbersAndX> numbersAndX = smallRes.numbersAndX;
            smallRes.numbersAndX = new List<NumbersAndX>();
            if (!smallRes.IsEmpty())
            {
                smallRes = multiplayTwoSmallEqualinations(commonGround.Clone(), smallRes);
            }
            //smallRes.numbersAndX.AddRange(numbersAndX);
            for (int i = 0; i <= numbersAndX.Count - 1; i++)
            {
                if (numbersAndX[i].Type == NumbersAndX.NumbersAndXTypes.devide)
                {
                    Devide devide = (Devide)numbersAndX[i];
                    smallRes.numbersAndX.Add(new NumbersAndX(devide.Counter.getSymbols()));
                    Devide dev = new Devide();
                    dev.Counter = commonGround;
                    dev.Denominator = devide.Denominator;
                    dev.Denominator.Calc();
                    smallRes.numbersAndX[smallRes.numbersAndX.Count - 1].multiplayWithSmallEqualination(new SmallEqualination(0, new List<decimal>(), new List<NumbersAndX> { checkIfCanGetASmallerDevide(dev) }));
                }
            }
        }

        private SmallEqualination findCommonDenominatorBetweenTwoSmallEqualinations(SmallEqualination commonGround, SmallEqualination smallEqualination, ref string way)
        {
            Devide dev = new Devide();
            smallEqualination = smallEqualination.Calc();
            commonGround = commonGround.Calc();
            dev.Counter = smallEqualination;
            dev.Denominator = commonGround;
            NumbersAndX res = checkIfCanGetASmallerDevide(dev);
            if (res.Type == NumbersAndX.NumbersAndXTypes.devide)
            {
                return commonGround;
            }
            else
            {
                string way2 = "";
                return smallEqualination;
            }

            /*List<Symbol> symbols = new List<Symbol>();
            for (int i = 0; i <= smallEqualination.numbersAndX.Count - 1; i++)
            {
                if (symbols.Count != 0)
                {
                    symbols.Add(new Symbol(Symbol.SymbolKinds.plus));
                }
                symbols.AddRange(smallEqualination.numbersAndX[i].getSymbols());
            }
            if (symbols.Count != 0)
            {
                symbols.Insert(0, new Symbol(Symbol.SymbolKinds.plus));
            }
            symbols.InsertRange(0, getSymbolsFromSmallEqualination(smallEqualination));
            smallEqualination = getSmallerEqualinationFromSymbols(symbols, false, ref way);
            decimal dev;
            smallEqualination.setXList();
            commonGround.setXList();*/
            /*List<Symbol> commonGroundList = getSymbolsFromSmallEqualination(commonGround, false);
            List<Symbol> smallEqualinationList = getSymbolsFromSmallEqualination(smallEqualination, false);*/
            //add after
            
            //return multiplayTwoSmallEqualinations(commonGround, smallEqualination);
        }

        private void putPowerToTheEqualination(ref SmallEqualination smallRes, ref string way)
        {
            //check if has any root
            int count = 0;
            int pos = 0;
            for (int i = 0; i <= smallRes.numbersAndX.Count - 1; i++)
            {
                if (smallRes.numbersAndX[i].Type == NumbersAndX.NumbersAndXTypes.root)
                {
                    count++;
                    pos = i;
                }
            }

            //for now it can calc only with one root
            if (count == 1)
            {
                //put the power of the root to the equalination

                //take out the root
                Root root = (Root)smallRes.numbersAndX[pos];
                smallRes.numbersAndX.RemoveAt(pos);

                //put the power to all the equalination
                List<Symbol> symbols = new List<Symbol>();
                if (!smallRes.IsEmpty()) //add a power to all the equalination if isn't 0
                {
                    symbols = smallRes.getSymbols(addParenthessis:true);
                    symbols.Add(new Symbol(Symbol.SymbolKinds.power));
                    symbols.Add(new Symbol(Symbol.SymbolKinds.number, root.Power));
                }
                //add -(multiplay)(root)
                //symbols.AddRange(root.getMinus().getSymbols());
                symbols.Add(new Symbol(Symbol.SymbolKinds.minus));
                if (root.Multiplay.ToString() != "1") //add the multiplay if isn't 1
                {
                    symbols.Add(new Symbol(Symbol.SymbolKinds.parenthesisStart));
                    symbols.AddRange(root.Multiplay.getSymbols(addParenthessis:true));
                    symbols.Add(new Symbol(Symbol.SymbolKinds.power));
                    symbols.Add(new Symbol(Symbol.SymbolKinds.number, root.Power));
                    symbols.Add(new Symbol(Symbol.SymbolKinds.parenthesisEnd));
                }
                symbols.AddRange(root.Expression.getSymbols(addParenthessis:true));
                
                //add to the way
                way +="\n"+getStringFromListOfSymbols(symbols)+" = 0";
                string way2 = "";
                smallRes = getSmallerEqualinationFromSymbols(symbols, false, ref way2);
                way2 = way2.Remove(0, 1);
                way += "\n"+way2.Replace("\n", " = 0\n");
            }
            else
            {
                smallRes.cantSolve = true;
                return;
            }
        }

        #endregion

        #region calc without numbers and x list

        private EqualinationRes calcEqualinationFromSmallResWithoutNumberAndXList(SmallEqualination smallRes, ref string way)
        {
            EqualinationRes res = new EqualinationRes();
            //add the x and numbers list that theyr type is regular
            List<Symbol> symbols = smallRes.getSymbols();
            /*for (int i = 0; i <= smallRes.numbersAndX.Count - 1; i++)
            {
                if (smallRes.numbersAndX[i].Type == NumbersAndX.NumbersAndXTypes.regular)
                {
                    symbols.Add(new Symbol(Symbol.SymbolKinds.plus));
                    symbols.Add(new Symbol(Symbol.SymbolKinds.parenthesisStart));
                    symbols.AddRange(smallRes.numbersAndX[i].Symbols);
                    symbols.Add(new Symbol(Symbol.SymbolKinds.parenthesisEnd));

                }
            }*/
            //calc the new symbols
            string way2 = "";
            smallRes = getSmallerEqualinationFromSymbols(symbols, false, ref way2);
            way += "\n" + smallRes.ToString() + " = 0";

            smallRes.setXList();
            if (smallRes.XList.Count == 0)
            {
                return res;
            }
            else if (smallRes.XList.Count == 1) //regular equlination
            {
                if (smallRes.XList[0] == 0)
                {
                    res.Results.Clear();
                    return res;
                }
                way += "\nx = " + numberToString((-smallRes.Num) / smallRes.XList[0]);
                res.Results.Add((-smallRes.Num) / smallRes.XList[0]);
            }
            else if (smallRes.XList.Count == 2)
            {
                return rootFormula(smallRes.XList[1], smallRes.XList[0], smallRes.Num, ref way);
            }
            else
            {
                //check if has only one x
                int count = 0;
                int startIndex = -1;
                int endIndex = 0;
                for (int i = 0; i <= smallRes.XList.Count - 1; i++)
                {
                    if (smallRes.XList[i] != 0)
                    {
                        count++;
                        if (startIndex == -1)
                        {
                            startIndex = i;
                        }
                        endIndex = i;
                    }
                }

                if (count == 1) //the equalination has only one x like: x^3-1=0
                {
                    if (smallRes.Num == 0)//x^3-0=0
                    {
                        res.Results.Add(0);
                        way += "\nx = 0";
                        return res;
                    }
                    else
                    {
                        smallRes.Num /= -smallRes.XList[startIndex];
                        way += "\n" + new Symbol(Symbol.SymbolKinds.x, smallRes.XList[startIndex], startIndex + 1).ToString() + " = " + numberToString(smallRes.Num);
                        if ((startIndex + 1) % 2 == 0) //if the power is double
                        {
                            //if two of them are plus or minus so dousent have a result
                            //if ((smallRes.XList[startIndex] > 0 && smallRes.Num > 0) || (smallRes.XList[startIndex] < 0 && smallRes.Num < 0))
                            if (smallRes.Num < 0)
                            {
                                return res;
                            }
                            res.Results.Add(-(decimal)Math.Pow((double)smallRes.Num, 1 / (startIndex + 1))); //if the power is adouble number it has +-root answer
                            way += "\nx = " + numberToString(res.Results[res.Results.Count - 1]);
                        }
                        res.Results.AddRange(getAnswerFromRoot(smallRes.Num, startIndex + 1, ref way));
                    }
                    return res;
                }
                else if (count == 2 && smallRes.Num == 0)
                {
                    if (startIndex == 0) //if its x^1 out of the parenthessis
                    {
                        way += "\nx";
                    }
                    else
                    {
                        way += "\nx^" + (startIndex + 1).ToString();
                    }
                    way += "(" + getStringOfXListMinusStartIndex(smallRes.XList, startIndex, endIndex) + ") = 0";
                    res.Results.Add(0);
                    way += "\nx = 0";
                    smallRes.XList[startIndex] /= -smallRes.XList[endIndex];
                    res.Results.AddRange(getAnswerFromRoot(smallRes.XList[startIndex], endIndex - startIndex, ref way));
                    return res;
                }
                else if (count == 3 && smallRes.Num == 0)
                {
                    if (endIndex - startIndex < 3)
                    {
                        way += "\nx(" + getStringOfXListMinusStartIndex(smallRes.XList, startIndex, endIndex) + ") = 0";
                        res.Results.Add(0);
                        way += "\nx = 0";
                        if (endIndex - startIndex == 2)
                        {
                            res = rootFormula(smallRes.XList[startIndex + 2], smallRes.XList[startIndex + 1], smallRes.XList[startIndex], ref way);
                        }
                        else if (endIndex - startIndex == 1)
                        {
                            res.Results.Add(smallRes.XList[startIndex] / smallRes.XList[endIndex]);
                            way += "\nx = " + numberToString(res.Results[res.Results.Count - 1]);
                        }
                        return res;
                    }
                }
                res.cantSolve = true;
            }
            return res;
        }

        private EqualinationRes rootFormula(decimal a, decimal b, decimal c, ref string way)
        {
            EqualinationRes res = new EqualinationRes();
            if (a == 0)
            {
                if (c == 0)
                {
                    res.Results.Add(0);
                    return res;
                }
                res.Results.Add(c / b);
                return res;
            }
            //check if b^2-4ac isnt below zero
            decimal root = b * b - 4 * a * c;
            if (root < 0)
            {
                way += "\nThe root formula of this dousnt give any answers";
                return res;
            }
            root = (decimal)Math.Sqrt((double)root);
            if (root == 0)
            {
                res.Results.Add(-b / (2 * a));
                way += "\nx = " + numberToString(res.Results[res.Results.Count - 1]);
            }
            else
            {
                res.Results.Add((-b + root) / (2 * a));
                way += "\nx = " + numberToString(res.Results[res.Results.Count - 1]);
                res.Results.Add((-b - root) / (2 * a));
                way += "\nx = " + numberToString(res.Results[res.Results.Count - 1]);
            }
            return res;
        }
        
        private List<decimal> getAnswerFromRoot(decimal num, int power, ref string way)
        {
            List<decimal> res = new List<decimal>();
            CalcExercise calc = new CalcExercise();
            if (num < 0)
            {
                if (power % 2 == 0) //the power is double so cant do root
                {
                    return new List<decimal>();
                }
                res.Add((decimal)-Math.Pow((double)-num, (double)1 / (double)power));
                way += "\nx = " + calc.numberToString(res[res.Count - 1]);
            }
            else
            {
                if (power % 2 == 0) //if the power iis double so the root is +-num
                {
                    res.Add((decimal)-Math.Pow((double)num, (double)1 / power));
                    way += "\nx = " + calc.numberToString(res[res.Count - 1]);
                }
                res.Add((decimal)Math.Pow((double)num, (double)1 / power));
                way += "\nx = " + calc.numberToString(res[res.Count - 1]);
            }
            return res;
        }

        #endregion

        #endregion

        #region get small equalination

        private SmallEqualination subtractTwoSmallEqualinations(SmallEqualination one, SmallEqualination two)
        {
            if (two.XList.Count > one.XList.Count)
            {
                //add the numbers and x list
                for (int i = 0; i <= one.numbersAndX.Count - 1; i++)
                {
                    two.numbersAndX.Add(one.numbersAndX[i].getMinus());
                }
                two.Num -= one.Num;
                for (int i = 0; i <= one.XList.Count - 1; i++)
                {
                    two.XList[i] -= one.XList[i];
                }
                return two;
            }
            else
            {
                //add the numbers and x list
                for (int i = 0; i <= two.numbersAndX.Count - 1; i++)
                {
                    one.numbersAndX.Add(two.numbersAndX[i].getMinus());
                }
                one.Num -= two.Num;
                for (int i =0;i<=two.XList.Count - 1; i++)
                {
                    one.XList[i] -= two.XList[i];
                }
                return one;
            }
        }

        public SmallEqualination getSmallerEqualinationFromText(string text, ref string way)
        {
            error = false;
            //start solving

            //get a list of the numbers from the string
            List<Symbol> symbols = getAListOfNumbersFromString(text);

            //calc the list of numbers
            SmallEqualination res = getSmallerEqualinationFromSymbols(symbols, true, ref way);

            //show the answer
            return res;
        }

        public SmallEqualination getSmallerEqualinationFromSymbols(List<Symbol> symbols, bool checkProblems, ref string way)
        {
            //check if the list has problems
            if (checkProblems && checkForProblems(ref symbols))
            {
                wasOnError();
                return new SmallEqualination(0, new List<decimal>());
            }

            //calc the list of numbers
            return startCalcing(symbols, ref way);
        }

        #endregion

        #region calcing

        private SmallEqualination startCalcing(List<Symbol> symbols, ref string way)
        {
            if (symbols.Count == 0)
            {
                return new SmallEqualination(0, new List<decimal>());
            }
            SmallEqualination res = new SmallEqualination();
            //calc the funcs like root and cos
            calcFuncs(ref symbols, ref res);

            way += "\n" + getStringFromListOfSymbols(symbols)+res.ToString();

            //calc the parenthessis
            calcParenthessis(ref symbols, ref res);

            //2
            //calc the multiplay and divide
            calcPowerAndMultiplayAndDivide(ref symbols, ref res);

            //3
            //calc the plus and minus
            calcPlusAndMinus(symbols, ref way, ref res);
            return res;
        }

        #region calc funcs

        private void calcFuncs(ref List<Symbol> symbols, ref SmallEqualination res)
        {
            //move an the symbols and if has an symbol that is a func calc it
            for (int i = 0; i <= symbols.Count - 6; i++) //the minimum place tht root can bee is symbols.Count - 6
            {
                if (symbols[i].Kind == Symbol.SymbolKinds.root)
                {
                    int start = i;
                    int end = i + 2;
                    i++;
                    //get the after number
                    if (symbols[i].Kind != Symbol.SymbolKinds.parenthesisStart)
                    {
                        error = true;
                        return;
                    }
                    SmallEqualination num = getUntilSymbolNumbers(symbols, ref end, Symbol.SymbolKinds.comma);
                    end++;
                    if (symbols[end].Kind != Symbol.SymbolKinds.comma)
                    {
                        error = true;
                        return;
                    }
                    end++;
                    SmallEqualination pow = getUntilSymbolNumbers(symbols, ref end, Symbol.SymbolKinds.parenthesisEnd);
                    end += 2;

                    //calc the multiplay before
                    bool startsOrEndsWithDevide = false;
                    bool endsWithPower = false;
                    SmallEqualination multiplay = new SmallEqualination(1, new List<decimal>());
                    if (start != 0)
                    {
                        multiplay = getBeforeAndAfterOnePart(symbols, ref start, ref end, ref startsOrEndsWithDevide, ref endsWithPower);
                        //remove the root from the list
                        start++;
                    }
                    
                    if (startsOrEndsWithDevide || endsWithPower)
                    {
                        //adds (multiplay*root(num,pow))
                        /*List<Symbol> symbolsToAdd = new List<Symbol>();
                        if (multiplay.ToString() != "1")
                        {
                            symbolsToAdd.Add(new Symbol(Symbol.SymbolKinds.parenthesisStart));
                            symbolsToAdd.AddRange(getSymbolsFromSmallEqualination(multiplay));
                            symbolsToAdd.Add(new Symbol(Symbol.SymbolKinds.multiplay));
                        }
                        symbolsToAdd.Add(new Symbol(Symbol.SymbolKinds.root));
                        symbolsToAdd.Add(new Symbol(Symbol.SymbolKinds.parenthesisStart));
                        symbolsToAdd.AddRange(getSymbolsFromSmallEqualination(num));
                        symbolsToAdd.Add(new Symbol(Symbol.SymbolKinds.comma));
                        symbolsToAdd.Add(new Symbol(Symbol.SymbolKinds.number, pow.Num));
                        symbolsToAdd.Add(new Symbol(Symbol.SymbolKinds.parenthesisEnd));
                        symbols.InsertRange(start, symbolsToAdd);*/

                        //calc the divide
                        start--;
                        SmallEqualination symbolsBefore = getBeforeIndexOnePart(symbols, ref start, ref startsOrEndsWithDevide);
                        start++;
                        symbols.RemoveRange(start, end - start);
                        res.numbersAndX.Add(new Devide(symbolsBefore, new SmallEqualination(0, new List<decimal>(), new List<NumbersAndX> { new Root(num, multiplay, (int)pow.Num) })));
                    }
                    else
                    {
                        symbols.RemoveRange(start, end - start);
                        if (num.XList.Count == 0 && num.numbersAndX.Count == 0) //if the root number is a number
                        {
                            string way = "";
                            List<decimal> roots = getAnswerFromRoot(num.Num, (int)pow.Num, ref way);
                            if (roots.Count == 0)
                            {
                                error = true;
                                return;
                            }
                            SmallEqualination rootNum = new SmallEqualination(roots[roots.Count-1], new List<decimal>());
                            rootNum = multiplayTwoSmallEqualinations(rootNum, multiplay);
                            symbols.Insert(start, new Symbol(Symbol.SymbolKinds.parenthesisStart));
                            symbols.Insert(start+1, new Symbol(Symbol.SymbolKinds.parenthesisEnd));
                            symbols.InsertRange(start+1, rootNum.getSymbols());
                        }
                        else
                        {
                            res.numbersAndX.Add(new Root(num, multiplay, (int)pow.Num));
                        }
                    }
                    i = end;
                }
            }
        }

        private SmallEqualination getUntilSymbolNumbers(List<Symbol> symbols, ref int end, Symbol.SymbolKinds endSymbol)
        {
            SmallEqualination res = new SmallEqualination();
            int parenthessis = 0;
            int count = 0;
            int start = end;
            for (;end <= symbols.Count - 1; end++)
            {
                if (symbols[end].Kind == Symbol.SymbolKinds.parenthesisStart)
                {
                    parenthessis++;
                }
                else if (symbols[end].Kind == Symbol.SymbolKinds.parenthesisEnd)
                {
                    parenthessis--;
                    if (parenthessis == -1&&endSymbol == Symbol.SymbolKinds.parenthesisEnd)
                    {
                        end--;
                        break;
                    }
                }
                else if (parenthessis == 0 && symbols[end].Kind == endSymbol)
                {
                    end--;
                    break;
                }
                count++;
            }
            if (count > 0)
            {
                string way = "";
                res = getSmallerEqualinationFromSymbols(symbols.GetRange(start, count), false, ref way);
            }
            return res;
        }

        #endregion

        #region parenthessis

        private void calcParenthessis(ref List<Symbol> symbols, ref SmallEqualination smallEqualination)
        {
            //check for parenthessis start and if finds check where is the end and then calcs it
            for (int i = 0; i <= symbols.Count - 1; i++)
            {
                if (symbols[i].Kind == Symbol.SymbolKinds.parenthesisStart)
                {
                    int start = i;
                    int end = i + 1;
                    int count = 0;
                    int parenthessisStart = 0;
                    //check where it ends
                    for (int j = i + 1; j <= symbols.Count - 1; j++)
                    {
                        if (symbols[j].Kind == Symbol.SymbolKinds.parenthesisStart)
                        {
                            parenthessisStart++;
                        }
                        else if (symbols[j].Kind == Symbol.SymbolKinds.parenthesisEnd)
                        {
                            if (parenthessisStart == 0)
                            {
                                string way = "";
                                //calc the parenthessis and before and after multiplay
                                SmallEqualination res = getSmallerEqualinationFromSymbols(symbols.GetRange(start + 1, count), false, ref way);

                                end = start + count + 2;
                                bool devideBeforeOrAfter = false;
                                bool powerAfter = false;
                                SmallEqualination beforeAndAfter = getBeforeAndAfterOnePart(symbols, ref start, ref end, ref devideBeforeOrAfter, ref powerAfter);
                                /*if (start < 0)
                                {
                                    start = 0;
                                }*/
                                start++;
                                //remove all the parenthessis and put the res
                                symbols.RemoveRange(start, end - start);
                                if (devideBeforeOrAfter || powerAfter)
                                {
                                    List<Symbol> pSymbols = new List<Symbol>();
                                    //add parenthessis start and parenthessis end before and after th parenthessis res
                                    pSymbols.Add(new Symbol(Symbol.SymbolKinds.parenthesisStart));
                                    pSymbols.AddRange(res.getSymbols());
                                    pSymbols.Add(new Symbol(Symbol.SymbolKinds.parenthesisEnd));
                                    symbols.InsertRange(start, pSymbols);
                                }
                                else
                                {
                                    //putt the res
                                    res = multiplayTwoSmallEqualinations(res, beforeAndAfter);
                                    symbols.InsertRange(start, res.getSymbols());
                                    symbols.Insert(start, new Symbol(Symbol.SymbolKinds.plus));
                                }
                                j = symbols.Count - 1;
                                count = -1;
                            }
                            else
                            {
                                parenthessisStart--;
                            }
                        }
                        count++;
                    }
                }
            }
        }
        
        private SmallEqualination getBeforeAndAfterOnePart(List<Symbol> symbols, ref int startPIndex, ref int endPIndex, ref bool startsOrEndsWithDevide, ref bool powerAfter)
        {
            SmallEqualination res = new SmallEqualination(1, new List<decimal>());
            res = multiplayTwoSmallEqualinations(res, getBeforeIndexOnePart(symbols, ref startPIndex, ref startsOrEndsWithDevide));
            res = multiplayTwoSmallEqualinations(res, getAfterIndexOnePart(symbols, ref endPIndex, ref startsOrEndsWithDevide, ref powerAfter));
            return res;
        }

        private SmallEqualination getBeforeIndexOnePart(List<Symbol> symbols, ref int startPIndex, ref bool startsWithDevide)
        {
            if (startPIndex == 0)
            {
                return new SmallEqualination(1, new List<decimal>());
            }
            
            SmallEqualination res = new SmallEqualination(1, new List<decimal>());
            
            int count = 0;
            int parenthessis = 0;
            int addSymbol = 0;
            startPIndex--;
            //get the before numbers
            for (; startPIndex >= 0; startPIndex--)
            {
                if (symbols[startPIndex].Kind == Symbol.SymbolKinds.parenthesisEnd)
                {
                    parenthessis++;
                }
                else if (symbols[startPIndex].Kind == Symbol.SymbolKinds.parenthesisStart)
                {
                    parenthessis--;
                    if (parenthessis == -1)
                    {
                        if (startPIndex != 0)
                        {
                            if (symbols[startPIndex-1].Kind == Symbol.SymbolKinds.divide)
                            {
                                startsWithDevide = true;
                                return res;
                            } 
                        }
                    }
                }
                else if (parenthessis == 0)
                {
                    if (symbols[startPIndex].Kind == Symbol.SymbolKinds.minus)
                    {
                        res = res.getMinus();
                        symbols[startPIndex] = new Symbol(Symbol.SymbolKinds.number, 1);
                        count++;
                        startPIndex--;
                        break;
                    }
                    else if (symbols[startPIndex].Kind == Symbol.SymbolKinds.plus)
                    {
                        //startPIndex++;
                        break;
                    }
                    else if (symbols[startPIndex].Kind == Symbol.SymbolKinds.divide)
                    {
                        //startPIndex++;
                        startsWithDevide = true;
                        return res;
                    }
                }
                count++;
            }
            if (count > 0)
            {
                string way = "";
                SmallEqualination smallEqualinationRes = getSmallerEqualinationFromSymbols(symbols.GetRange(startPIndex+1-addSymbol, count+addSymbol), false, ref way);
                res = multiplayTwoSmallEqualinations(res, smallEqualinationRes);
            }
            return res;
        }

        private SmallEqualination getAfterIndexOnePart(List<Symbol> symbols, ref int endPIndex, ref bool endsWithDevide, ref bool powerAfter)
        {
            if (endPIndex > symbols.Count - 1)
            {
                return new SmallEqualination(1,new List<decimal>());
            }

            SmallEqualination res = new SmallEqualination(1, new List<decimal>());
            int count = 0;
            int parenthessis = 0;
            //get the after numbers
            for (; endPIndex <= symbols.Count - 1; endPIndex++)
            {
                if (symbols[endPIndex].Kind == Symbol.SymbolKinds.parenthesisStart)
                {
                    parenthessis++;
                }
                else if (symbols[endPIndex].Kind == Symbol.SymbolKinds.parenthesisEnd)
                {
                    parenthessis--;
                    if (parenthessis == -1)
                    {
                        break;
                    }
                }
                else if (parenthessis == 0 && (symbols[endPIndex].Kind == Symbol.SymbolKinds.minus || symbols[endPIndex].Kind == Symbol.SymbolKinds.plus))
                {
                    break;
                }
                else if (symbols[endPIndex].Kind == Symbol.SymbolKinds.divide)
                {
                    endsWithDevide = true;
                    return res;
                }
                else if (symbols[endPIndex].Kind == Symbol.SymbolKinds.power)
                {
                    powerAfter = true;
                    break;
                }
                count++;
            }
            if (count > 0)
            {
                string way = "";
                SmallEqualination smallEqualinationRes = getSmallerEqualinationFromSymbols(symbols.GetRange(endPIndex - count+1, count-1), false, ref way);
                res = multiplayTwoSmallEqualinations(res, smallEqualinationRes);
            }
            return res;
        }

        public SmallEqualination multiplayTwoSmallEqualinations(SmallEqualination one, SmallEqualination two)
        {
            SmallEqualination res = new SmallEqualination();

            one.setXList();
            two.setXList();
            int oneStartX = getStartX(one.XList);
            int twoStartX = getStartX(two.XList);
            if (one.Num != 0)
            {
                res.Num = one.Num * two.Num;
                //multiplay the num with the xList
                for (int i = twoStartX; i <= two.XList.Count - 1; i++)
                {
                    res.XList = addXToTheList(new Symbol(Symbol.SymbolKinds.x, one.Num * two.XList[i], i + 1), res.XList);
                }
            }
            //multiplay two number with xList
            if (two.Num != 0)
            {
                for (int i = oneStartX; i <= one.XList.Count - 1; i++)
                {
                    res.XList = addXToTheList(new Symbol(Symbol.SymbolKinds.x, two.Num * one.XList[i], i + 1), res.XList);
                }
            }

            //multiplay the one x list with the two x list
            for (int i = oneStartX; i <= one.XList.Count - 1; i++)
            {
                for (int j = twoStartX; j <= two.XList.Count - 1; j++)
                {
                    res.XList = addXToTheList(new Symbol(Symbol.SymbolKinds.x, one.XList[i] * two.XList[j], i + j + 2), res.XList);
                }
            }

            //multiplay one with the x and numbers
            for (int i = 0; i <= one.numbersAndX.Count - 1; i++)
            {
                one.numbersAndX[i] = one.numbersAndX[i].multiplayWithSmallEqualination(two);
            }

            for (int i = 0; i <= two.numbersAndX.Count - 1; i++)
            {
                two.numbersAndX[i] = two.numbersAndX[i].multiplayWithSmallEqualination(one);
            }

            res.numbersAndX.AddRange(one.numbersAndX);
            res.numbersAndX.AddRange(two.numbersAndX);

            /*string way = "";
            SmallEqualination smallEqualination = new SmallEqualination();
            calcPlusAndMinus(res, ref way, ref smallEqualination);*/
            return res;
        }

        #endregion

        #region power multiplay and devide

        private void calcPowerAndMultiplayAndDivide(ref List<Symbol> symbols, ref SmallEqualination smallEqualination)
        {
            calcPower(ref symbols);
            //move an the list and if you see a multiplay or divide symbol
            //and if you see calc it
            for (int i = 1; i <= symbols.Count - 1; i++)
            {
                if (symbols[i].Kind == Symbol.SymbolKinds.multiplay)
                {
                    if (symbols[i + 1].Kind == Symbol.SymbolKinds.plus)
                    {
                        symbols[i - 1] = getSymbolFromMultiplay(symbols[i - 1], symbols[i + 2]);
                        symbols.RemoveRange(i, 3);
                    }
                    else if (symbols[i + 1].Kind == Symbol.SymbolKinds.minus)
                    {
                        symbols[i - 1] = getSymbolFromMultiplay(symbols[i - 1], getMinusNumSymbol(symbols[i + 2]));
                        symbols.RemoveRange(i, 3);
                    }
                    else
                    {
                        symbols[i - 1] = getSymbolFromMultiplay(symbols[i - 1], symbols[i + 1]);
                        symbols.RemoveRange(i, 2);
                    }
                    i--;
                }
                else if (symbols[i].Kind == Symbol.SymbolKinds.x && (symbols[i-1].Kind == Symbol.SymbolKinds.number || symbols[i-1].Kind == Symbol.SymbolKinds.x))
                {
                    symbols[i - 1] = new Symbol(Symbol.SymbolKinds.x, symbols[i - 1].Num * symbols[i].Num, symbols[i].Pow);
                    symbols.RemoveAt(i);
                    i--;
                }

                else if (symbols[i].Kind == Symbol.SymbolKinds.divide)
                {
                    int secondNum = 1;
                    if (symbols[i + 1].Kind == Symbol.SymbolKinds.plus)
                    {
                        secondNum = 2;
                    }
                    else if (symbols[i + 1].Kind == Symbol.SymbolKinds.minus)
                    {
                        symbols[i - 1].Num = -symbols[i - 1].Num;
                        secondNum = 2;
                    }
                    if ((symbols[i + secondNum].Kind == Symbol.SymbolKinds.number || symbols[i + secondNum].Kind == Symbol.SymbolKinds.x)&& symbols[i+secondNum].Num == 0)//like 2/0
                    {
                        error = true;
                        return;
                    }
                    int startIndex = 0;
                    int endIndex = 0;
                    SmallEqualination before = new SmallEqualination();
                    SmallEqualination after = new SmallEqualination();
                    getDevideSymbols(symbols, ref before, ref after, i, ref startIndex, ref endIndex);
                    if (startIndex < 0)
                    {
                        startIndex++;
                    }
                    symbols.RemoveRange(startIndex, endIndex - startIndex);
                    i = startIndex;
                    bool canDev = false;
                    List<Symbol> dev = devideTwoSmallEqualinations(before, after, ref canDev);
                    if (canDev)
                    {
                        symbols.Add(new Symbol(Symbol.SymbolKinds.plus));
                        symbols.AddRange(dev);
                    }
                    else
                    {
                        smallEqualination.numbersAndX.Add(new Devide(before, after));
                    }
                    //symbols[i - 1].Num /= symbols[i + secondNum].Num;
                    //symbols.RemoveRange(i-1, secondNum + 2);
                    //i--;
                    
                }
            }
        }

        private void calcPower(ref List<Symbol> symbols)
        {
            //calc power
            for (int i = 1; i <= symbols.Count - 2; i++)
            {
                if (symbols[i].Kind == Symbol.SymbolKinds.power)
                {
                    int start = i -1;
                    int end = i + 1;
                    SmallEqualination before = getBeforePowerOnePart(symbols, ref start);
                    SmallEqualination after = getAfterDevideSmallEqualination(symbols, ref end);

                    if (after.XList.Count!=0 || after.numbersAndX.Count != 0) //like 1^x
                    {
                        return;
                    }
                    symbols.RemoveRange(start, end - start);
                    List<Symbol> symbolsToAdd = new List<Symbol>();
                    if (after.Num == 0) //like (x+2)^0 = 1
                    {
                        symbolsToAdd.Add(new Symbol(Symbol.SymbolKinds.number, 1));
                    }
                    else if (after.Num < 0) //like (x+2)^-1 = 1/(x+2)^1
                    {
                        symbolsToAdd.Add(new Symbol(Symbol.SymbolKinds.number, 1));
                        symbolsToAdd.Add(new Symbol(Symbol.SymbolKinds.divide));
                        symbolsToAdd.Add(new Symbol(Symbol.SymbolKinds.parenthesisStart));
                        symbolsToAdd.AddRange(before.getSymbols());
                        symbolsToAdd.Add(new Symbol(Symbol.SymbolKinds.parenthesisEnd));
                        symbolsToAdd.Add(new Symbol(Symbol.SymbolKinds.power));
                        symbolsToAdd.Add(new Symbol(Symbol.SymbolKinds.number, -after.Num));
                    }
                    else //like (x+2)^2
                    {
                        int xCount = getNumOfXes(before.XList);
                        if (xCount == 1 && before.Num == 0)
                        {
                            int startX = getStartX(before.XList);
                            symbolsToAdd.Add(new Symbol(Symbol.SymbolKinds.x, before.XList[startX], (startX+1)*(int)after.Num));
                        }
                        else
                        {
                            List<Symbol> beforeSymbols = before.getSymbols();
                            for (int j = 0; j <= after.Num - 1; j++)
                            {
                                symbolsToAdd.Add(new Symbol(Symbol.SymbolKinds.parenthesisStart));
                                symbolsToAdd.AddRange(beforeSymbols);
                                symbolsToAdd.Add(new Symbol(Symbol.SymbolKinds.parenthesisEnd));
                            }
                        }
                    }

                    //putt the symbols to add in the right place
                    string way2 = "";
                    SmallEqualination res = getSmallerEqualinationFromSymbols(symbolsToAdd, false, ref way2);
                    symbols.InsertRange(start, res.getSymbols());
                }
            }
        }

        private SmallEqualination getBeforePowerOnePart(List<Symbol> symbols, ref int start)
        {
            SmallEqualination res = new SmallEqualination();
            int parenthessis = 0;
            int count = 1;
            for (; start >= 0; start--)
            {
                if (symbols[start].Kind == Symbol.SymbolKinds.parenthesisStart)
                {
                    parenthessis--;
                }
                else if (symbols[start].Kind == Symbol.SymbolKinds.parenthesisEnd)
                {
                    parenthessis++;
                }
                else if (parenthessis == 0)
                {
                    if (symbols[start].Kind == Symbol.SymbolKinds.multiplay || symbols[start].Kind == Symbol.SymbolKinds.divide || symbols[start].Kind == Symbol.SymbolKinds.plus || symbols[start].Kind == Symbol.SymbolKinds.minus)
                    {
                        break;
                    }
                }
                count++;
            }
            count--;
            start++;
            string way = "";
            res = getSmallerEqualinationFromSymbols(symbols.GetRange(start, count), false, ref way);
            return res;

        }

        private List<Symbol> devideTwoSmallEqualinations(SmallEqualination before, SmallEqualination after, ref bool canDev)
        {
            List<Symbol> res = new List<Symbol>();
            if (after.XList.Count == 0)
            {
                if (after.Num == 0)
                {
                    error = true;
                    return res;
                }
                if (before.Num != 0)
                {
                    res.Add(new Symbol(Symbol.SymbolKinds.number, before.Num / after.Num));
                }
                for (int i = 0; i <= before.XList.Count - 1; i++)
                {
                    if (before.XList[i] != 0)
                    {
                        if (res.Count != 0)
                        {
                            res.Add(new Symbol(Symbol.SymbolKinds.plus));
                        }
                        res.Add(new Symbol(Symbol.SymbolKinds.x, before.XList[i] / after.Num, i + 1));
                    }
                }
                canDev = true;
            }
            else
            {
                canDev = false;
            }
            return res;
        }

        private void getDevideSymbols(List<Symbol> symbols, ref SmallEqualination symbolsBefore, ref SmallEqualination symbolsAfter, int i, ref int startIndex, ref int endIndex)
        {
            //get the before index
            startIndex = i;
            bool devBeforeOrAfter = false;
            symbolsBefore = getBeforeIndexOnePart(symbols, ref startIndex, ref devBeforeOrAfter);
            
            //get the after index
            endIndex = i+1;
            symbolsAfter = getAfterDevideSmallEqualination(symbols, ref endIndex);
        }

        private SmallEqualination getAfterDevideSmallEqualination(List<Symbol> symbols, ref int endIndex)
        {
            //move an the list of symbols and if the number end return it
            SmallEqualination res = new SmallEqualination();
            int parenthessis = 0;
            int count = 1;
            int start = endIndex;
            for (; endIndex <= symbols.Count - 1; endIndex++)
            {
                if (symbols[endIndex].Kind == Symbol.SymbolKinds.parenthesisStart)
                {
                    parenthessis++;
                }
                else if (symbols[endIndex].Kind == Symbol.SymbolKinds.parenthesisEnd)
                {
                    parenthessis--;
                    if (parenthessis == -1) //if end with ) [can be in func]
                    {
                        endIndex--;
                        break;
                    }
                }
                else if (parenthessis == 0)
                {
                    if (endIndex != start)
                    {
                        if (symbols[endIndex].Kind == Symbol.SymbolKinds.multiplay || symbols[endIndex].Kind == Symbol.SymbolKinds.divide || symbols[endIndex].Kind == Symbol.SymbolKinds.plus || symbols[endIndex].Kind == Symbol.SymbolKinds.minus)
                        {
                            break;
                        }
                    }
                }
                count++;
            }
            if (count > 0)
            {
                string way = "";
                res = getSmallerEqualinationFromSymbols(symbols.GetRange(start, endIndex - start), false, ref way);
            }
            return res;
        }

        private void addSmallEqualinationToListSymbols(ref List<Symbol> res, SmallEqualination smallEqualination)
        {
            if (smallEqualination.Num != 0)
            {
                res.Add(new Symbol(Symbol.SymbolKinds.number, smallEqualination.Num));
            }
            for (int i = 0; i <= smallEqualination.XList.Count - 1; i++)
            {
                if (smallEqualination.XList[i] != 0)
                {
                    res.Add(new Symbol(Symbol.SymbolKinds.x, smallEqualination.XList[i], i + 1));
                }
            }
        }

        private Symbol getSymbolFromMultiplay(Symbol symbol1, Symbol symbol2)
        {
            if (symbol1.Kind == Symbol.SymbolKinds.x && symbol2.Kind == Symbol.SymbolKinds.x)
            {
                return new Symbol(Symbol.SymbolKinds.x, symbol1.Num * symbol2.Num, symbol1.Pow + symbol2.Pow);
            }
            else if (symbol1.Kind == Symbol.SymbolKinds.x || symbol2.Kind == Symbol.SymbolKinds.x) //if one of them or two of them is x
            {
                return new Symbol(Symbol.SymbolKinds.x, symbol1.Num * symbol2.Num, symbol1.Pow * symbol2.Pow);
            }
            else
            {
                return new Symbol(Symbol.SymbolKinds.number, symbol1.Num * symbol2.Num);
            }
        }

        private Symbol getMinusNumSymbol(Symbol symbol)
        {
            symbol.Num = -symbol.Num;
            return symbol;
        }

        private Symbol getSymbolFromPower(Symbol symbol1, Symbol symbol2)
        {
            if (symbol2.Num == 0)
            {
                return new Symbol(Symbol.SymbolKinds.number, 0);
            }
            else if (symbol1.Kind == Symbol.SymbolKinds.number)
            {
                return new Symbol(Symbol.SymbolKinds.number, (decimal)Math.Pow((double)symbol1.Num, (double)symbol2.Num));
            }
            else if (symbol1.Kind == Symbol.SymbolKinds.x)
            {
                return new Symbol(Symbol.SymbolKinds.x, (decimal)Math.Pow((double)symbol1.Num, (double)symbol2.Num), (int)(symbol1.Pow*symbol2.Num));
            }
            return new Symbol(Symbol.SymbolKinds.number, 0);
        }

        #endregion

        #region plus and minus

        private void calcPlusAndMinus(List<Symbol> symbols, ref string way, ref SmallEqualination res)
        {
            if (symbols.Count == 0)
            {
                return;
            }
            //check for minus or plus in the start
            if (symbols[0].Kind == Symbol.SymbolKinds.minus)
            {
                symbols[1].Num = -symbols[1].Num;
                symbols.RemoveAt(0);
            }
            else if (symbols[0].Kind == Symbol.SymbolKinds.plus && symbols.Count != 1)
            {
                symbols.RemoveAt(0);
            }

            //get the number in start
            addToRes(ref res, symbols[0]);

            //check for a plus or minus sign and if finds calcs it
            for (int i = 1; i <= symbols.Count - 2; i++)
            {
                if (symbols[i].Kind == Symbol.SymbolKinds.plus)
                {
                    addToRes(ref res, symbols[i + 1]);
                }
                else if (symbols[i].Kind == Symbol.SymbolKinds.minus)
                {
                    subtractRes(ref res, symbols[i+1]);
                }
            }
            way += "\n" + res.ToString();
        }

        private void subtractRes(ref SmallEqualination res, Symbol symbol)
        {
            symbol.Num = -symbol.Num;
            addToRes(ref res, symbol);
        }

        private void addToRes(ref SmallEqualination res, Symbol symbol)
        {
            if (symbol.Kind == Symbol.SymbolKinds.number)
            {
                res.Num += symbol.Num;
            }
            else if (symbol.Kind == Symbol.SymbolKinds.x)
            {
                res.XList = addXToTheList(symbol, res.XList);
            }
        }

        private List<decimal> addXToTheList(Symbol symbol, List<decimal> xList)
        {
            if (symbol.Pow-1 < xList.Count) //already has a x in this pow
            {
                xList[symbol.Pow - 1] += symbol.Num;
            }
            else
            {
                while (symbol.Pow - 1 >= xList.Count)
                {
                    xList.Add(0);
                }
                xList[symbol.Pow - 1] += symbol.Num;
            }
            return xList;
        }

        #endregion

        #endregion

        #region check problems

        private bool checkForProblems(ref List<Symbol> symbols)
        {
            if (symbols.Count == 0)
            {
                return false;
            }
            //1
            //check if starts or ends with a math symbol
            if (symbols[0].Kind == Symbol.SymbolKinds.divide || symbols[0].Kind == Symbol.SymbolKinds.multiplay || symbols[symbols.Count - 1].Kind == Symbol.SymbolKinds.divide || symbols[symbols.Count - 1].Kind == Symbol.SymbolKinds.multiplay || symbols[symbols.Count - 1].Kind == Symbol.SymbolKinds.plus || symbols[symbols.Count - 1].Kind == Symbol.SymbolKinds.minus || symbols[symbols.Count - 1].Kind == Symbol.SymbolKinds.power)
            {
                return true;
            }

            //check if starts with plus or minus
            /*if (symbols[0].Kind == Symbol.SymbolKinds.plus)
            {
                symbols.RemoveAt(0);
                return checkForProblems(ref symbols);
            }
            else if (symbols[0].Kind == Symbol.SymbolKinds.minus)
            {
                symbols.RemoveAt(0);
                symbols[0].Num = -symbols[0].Num;
                return checkForProblems(ref symbols);
            }*/

            //
            //check if has an error symbol
            for (int i = 0; i <= symbols.Count - 1; i++)
            {
                if (symbols[i].Kind == Symbol.SymbolKinds.error)
                {
                    return true;
                }
            }

            //
            //check if has double symbols one after ather
            for (int i = 2; i <= symbols.Count - 2; i++)
            {
                if (checkIfSymbolIsntANumberOrPlusOrMinus(symbols[i]) && checkIfSymbolIsntANumberOrPlusOrMinus(symbols[i - 1]))
                {
                    return true;
                }
            }

            //check if has more parenthessis start or more parenthessis end
            int p = 0;
            for (int i = 0; i <= symbols.Count - 1; i++)
            {
                if (symbols[i].Kind == Symbol.SymbolKinds.parenthesisStart)
                {
                    p++;
                }
                else if (symbols[i].Kind == Symbol.SymbolKinds.parenthesisEnd)
                {
                    p--;
                    if (p < 0) // more parenthessis end
                    {
                        return true;
                    }
                    if (symbols[i - 1].Kind == Symbol.SymbolKinds.parenthesisStart) //check for empty parenthessis
                    {
                        return true;
                    }
                }
            }
            if (p > 0) //more parenthessis start
            {
                return true;
            }

            //set double plus and minus
            for (int i = 1; i <= symbols.Count - 1; i++)
            {
                if (symbols[i - 1].Kind == Symbol.SymbolKinds.minus && symbols[i].Kind == Symbol.SymbolKinds.minus)
                {
                    symbols[i - 1].Kind = Symbol.SymbolKinds.plus;
                    symbols.RemoveAt(i);
                    i--;
                }
                else if ((symbols[i - 1].Kind == Symbol.SymbolKinds.minus && symbols[i].Kind == Symbol.SymbolKinds.plus) || (symbols[i - 1].Kind == Symbol.SymbolKinds.plus && symbols[i].Kind == Symbol.SymbolKinds.minus))
                {
                    symbols[i - 1].Kind = Symbol.SymbolKinds.minus;
                    symbols.RemoveAt(i);
                    i--;
                }
                else if (symbols[i - 1].Kind == Symbol.SymbolKinds.plus && symbols[i].Kind == Symbol.SymbolKinds.plus)
                {
                    symbols.RemoveAt(i);
                    i--;
                }
            }

            //check for number right after end of parenthessis and empty parenthessis
            for (int i = 0; i <= symbols.Count - 2; i++)
            {
                if (symbols[i].Kind == Symbol.SymbolKinds.parenthesisEnd)
                {
                    if (symbols[i + 1].Kind == Symbol.SymbolKinds.number)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool checkIfSymbolIsntANumberOrPlusOrMinus(Symbol symbol)
        {
            if (symbol.Kind == Symbol.SymbolKinds.divide || symbol.Kind == Symbol.SymbolKinds.multiplay || symbol.Kind == Symbol.SymbolKinds.power)
            {
                return true;
            }
            return false;
        }

        private void wasOnError()
        {
            error = true;
        }

        #endregion

        #region convert from string to symbols

        public List<Symbol> getAListOfNumbersFromString(string text)
        {
            //remove all the spaces
            text = text.Replace(" ", "");
            //replace the funcs like root to r
            text = text.Replace("root", "r");

            //move an all the chars and check where a number starts and ends
            List<Symbol> res = new List<Symbol>();
            for (int i = 0; i <= text.Length - 1; i++)
            {
                res.Add(getSymbolFromText(text, ref i));
            }

            return res;
        }

        private Symbol getSymbolFromText(string text, ref int i)
        {
            string num = "" + text[i];
            
            Symbol.SymbolKinds kind = getKindOfChar(num[0]);
            if (kind == Symbol.SymbolKinds.x)
            {
                return new Symbol(kind, 1, 1);
            }
            else if (kind != Symbol.SymbolKinds.number)
            {
                return new Symbol(kind);
            }

            i++;
            //check where is the end of the number
            for (; i <= text.Length - 1; i++)
            {
                kind = getKindOfChar(text[i]);
                if (kind != Symbol.SymbolKinds.number)
                {
                    i--;
                    break;
                }
                num += text[i];
            }
            return new Symbol(Symbol.SymbolKinds.number, decimal.Parse(num));
        }

        private Symbol.SymbolKinds getKindOfChar(char c)
        {
            switch (c)
            {
                case '*':
                    return Symbol.SymbolKinds.multiplay;
                case '/':
                    return Symbol.SymbolKinds.divide;
                case '+':
                    return Symbol.SymbolKinds.plus;
                case '-':
                    return Symbol.SymbolKinds.minus;
                case '(':
                case '{':
                case '[':
                    return Symbol.SymbolKinds.parenthesisStart;
                case ')':
                case '}':
                case ']':
                    return Symbol.SymbolKinds.parenthesisEnd;
                case '^':
                    return Symbol.SymbolKinds.power;
                case 'x':
                    return Symbol.SymbolKinds.x;
                case 'r':
                    return Symbol.SymbolKinds.root;
                case ',':
                    return Symbol.SymbolKinds.comma;
                default:
                    if ((c < '0' || c > '9') && c != '.') //check if its a number
                    {
                        return Symbol.SymbolKinds.error;
                    }
                    else return Symbol.SymbolKinds.number;
            }
        }

        #endregion
    }
}
