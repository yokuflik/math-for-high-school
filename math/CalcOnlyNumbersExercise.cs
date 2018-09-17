using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace math
{
    class CalcExercise
    {
        public bool error { get; set; }

        public EqualinationRes calcEqualination(string text)
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
                return calcRegularEqualinationFromText(sideOneText, sideTwoText);
            }
            else
            {
                return getSmallerEqualinationFromText(text);
            }
        }

        public EqualinationRes calcRegularEqualinationFromText(string sideOne, string sideTwo)
        {
            SmallEqualination sideOneSymbols = getSmallerEqualinationFromText(sideOne);
            SmallEqualination sideTwoSymbols = getSmallerEqualinationFromText(sideTwo);
            return calcRegularEqualinationFromSymbols(sideOneSymbols, sideTwoSymbols);
        }

        private EqualinationRes calcRegularEqualinationFromSymbols(SmallEqualination sideOne, SmallEqualination sideTwo)
        {
            SmallEqualination smallRes = subtractTwoSmallEqualinations(sideOne, sideTwo);
            //check what kind of equalination is it
            EqualinationRes res = new EqualinationRes();
            if (smallRes.XList.Count == 1) //regular equlination
            {
                res.Results.Add((-smallRes.Num) / smallRes.XList[0]);
            }
            else if (smallRes.XList.Count == 2)
            {
                return rootFormula(smallRes.XList[1], smallRes.XList[0], smallRes.Num);
            }
            else
            {
                res.cantSolve = true;
            }
            return res;
        }

        private SmallEqualination subtractTwoSmallEqualinations(SmallEqualination one, SmallEqualination two)
        {
            if (one.XList.Count > two.XList.Count)
            {
                one.Num -= two.Num;
                for (int i =0;i<=two.XList.Count - 1; i++)
                {
                    one.XList[i] -= two.XList[i];
                }
                return one;
            }
            else
            {
                two.Num -= one.Num;
                for (int i = 0; i <= one.XList.Count - 1; i++)
                {
                    two.XList[i] -= one.XList[i];
                }
                return two;
            }
        }

        private EqualinationRes rootFormula(decimal a, decimal b, decimal c)
        {
            EqualinationRes res = new EqualinationRes();
            //check if b^2-4ac isnt below zero
            decimal root = (decimal)Math.Sqrt((double)(b * b - 4 * a * c));
            if (root < 0)
            {
                return res;
            }
            else if (root == 0)
            {
                res.Results.Add(-b / (2 * a));
            }
            else
            {
                res.Results.Add((-b + root) / (2 * a));
                res.Results.Add((-b - root) / (2 * a));
            }
            return res;
        }

        public SmallEqualination getSmallerEqualinationFromText(string text)
        {
            error = false;
            //start solving

            //get a list of the numbers from the string
            List<Symbol> symbols = getAListOfNumbersFromString(text);

            //calc the list of numbers
            SmallEqualination res = getSmallerEqualinationFromSymbols(symbols, true);

            //show the answer
            return res;
        }

        public SmallEqualination getSmallerEqualinationFromSymbols(List<Symbol> symbols, bool checkProblems)
        {
            //check if the list has problems
            if (checkProblems && checkForProblems(ref symbols))
            {
                wasOnError();
                return new SmallEqualination(0, new List<decimal>());
            }

            //calc the list of numbers
            return startCalcing(symbols);
        }

        #region calcing

        private SmallEqualination startCalcing(List<Symbol> symbols)
        {
            if (symbols.Count == 0)
            {
                return new SmallEqualination(0, new List<decimal>());
            }
            //1
            //calc the parenthessis
            calcParenthessis(ref symbols);

            //2
            //calc the multiplay and divide
            calcPowerAndMultiplayAndDivide(ref symbols);

            //3
            //calc the plus and minus
            return calcPlusAndMinus(symbols);
        }

        #region parenthessis

        private void calcParenthessis(ref List<Symbol> symbols)
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
                                //calc the parenthessis and before and after multiplay
                                SmallEqualination res = getSmallerEqualinationFromSymbols(symbols.GetRange(start + 1, count), false);

                                end = start + count + 2;
                                res = multiplayTwoSmallEqualinations(res, getBeforeAndAfterOnePart(symbols, ref start, ref end));

                                //remove all the parenthessis and put the res
                                symbols.RemoveRange(start, end - start);

                                //putt the res
                                //symbols[start].Kind = Symbol.SymbolKinds.number;
                                //symbols[start].Num = res;
                                symbols.InsertRange(start, getSymbolsFromSmallEqualination(res));
                                
                                j = symbols.Count - 1;
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

        public List<Symbol> getSymbolsFromSmallEqualination(SmallEqualination smallEqualination)
        {
            List<Symbol> res = new List<Symbol>();

            //add the number
            if (smallEqualination.Num != 0)
            {
                res.Add(new Symbol(Symbol.SymbolKinds.number, smallEqualination.Num));
            }

            //add the x list
            for (int i = 0; i <= smallEqualination.XList.Count - 1; i++)
            {
                if (smallEqualination.XList[i] != 0)
                {
                    if (res.Count != 0)
                    {
                        res.Add(new Symbol(Symbol.SymbolKinds.plus));
                    }
                    res.Add(new Symbol(Symbol.SymbolKinds.x, smallEqualination.XList[i], i + 1));
                }
            }

            return res;
        }

        private SmallEqualination getBeforeAndAfterOnePart(List<Symbol> symbols, ref int startPIndex, ref int endPIndex)
        {
            SmallEqualination res = new SmallEqualination(1, new List<decimal>());
            res = multiplayTwoSmallEqualinations(res, getBeforeIndexOnePart(symbols, ref startPIndex));
            res = multiplayTwoSmallEqualinations(res, getAfterIndexOnePart(symbols, ref endPIndex));
            return res;
        }

        private SmallEqualination getBeforeIndexOnePart(List<Symbol> symbols, ref int startPIndex)
        {
            if (startPIndex == 0)
            {
                return new SmallEqualination(1, new List<decimal>());
            }

            //startPIndex--;
            SmallEqualination res = new SmallEqualination(1, new List<decimal>());

            /*if (symbols[startPIndex].Kind == Symbol.SymbolKinds.number)
            {
                res.Num = symbols[startPIndex].Num;
            }
            else if (symbols[startPIndex].Kind == Symbol.SymbolKinds.x)
            {
                res.XList = addXToTheList(symbols[startPIndex], new List<decimal>());
            }*/

            int count = 0;
            int parenthessis = 0;
            int addSymbol = 0;
            //get the before numbers
            for (; startPIndex > 0; startPIndex--)
            {
                if (symbols[startPIndex].Kind == Symbol.SymbolKinds.parenthesisEnd)
                {
                    parenthessis++;
                }
                else if (symbols[startPIndex].Kind == Symbol.SymbolKinds.parenthesisStart)
                {
                    parenthessis--;
                }
                else if (parenthessis == 0)
                {
                    if (symbols[startPIndex].Kind == Symbol.SymbolKinds.minus)
                    {
                        addSymbol = 1;
                        startPIndex++;
                        break;
                    }
                    else if (symbols[startPIndex].Kind == Symbol.SymbolKinds.plus || symbols[startPIndex].Kind == Symbol.SymbolKinds.divide)
                    {
                        startPIndex++;
                        break;
                    }
                }
                count++;
            }
            if (count > 0)
            {
                SmallEqualination smallEqualinationRes = getSmallerEqualinationFromSymbols(symbols.GetRange(startPIndex-addSymbol, count+addSymbol), false);
                res = multiplayTwoSmallEqualinations(res, smallEqualinationRes);
            }
            return res;
        }

        private SmallEqualination getAfterIndexOnePart(List<Symbol> symbols, ref int endPIndex)
        {
            if (endPIndex >= symbols.Count - 1)
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
                }
                else if (parenthessis == 0 && (symbols[endPIndex].Kind == Symbol.SymbolKinds.minus || symbols[endPIndex].Kind == Symbol.SymbolKinds.plus || symbols[endPIndex].Kind == Symbol.SymbolKinds.divide || symbols[endPIndex].Kind == Symbol.SymbolKinds.power))
                {
                    break;
                }
                count++;
            }
            if (count > 0)
            {
                SmallEqualination smallEqualinationRes = getSmallerEqualinationFromSymbols(symbols.GetRange(endPIndex - count, count), false);
                res = multiplayTwoSmallEqualinations(res, smallEqualinationRes);
            }
            return res;
        }
        
        private SmallEqualination multiplayTwoSmallEqualinations(SmallEqualination one, SmallEqualination two)
        {
            List<Symbol> res = new List<Symbol>();

            List<Symbol> oneSymbol = getSymbolsFromSmallEqualination(one);
            List<Symbol> twoSymbol = getSymbolsFromSmallEqualination(two);

            for (int i = 0; i <= oneSymbol.Count - 1; i++)
            {
                for (int j = 0; j <= twoSymbol.Count - 1; j++)
                {
                    if (res.Count != 0)
                    {
                        res.Add(new Symbol(Symbol.SymbolKinds.plus));
                    }
                    res.Add(getSymbolFromMultiplay(oneSymbol[i], twoSymbol[j]));
                }
            }

            return calcPlusAndMinus(res);
        }

        #endregion

        #region power multiplay and devide

        private void calcPowerAndMultiplayAndDivide(ref List<Symbol> symbols)
        {
            //calc power
            for (int i = 1; i <= symbols.Count - 2; i++)
            {
                if (symbols[i].Kind == Symbol.SymbolKinds.power)
                {
                    if (symbols[i + 1].Kind == Symbol.SymbolKinds.plus)
                    {
                        symbols[i - 1] = getSymbolFromPower(symbols[i - 1], symbols[i + 2]);
                        symbols.RemoveRange(i, 3);
                    }
                    else if (symbols[i + 1].Kind == Symbol.SymbolKinds.minus)
                    {
                        symbols[i - 1] = getSymbolFromPower(symbols[i - 1], getMinusNumSymbol(symbols[i + 2]));
                        symbols.RemoveRange(i, 3);
                    }
                    else
                    {
                        symbols[i - 1] = getSymbolFromPower(symbols[i - 1], symbols[i + 1]);
                        symbols.RemoveRange(i, 2);
                    }
                    i--;
                }
            }

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
                    if (symbols[i+secondNum].Num == 0)
                    {
                        error = true;
                    }
                    int startIndex = 0;
                    int endIndex = 0;
                    //List<Symbol> devideRes = getDevideSymbols(symbols, i, ref startIndex, ref endIndex);
                    symbols[i - 1].Num /= symbols[i + secondNum].Num;
                    symbols.RemoveRange(i-1, secondNum + 2);
                    i--;
                    
                }
            }
        }

        private List<Symbol> getDevideSymbols(List<Symbol> symbols, int i, ref int startIndex, ref int endIndex)
        {
            List<Symbol> res = new List<Symbol>();

            //get the before index
            res.Add(new Symbol(Symbol.SymbolKinds.parenthesisStart));
            startIndex = i-1;
            SmallEqualination beforeRes = getBeforeIndexOnePart(symbols, ref startIndex);
            addSmallEqualinationToListSymbols(ref res, beforeRes);
            res.Add(new Symbol(Symbol.SymbolKinds.parenthesisEnd));
            
            res.Add(new Symbol(Symbol.SymbolKinds.divide));

            //get the after index
            res.Add(new Symbol(Symbol.SymbolKinds.parenthesisStart));
            endIndex = i +1;
            SmallEqualination afterRes = getBeforeIndexOnePart(symbols, ref endIndex);
            addSmallEqualinationToListSymbols(ref res, afterRes);
            res.Add(new Symbol(Symbol.SymbolKinds.parenthesisEnd));

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

        private SmallEqualination calcPlusAndMinus(List<Symbol> symbols)
        {
            SmallEqualination res = new SmallEqualination();
            if (symbols.Count == 0)
            {
                return res;
            }
            //check for minus or plus in the start
            if (symbols[0].Kind == Symbol.SymbolKinds.minus)
            {
                symbols[1].Num = -symbols[1].Num;
                symbols.RemoveAt(0);
            }
            else if (symbols[0].Kind == Symbol.SymbolKinds.plus)
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
            return res;
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
                xList[(int)symbol.Pow - 1] += symbol.Num;
            }
            else
            {
                while (symbol.Pow - 1 >= xList.Count)
                {
                    xList.Add(0);
                }
                xList[(int)symbol.Pow - 1] += symbol.Num;
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
            //1
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

            Symbol.SymbolKinds kind;
            kind = getKindOfChar(num[num.Length - 1]);
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
                    return Symbol.SymbolKinds.parenthesisStart;
                case ')':
                    return Symbol.SymbolKinds.parenthesisEnd;
                case '^':
                    return Symbol.SymbolKinds.power;
                case 'x':
                    return Symbol.SymbolKinds.x;
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
