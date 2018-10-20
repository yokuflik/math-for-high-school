using System;
using System.Collections.Generic;

namespace math
{
    public class NumbersAndX
    {
        CalcExercise calc = new CalcExercise();
        public NumbersAndX()
        {
            Symbols = new List<Symbol>();
            Type = NumbersAndXTypes.regular;
        }

        public NumbersAndX(List<Symbol> symbols)
        {
            Symbols = symbols;
            Type = NumbersAndXTypes.regular;
        }

        public virtual NumbersAndX getMinus()
        {
            NumbersAndX numbersAndX2 = new NumbersAndX(Symbols);
            numbersAndX2.Symbols.Insert(0, new Symbol(Symbol.SymbolKinds.minus));
            numbersAndX2.Symbols.Insert(1, new Symbol(Symbol.SymbolKinds.parenthesisStart));
            numbersAndX2.Symbols.Add(new Symbol(Symbol.SymbolKinds.parenthesisEnd));
            return numbersAndX2;
        }

        public virtual List<Symbol> getSymbols()
        {
            return Symbols;
        }

        public virtual NumbersAndX Clone()
        {
            if (Type == NumbersAndXTypes.regular)
            {
                return new NumbersAndX(new List<Symbol>(Symbols));
            }
            return new NumbersAndX();
        }

        public virtual NumbersAndX multiplayWithSmallEqualination(SmallEqualination smallEqualination)
        {
            if (Type == NumbersAndXTypes.regular)
            {
                List<Symbol> symbols = smallEqualination.getSymbols();
                symbols.Insert(0, new Symbol(Symbol.SymbolKinds.parenthesisStart));
                symbols.Add(new Symbol(Symbol.SymbolKinds.parenthesisEnd));
                Symbols.Insert(0, new Symbol(Symbol.SymbolKinds.parenthesisStart));
                Symbols.Add(new Symbol(Symbol.SymbolKinds.parenthesisEnd));
                Symbols.InsertRange(0, symbols);
            }
            return this;
        }

        public override string ToString()
        {
            if (Type == NumbersAndXTypes.regular)
            {
                return calc.getStringFromListOfSymbols(Symbols);
            }
            return "";
        }

        public enum NumbersAndXTypes { regular, devide, root }
        public NumbersAndXTypes Type;

        public List<Symbol> Symbols { get; set; }

    }

    public class Devide : NumbersAndX
    {
        CalcExercise calc = new CalcExercise();
        public Devide()
        {
            Counter = new SmallEqualination();
            Denominator = new SmallEqualination();

            MinusBefore = false;
            base.Type = NumbersAndXTypes.devide;
        }

        public Devide(SmallEqualination counter, SmallEqualination denominator)
        {
            Counter = counter;
            Denominator = denominator;
            MinusBefore = false;
            base.Type = NumbersAndXTypes.devide;
        }

        public Devide(SmallEqualination counter, SmallEqualination denominator, bool minusBefore)
        {
            Counter = counter;
            Denominator = denominator;
            MinusBefore = minusBefore;
            base.Type = NumbersAndXTypes.devide;
        }

        public override NumbersAndX Clone()
        {
            Devide res = new Devide();
            res.Counter = Counter.Clone();
            res.Denominator = Denominator.Clone();
            res.MinusBefore = MinusBefore;
            res.Type = NumbersAndXTypes.devide;
            return res;
        }

        public override List<Symbol> getSymbols()
        {
            List<Symbol> res = new List<Symbol>();
            res.Add(new Symbol(Symbol.SymbolKinds.parenthesisStart));
            res.AddRange(Counter.getSymbols());
            res.Add(new Symbol(Symbol.SymbolKinds.parenthesisEnd));
            res.Add(new Symbol(Symbol.SymbolKinds.divide));
            res.Add(new Symbol(Symbol.SymbolKinds.parenthesisStart));
            res.AddRange(Denominator.getSymbols());
            res.Add(new Symbol(Symbol.SymbolKinds.parenthesisEnd));
            return res;
        }

        public override NumbersAndX getMinus()
        {
            Devide res = new Devide();
            res.Counter = Counter.getMinus();
            res.Denominator = Denominator.Clone();
            return res;
        }

