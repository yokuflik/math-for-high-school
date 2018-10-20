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
            res.Func = func;
            string way = "";

            //get the func domain definition
            List<FuncDomainDefinition> domainDefinition = getFuncDomineDefinition(func.Clone(), ref way);
            res.DomainDefinition = domainDefinition;
            way = calc.removeDuplicateLinesFromWay(way);
            res.DomainDefinitionWay = way;

            //get the asymatots with x and y
            res.AsymatotsWithX = getAsymatotsWithX(domainDefinition);

            way = "";
            bool hasAsymatot = true;
            decimal AsymatotWithY = getAsymatotWithY(func.Clone(), ref way, ref hasAsymatot);
            res.HasAsymatotWithY = hasAsymatot;
            if (hasAsymatot)
            {
                res.AsymatotWithY = AsymatotWithY;
            }
            way = calc.removeDuplicateLinesFromWay(way);
            res.AsymatotWithYWay = way;

            way = "";
            if (calc.checkIfIsInTheDomainDefinition(res.DomainDefinition, 0))
            { 
                res.CutWithY = new PointF(0, (float)calc.putXInEqualination(func, 0, ref way));
            }

            way = "";
            EqualinationRes cutsWithX = calc.calcRegularEqualinationFromSmallEqualinations(func.Clone(), new SmallEqualination(), ref way);
            if (!cutsWithX.cantSolve)
            {
                for (int i = 0; i <= cutsWithX.Results.Count - 1; i++) //add all the cuts with x
                {
                    if (calc.checkIfIsInTheDomainDefinition(res.DomainDefinition, cutsWithX.Results[i]))
                    {
                        res.CutsWithX.Add(new PointF((float)cutsWithX.Results[i], 0));
                    }
                    else
                    {
                        way += "\nx = " + calc.numberToString(cutsWithX.Results[i]) + " - Isn't in the domain definition";
                    }
                }
            }
            way = calc.removeDuplicateLinesFromWay(way);
            res.CutsWithXWay = way;

            findMinAndMaxPoints(func, ref res, ref way);

            return res;
        }

        private decimal getAsymatotWithY(SmallEqualination func, ref string way, ref bool hasAnAsymatot)
        {
            way += "\nlim (" + func.ToString() + ")";
            func.setXList();
            if ((func.XList.Count == 0 && func.numbersAndX.Count == 0)||func.XList.Count>0)
            {
                hasAnAsymatot = false;
                return 0;
            }
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
                    dev.Counter.setXList();
                    dev.Denominator.setXList();
                    if (dev.Counter.XList.Count > dev.Denominator.XList.Count)
                    {
                        hasAnAsymatot = false;
                        return 0;
                    }
                    //get the biggest power in the func
                    int biggestPower = Math.Max(calc.getBiggestPowerFormXList(dev.Counter.XList), calc.getBiggestPowerFormXList(dev.Denominator.XList));
                    dev.Counter = devideSmallEqualinationWithBiggestPower(biggestPower, dev.Counter);
                    dev.Denominator = devideSmallEqualinationWithBiggestPower(biggestPower, dev.Denominator);
                }
            }
            List<Symbol> symbols = func.getSymbols();
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
            SmallEqualination cuttedFunc = cutFunc(func.Clone(), ref way);
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
                    way += "\nx  | " + calc.numberToString((double)minAndMaxPoints.Results[i] - 0.01) + " | " + calc.numberToString((double)minAndMaxPoints.Results[i] + 0.01);
                    //find the before and after points
                    decimal before = calc.putXInEqualination(cuttedFunc, (double)minAndMaxPoints.Results[i] - 0.01, ref p);
                    decimal after = calc.putXInEqualination(cuttedFunc, (double)minAndMaxPoints.Results[i] + 0.01, ref p);
                    way += "\nf ' |" + calc.numberToString(before) + " | " + calc.numberToString(after);

                    if (before > 0 && after < 0)
                    {
                        PointF pF = new PointF((float)minAndMaxPoints.Results[i], (float)calc.putXInEqualination(func, (double)minAndMaxPoints.Results[i], ref p));
                        way += "\nMax " + calc.pointFToString(pF);
                        if (calc.checkIfIsInTheDomainDefinition(res.DomainDefinition, minAndMaxPoints.Results[i]))
                        {
                            res.MaximumPoints.Add(pF);
                        }
                        else
                        {
                            way += " - Isn't in the domain definition";
                        }

                    }
                    else if (before < 0 && after > 0)
                    {
                        PointF pF = new PointF((float)minAndMaxPoints.Results[i], (float)calc.putXInEqualination(func, (double)minAndMaxPoints.Results[i], ref p));
                        way += "\nMin " + calc.pointFToString(pF);
                        if (calc.checkIfIsInTheDomainDefinition(res.DomainDefinition, minAndMaxPoints.Results[i]))
                        {
                            res.MinimumPoints.Add(pF);
                        }
                        else
                        {
                            way += " - Isn't in the domain definition";
                        }
                    }
                }
            }
            way = calc.removeDuplicateLinesFromWay(way);
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
                            FuncDomainDefinition fdd = new FuncDomainDefinition(FuncDomainDefinition.DomainDefinitionKinds.xNotEquals, equalinationRes.Results[j]);
                            if (!res.Contains(fdd))
                            {
                                res.Add(fdd);
                            }
                            way += "\nx != " + calc.numberToString(equalinationRes.Results[j]);
                        }
                    }

                    //add the devide denominator func domain definition
                    res.AddRange(getFuncDomineDefinition(dev.Denominator.Clone(), ref way));
                }
                else if(func.numbersAndX[i].Type == NumbersAndX.NumbersAndXTypes.root)
                {
                    Root root = (Root)func.numbersAndX[i];
                    //add to way
                    way += "\n" + root.Expression.ToString() + " >= 0";
                    res.AddRange(calc.calcNotEqualsEqualinationFromSmallEqualination(root.Expression.Clone()));
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
                    res.numbersAndX.Add(cutDevide(dev, ref way));
                }
                else if (func.numbersAndX[i].Type == NumbersAndX.NumbersAndXTypes.root)
                {
                    Root root = (Root)func.numbersAndX[i];
                    res.numbersAndX.Add(cutRoot(root, ref way));
                }
            }
            return res;
        }

        private NumbersAndX cutRoot(Root root, ref string way)
        {
            NumbersAndX res = new NumbersAndX();
            string way2 = "";

            //add the root multiplay if isn't 1
            if (root.Multiplay.ToString() != "1")
            {
                //multiplay'*root
                res.Symbols.Add(new Symbol(Symbol.SymbolKinds.parenthesisStart));
                res.Symbols.AddRange(cutFunc(root.Multiplay.Clone(), ref way2).getSymbols());
                res.Symbols.Add(new Symbol(Symbol.SymbolKinds.parenthesisEnd));
                res.Symbols.Add(new Symbol(Symbol.SymbolKinds.root));
                res.Symbols.Add(new Symbol(Symbol.SymbolKinds.parenthesisStart));
                res.Symbols.AddRange(root.Expression.getSymbols());
                res.Symbols.Add(new Symbol(Symbol.SymbolKinds.comma));
                res.Symbols.Add(new Symbol(Symbol.SymbolKinds.number, 2));
                res.Symbols.Add(new Symbol(Symbol.SymbolKinds.parenthesisEnd));
                res.Symbols.Add(new Symbol(Symbol.SymbolKinds.plus));
            }

            //add the root cut
            //multiplay' * root' / 2*root
            SmallEqualination cuttedMultiplay = cutFunc(root.Multiplay.Clone(), ref way2);
            if (cuttedMultiplay.ToString() != "0")
            {
                res.Symbols.Add(new Symbol(Symbol.SymbolKinds.parenthesisStart));
                res.Symbols.AddRange(cuttedMultiplay.getSymbols());
                res.Symbols.Add(new Symbol(Symbol.SymbolKinds.parenthesisEnd));
            }

            res.Symbols.Add(new Symbol(Symbol.SymbolKinds.parenthesisStart));
            res.Symbols.AddRange(cutFunc(root.Expression, ref way2).getSymbols());
            res.Symbols.Add(new Symbol(Symbol.SymbolKinds.parenthesisEnd));
            res.Symbols.Add(new Symbol(Symbol.SymbolKinds.divide));
            res.Symbols.Add(new Symbol(Symbol.SymbolKinds.number, 2));
            res.Symbols.Add(new Symbol(Symbol.SymbolKinds.multiplay));
            res.Symbols.Add(new Symbol(Symbol.SymbolKinds.root));
            res.Symbols.Add(new Symbol(Symbol.SymbolKinds.parenthesisStart));
            res.Symbols.AddRange(root.Expression.getSymbols());
            res.Symbols.Add(new Symbol(Symbol.SymbolKinds.comma));
            res.Symbols.Add(new Symbol(Symbol.SymbolKinds.number, 2));
            res.Symbols.Add(new Symbol(Symbol.SymbolKinds.parenthesisEnd));

            return res;
        }

        private NumbersAndX cutDevide(Devide dev, ref string way)
        {
            string way2 = "";
            //cut the devide u/v = (u'*v-v'*u)/(v^2)
            SmallEqualination cuttedU = cutFunc(dev.Counter.Clone(), ref way2);
            SmallEqualination cuttedV = cutFunc(dev.Denominator.Clone(), ref way2);
            List<Symbol> counter = new List<Symbol>();

            //counter
            //(u')*(v)
            counter.Add(new Symbol(Symbol.SymbolKinds.parenthesisStart));
            counter.AddRange(cuttedU.getSymbols());
            counter.Add(new Symbol(Symbol.SymbolKinds.parenthesisEnd));
            counter.Add(new Symbol(Symbol.SymbolKinds.multiplay));
            counter.Add(new Symbol(Symbol.SymbolKinds.parenthesisStart));
            counter.AddRange(dev.Denominator.getSymbols());
            counter.Add(new Symbol(Symbol.SymbolKinds.parenthesisEnd));

            //-(v')*(u)
            counter.Add(new Symbol(Symbol.SymbolKinds.minus));
            counter.Add(new Symbol(Symbol.SymbolKinds.parenthesisStart));
            counter.AddRange(cuttedV.getSymbols());
            counter.Add(new Symbol(Symbol.SymbolKinds.parenthesisEnd));
            counter.Add(new Symbol(Symbol.SymbolKinds.multiplay));
            counter.Add(new Symbol(Symbol.SymbolKinds.parenthesisStart));
            counter.AddRange(dev.Counter.getSymbols());
            counter.Add(new Symbol(Symbol.SymbolKinds.parenthesisEnd));

            //denominator
            List<Symbol> denominator = new List<Symbol>();
            denominator.Add(new Symbol(Symbol.SymbolKinds.parenthesisStart));
            denominator.AddRange(dev.Denominator.getSymbols());
            denominator.Add(new Symbol(Symbol.SymbolKinds.parenthesisEnd));
            denominator.Add(new Symbol(Symbol.SymbolKinds.power));
            denominator.Add(new Symbol(Symbol.SymbolKinds.number, 2));

            way += calc.getStringFromListOfSymbols(counter) + " / " + calc.getStringFromListOfSymbols(denominator) + "\nf '(x) = ";
            dev.Counter = calc.getSmallerEqualinationFromSymbols(counter, false, ref way2);
            //dev.Denominator = calc.getSmallerEqualinationFromSymbols(denominator, false, ref way2);
            dev.Denominator = new SmallEqualination(0, new List<decimal>(), new List<NumbersAndX> { new NumbersAndX(denominator) });
            return dev;
        }

        public List<FuncDomainDefinition> getPositiveDomain(SmallEqualination func, bool equals = true)
        {
            //get the cuts with x
            DifferentialCalculusRes allPointsOnFunc = getAllPointsInFunc(func);

            //for now it can give the positive domain without domain definition

            List<FuncDomainDefinition> res = new List<FuncDomainDefinition>();

            string way = "";
            if (allPointsOnFunc.CutsWithX.Count == 0)
            {
                if (calc.putXInEqualination(func.Clone(), 0, ref way) < 0)
                {
                    res.Add(new FuncDomainDefinition(FuncDomainDefinition.DomainDefinitionKinds.allX, false));
                }
                else
                {
                    res.Add(new FuncDomainDefinition(FuncDomainDefinition.DomainDefinitionKinds.allX, true));
                }
            }
            else
            {
                //sort the x list
                for (int i = 0; i <= allPointsOnFunc.CutsWithX.Count - 1; i++)
                {
                    for (int j = i; j <= allPointsOnFunc.CutsWithX.Count - 1; j++)
                    {
                        if (allPointsOnFunc.CutsWithX[j].X < allPointsOnFunc.CutsWithX[i].X)
                        {
                            PointF p = allPointsOnFunc.CutsWithX[j];
                            allPointsOnFunc.CutsWithX[j] = allPointsOnFunc.CutsWithX[i];
                            allPointsOnFunc.CutsWithX[i] = p;
                        }
                    }
                }

                //add the before of the first x
                bool positive;
                if (calc.putXInEqualination(func.Clone(), allPointsOnFunc.CutsWithX[0].X-0.001, ref way) < 0)//the first x is positive
                {
                    positive = false;
                }
                else
                {
                    positive = true;
                    res.Add(new FuncDomainDefinition(FuncDomainDefinition.DomainDefinitionKinds.xSmallThen, (decimal)allPointsOnFunc.CutsWithX[0].X, equals));
                }

                for (int i =1;i<= allPointsOnFunc.CutsWithX.Count - 1; i++)
                {
                    positive = !positive;
                    if (positive)
                    {
                        res.Add(new FuncDomainDefinition(FuncDomainDefinition.DomainDefinitionKinds.xBetweenThen, FuncDomainDefinition.XBetweenKinds.equalsToAll, (decimal)allPointsOnFunc.CutsWithX[i - 1].X, (decimal)allPointsOnFunc.CutsWithX[i].X));
                    }
                }

                //add the after of last x
                if (!positive)
                {
                    res.Add(new FuncDomainDefinition(FuncDomainDefinition.DomainDefinitionKinds.xBigThen, (decimal)allPointsOnFunc.CutsWithX[allPointsOnFunc.CutsWithX.Count-1].X, equals));
                }
            }

            return res;

            /*allPointsOnFunc.setDomainDefinition(); //sets the list like x<3, x>5
            List<decimal> xInAnyDomain = getXFromAnyDomain(allPointsOnFunc.DomainDefinition);

            //calc all the domains
            List<FuncDomainDefinition> res = new List<FuncDomainDefinition>();
            for (int i =0;i<= allPointsOnFunc.DomainDefinition.Count - 1; i++)
            {
                if (allPointsOnFunc.DomainDefinition[i].Kind == FuncDomainDefinition.DomainDefinitionKinds.xNotEquals)
                {

                }
            }
            return res;*/
        }

        public List<decimal> getXFromAnyDomain(List<FuncDomainDefinition> domainDefinition)
        {
            List<decimal> res = new List<decimal>();

            decimal numToAdd = 0;
            bool add = false;
            bool addWhenBig = false;

            if (domainDefinition.Count == 0)
            {
                return res;
            }
            if (domainDefinition[0].Kind == FuncDomainDefinition.DomainDefinitionKinds.xNotEquals)
            {
                res.Add(domainDefinition[0].X - (decimal)0.01);
                res.Add(domainDefinition[0].X + (decimal)0.01);
            }
            else if (domainDefinition[0].Kind == FuncDomainDefinition.DomainDefinitionKinds.xSmallThen)
            {
                res.Add(domainDefinition[0].X - (decimal)0.01);
                add = true;
                numToAdd = domainDefinition[0].X + (decimal)0.01;
            }
            else if (domainDefinition[0].Kind == FuncDomainDefinition.DomainDefinitionKinds.xBigThen)
            {
                res.Add(domainDefinition[0].X + (decimal)0.01);
            }
            else if (domainDefinition[0].Kind == FuncDomainDefinition.DomainDefinitionKinds.xBetweenThen)
            {
                res.Add(domainDefinition[0].SmallerX + (decimal)0.01);
                add = true;
                numToAdd = domainDefinition[0].BiggerX + (decimal)0.01;
            }
            //move an the list and add all the after domain x
            for (int i = 1; i <= domainDefinition.Count - 1; i++)
            {
                if (add) //add a number after the last num if has anathor num after it
                {
                    add = false;
                    res.Add(numToAdd);
                }
                if (domainDefinition[i].Kind == FuncDomainDefinition.DomainDefinitionKinds.xNotEquals)
                {
                    res.Add(domainDefinition[i].X + (decimal)0.01);
                }
                else if (domainDefinition[i].Kind == FuncDomainDefinition.DomainDefinitionKinds.xBigThen)
                {
                    res.Add(domainDefinition[i].X + (decimal)0.01);
                }
                else if (domainDefinition[i].Kind == FuncDomainDefinition.DomainDefinitionKinds.xBetweenThen)
                {
                    res.Add(domainDefinition[i].SmallerX + (decimal)0.01);
                    res.Add(domainDefinition[i].BiggerX + (decimal)0.01);
                }
            }
            return res;
        }
    }
}