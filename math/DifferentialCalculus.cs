using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace math
{
    internal class DifferentialCalculus
    {
        CalcExercise calc = new CalcExercise();

        public DifferentialCalculusRes getAllPointsInFunc(SmallEqualination func)
        {
            DifferentialCalculusRes res = new DifferentialCalculusRes();
            string way = "";
            EqualinationRes cutsWithX = calc.calcRegularEqualinationFromSmallEqualinations(func.Clone(), new SmallEqualination(), ref way);
            if (!cutsWithX.cantSolve)
            {
                for (int i = 0; i <= cutsWithX.Results.Count - 1; i++) //add all the cuts with x
                {
                    res.CutsWithX.Add(new PointF((float)cutsWithX.Results[i], 0));
                }
            }
            res.CutsWithXWay = way;

            //get the func definition domine
            way = "";
            List<FuncDomainDefinition> domainDefinition = getFuncDomineDefinition(func.Clone(), ref way);
            res.DomainDefinition = domainDefinition;
            res.DomainDefinitionWay = way;

            //get the asymatots with x and y
            res.AsymatotsWithX = getAsymatotsWithX(domainDefinition);

            way = "";
            bool hasAsymatot = true;
            decimal AsymatotWithY = getAsymatotWithY(func.Clone(), ref way, ref hasAsymatot);
            if (hasAsymatot)
            {
                res.AsymatotWithY = AsymatotWithY;
            }
            res.AsymatotWithYWay = way;

            way = "";
            if (calc.checkIfIsInTheDomainDefinition(res.DomainDefinition, 0))
            { 
                res.CutWithY = new PointF(0, (float)calc.putXInEqualination(func, 0, ref way));
            }


            findMinAndMaxPoints(func, ref res, ref way);

            return res;
        }

        private decimal getAsymatotWithY(SmallEqualination func, ref string way, ref bool hasAnAsymatot)
        {
            way += "\nlim (" + func.ToString() + ")";
            if (calc.getBiggestPowerFormXList(func.XList) != 0)
            {
                hasAnAsymatot = false;
                return 0;
            }
            //putt in all the devides the biggest power minus them and then calc the res
            for (int i = 0; i <= func.numbersAndX.Count - 1; i++)
            {
                if (func.numbersAndX[i].Type == NumbersAndX.NumbersAndXTypes.devide)
                {
                    Devide dev = (Devide)func.numbersAndX[i];
                    //get the biggest power in the func
                    int biggestPower = Math.Max(calc.getBiggestPowerFormXList(dev.Counter.XList), calc.getBiggestPowerFormXList(dev.Denominator.XList));
                    dev.Counter = devideSmallEqualinationWithBiggestPower(biggestPower, dev.Counter);
                    dev.Denominator = devideSmallEqualinationWithBiggestPower(biggestPower, dev.Denominator);
                }
            }
            List<Symbol> symbols = calc.getSymbolsFromSmallEqualination(func);
            SmallEqualination res = calc.getSmallerEqualinationFromSymbols(symbols, false, ref way);
            way = way.Remove(0, 1);
            way = way.Replace("\n", " = ");
            way = "\n" + way;
            way += "\nx -> \u221E";
            way += "\n y = " + calc.numberToString(res.Num);
            return res.Num;
        }

        private SmallEqualination devideSmallEqualinationWithBiggestPower(int biggestPower, SmallEqualination smallEqualination)
        {
            SmallEqualination res = new SmallEqualination();
            if (biggestPower == smallEqualination.XList.Count - 1)//the biggest power is in this small equalination
            {
                res.Num = smallEqualination.XList[biggestPower];
            }
            return res;
        }

        private List<decimal> getAsymatotsWithX(List<FuncDomainDefinition> domainDefinition)
        {
            List<decimal> res = new List<decimal>();
            //move an the domainDefinition list and what that is x!= somthing add to the asymatots with x list
            for (int i = 0; i <= domainDefinition.Count - 1; i++)
            {
                if (domainDefinition[i].Kind == FuncDomainDefinition.DomainDefinitionKinds.xNotEquals)
                {
                    res.Add(domainDefinition[i].X);
                }
            }
            return res;
        }

        private void findMinAndMaxPoints(SmallEqualination func, ref DifferentialCalculusRes res, ref string way)
        {
            way = "f '(x) = ";
            SmallEqualination cuttedFunc = cutFunc(func, ref way);
            res.CuttedFunc = cuttedFunc;
            way += cuttedFunc.ToString();

            EqualinationRes minAndMaxPoints = calc.calcRegularEqualinationFromSmallEqualinations(cuttedFunc.Clone(), new SmallEqualination(), ref way);
            //find witch point is min and witch one is max
            if (!minAndMaxPoints.cantSolve)
            {
                for (int i = 0; i <= minAndMaxPoints.Results.Count - 1; i++)
                {
                    string p = "";
                    way += "\n\nKind of: x = " + calc.numberToString(minAndMaxPoints.Results[i]);
                    way += "\nx | " + calc.numberToString((double)minAndMaxPoints.Results[i] - 0.1) + " | " + calc.numberToString((double)minAndMaxPoints.Results[i] + 0.1);
                    //find the before and after points
                    decimal before = calc.putXInEqualination(cuttedFunc, (double)minAndMaxPoints.Results[i] - 0.1, ref p);
                    decimal after = calc.putXInEqualination(cuttedFunc, (double)minAndMaxPoints.Results[i] + 0.1, ref p);
                    way += "\nf' |" + calc.numberToString(before) + " | " + calc.numberToString(after);

                    if (before > 0 && after < 0)
                    {
                        res.MaximumPoints.Add(new PointF((float)minAndMaxPoints.Results[i], (float)calc.putXInEqualination(func, (double)minAndMaxPoints.Results[i], ref p)));
                        way += "\nMax " + calc.pointFToString(res.MaximumPoints[res.MaximumPoints.Count - 1]);
                    }
                    else if (before < 0 && after > 0)
                    {
                        res.MinimumPoints.Add(new PointF((float)minAndMaxPoints.Results[i], (float)calc.putXInEqualination(func, (double)minAndMaxPoints.Results[i], ref p)));
                        way += "\nMin " + calc.pointFToString(res.MinimumPoints[res.MinimumPoints.Count - 1]);
                    }
                }
            }
            res.CuttedFuncAndMinimumMaximumWay = way;
        }

        private List<FuncDomainDefinition> getFuncDomineDefinition(SmallEqualination func, ref string way)
        {
            List<FuncDomainDefinition> res = new List<FuncDomainDefinition>();
            //move an the func x and numbers list and if has a devide find the domine definition of it
            for (int i = 0; i <= func.numbersAndX.Count - 1; i++)
            {
                if (func.numbersAndX[i].Type == NumbersAndX.NumbersAndXTypes.devide)
                {
                    Devide dev = (Devide)func.numbersAndX[i];
                    //add to way
                    way += "\n" + dev.Denominator.ToString() + " != 0";
                    //get the res of the denominator = 0 and the add it to the domain definition
                    EqualinationRes equalinationRes = calc.calcRegularEqualinationFromSmallEqualinations(dev.Denominator.Clone(), new SmallEqualination(), ref way);
                    if (!equalinationRes.cantSolve)
                    {
                        for (int j = 0; j <= equalinationRes.Results.Count - 1; j++)
                        {
                            res.Add(new FuncDomainDefinition(FuncDomainDefinition.DomainDefinitionKinds.xNotEquals, equalinationRes.Results[j]));
                            way += " \nx != " + calc.numberToString(equalinationRes.Results[j]);
                        }
                    }
                }
            }

            return res;
        }

        private SmallEqualination cutFunc(SmallEqualination func, ref string way)
        {
            if (func.XList.Count == 0 && func.numbersAndX.Count == 0)
            {
                return new SmallEqualination();
            }
            SmallEqualination res = new SmallEqualination();
            if (func.XList.Count != 0)
            {
                res.Num = func.XList[0]; //f'(2x) = 2

                //cut all the ather x numbers
                for (int i = 1; i <= func.XList.Count - 1; i++)
                {
                    res.XList.Add(0);
                    if (func.XList[i] != 0)
                    {
                        res.XList[i - 1] = func.XList[i] * (i + 1);
                    }
                }
            }

            //cut the numbers and x list
            for (int i = 0; i <= func.numbersAndX.Count - 1; i++)
            {
                if (func.numbersAndX[i].Type == NumbersAndX.NumbersAndXTypes.devide)
                {
                    Devide dev = (Devide)func.numbersAndX[i];
                    string way2 = "";
                    //cut the devide u/v = (u'*v-v'*u)/(v^2)
                    SmallEqualination cuttedU = cutFunc(dev.Counter.Clone(), ref way2);
                    SmallEqualination cuttedV = cutFunc(dev.Denominator.Clone(), ref way2);
                    List<Symbol> counter = new List<Symbol>();

                    //counter
                    //(u')*(v)
                    counter.Add(new Symbol(Symbol.SymbolKinds.parenthesisStart));
                    counter.AddRange(calc.getSymbolsFromSmallEqualination(cuttedU));
                    counter.Add(new Symbol(Symbol.SymbolKinds.parenthesisEnd));
                    counter.Add(new Symbol(Symbol.SymbolKinds.multiplay));
                    counter.Add(new Symbol(Symbol.SymbolKinds.parenthesisStart));
                    counter.AddRange(calc.getSymbolsFromSmallEqualination(dev.Denominator));
                    counter.Add(new Symbol(Symbol.SymbolKinds.parenthesisEnd));

                    //-(v')*(u)
                    counter.Add(new Symbol(Symbol.SymbolKinds.minus));
                    counter.Add(new Symbol(Symbol.SymbolKinds.parenthesisStart));
                    counter.AddRange(calc.getSymbolsFromSmallEqualination(cuttedV));
                    counter.Add(new Symbol(Symbol.SymbolKinds.parenthesisEnd));
                    counter.Add(new Symbol(Symbol.SymbolKinds.multiplay));
                    counter.Add(new Symbol(Symbol.SymbolKinds.parenthesisStart));
                    counter.AddRange(calc.getSymbolsFromSmallEqualination(dev.Counter));
                    counter.Add(new Symbol(Symbol.SymbolKinds.parenthesisEnd));

                    //denominator
                    List<Symbol> denominator = new List<Symbol>();
                    denominator.Add(new Symbol(Symbol.SymbolKinds.parenthesisStart));
                    denominator.AddRange(calc.getSymbolsFromSmallEqualination(dev.Denominator));
                    denominator.Add(new Symbol(Symbol.SymbolKinds.parenthesisEnd));
                    denominator.Add(new Symbol(Symbol.SymbolKinds.power));
                    denominator.Add(new Symbol(Symbol.SymbolKinds.number, 2));

                    way += calc.getStringFromListOfSymbols(counter) + " / " + calc.getStringFromListOfSymbols(denominator)+ "\nf '(x) = ";
                    dev.Counter = calc.getSmallerEqualinationFromSymbols(counter, false, ref way2);
                    //dev.Denominator = calc.getSmallerEqualinationFromSymbols(denominator, false, ref way2);
                    dev.Denominator = new SmallEqualination(0, new List<decimal>(), new List<NumbersAndX> { new NumbersAndX(denominator) });

                    func.numbersAndX[i] = dev;
                    res.numbersAndX.Add(dev);
                }
            }
            return res;
        }
    }
}