        public override NumbersAndX multiplayWithSmallEqualination(SmallEqualination smallEqualination)
        {
            Counter = calc.multiplayTwoSmallEqualinations(Counter, smallEqualination);
            return calc.checkIfCanGetASmallerDevide((Devide)Clone());
        }

        public override string ToString()
        {
            string res = "";
            if (Counter.ToString() != "" && Denominator.ToString() != "")
            {
                res += "(" + Counter.ToString() + ") / (" + Denominator.ToString()+ ")";
            }
            return res;
        }
        
        public SmallEqualination Counter { get; set; }
        public SmallEqualination Denominator { get; set; }

        public bool MinusBefore { get; set; }
    }

    public class Root : NumbersAndX
    {
        CalcExercise calc = new CalcExercise();
        public Root()
        {
            Expression = new SmallEqualination();
            Multiplay = new SmallEqualination();
            Power = 1;
            Type = NumbersAndXTypes.root;
        }

        public Root(SmallEqualination expression,SmallEqualination multiplay, int power)
        {
            Expression = expression;
            Multiplay = multiplay;
            Power = power;
            Type = NumbersAndXTypes.root;
        }

        public override NumbersAndX Clone()
        {
            return new Root(Expression.Clone(), Multiplay.Clone(), Power);
        }

        public override List<Symbol> getSymbols()
        {
            List<Symbol> symbols = new List<Symbol>();
            symbols.Add(new Symbol(Symbol.SymbolKinds.parenthesisStart));
            symbols.AddRange(Multiplay.getSymbols());
            symbols.Add(new Symbol(Symbol.SymbolKinds.parenthesisEnd));
            symbols.Add(new Symbol(Symbol.SymbolKinds.root));
            symbols.Add(new Symbol(Symbol.SymbolKinds.parenthesisStart));
            symbols.AddRange(Expression.getSymbols());
            symbols.Add(new Symbol(Symbol.SymbolKinds.comma));
            symbols.Add(new Symbol(Symbol.SymbolKinds.number, Power));
            symbols.Add(new Symbol(Symbol.SymbolKinds.parenthesisEnd));
            return symbols;
        }

        public override NumbersAndX getMinus()
        {
            //returns -((multiplay)root)
            List<Symbol> symbols = new List<Symbol>();
            symbols.Add(new Symbol(Symbol.SymbolKinds.minus));
            symbols.Add(new Symbol(Symbol.SymbolKinds.parenthesisStart));
            symbols.AddRange(getSymbols());
            symbols.Add(new Symbol(Symbol.SymbolKinds.parenthesisEnd));
            return new NumbersAndX(symbols);
        }

        public override NumbersAndX multiplayWithSmallEqualination(SmallEqualination smallEqualination)
        {
            //check if the multiplay has also a root that is like it
            for (int i = 0; i <= smallEqualination.numbersAndX.Count - 1; i++)
            {
                if (smallEqualination.numbersAndX[i].Type == NumbersAndXTypes.root)
                {
                    Root root = (Root)smallEqualination.numbersAndX[i];
                    if (root.Expression == Expression && root.Power == Power)
                    {
                        smallEqualination.numbersAndX.RemoveAt(i);
                        List<Symbol> symbols = new List<Symbol>();
                        symbols.AddRange(smallEqualination.getSymbols(true));
                        symbols.AddRange(Multiplay.getSymbols(true));
                        symbols.AddRange(Expression.getSymbols(true));
                        return new NumbersAndX( symbols);
                    }
                }
            }
            Multiplay = calc.multiplayTwoSmallEqualinations(Multiplay, smallEqualination);
            return this;
        }
        
        public override string ToString()
        {
            string res = "";
            if (Multiplay.ToString() != "1")
            {
                res += "("+Multiplay.ToString() + ") * ";
            }
            res += "root(" + Expression.ToString() + ", " + Power.ToString() + ")";
            return res;
        }

        public SmallEqualination Expression { get; set; }
        public SmallEqualination Multiplay { get; set; }
        public int Power { get; set; }
    }
}