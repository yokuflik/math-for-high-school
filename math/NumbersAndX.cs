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

        public virtual NumbersAndX getMinus(NumbersAndX numbersAndX)
        {
            if (numbersAndX.Type == NumbersAndXTypes.devide)
            {
                Devide numbersAndX2 = (Devide)numbersAndX;
                numbersAndX2.MinusBefore = true;
                return numbersAndX2;
            }
            else if (numbersAndX.Type == NumbersAndXTypes.regular)
            {
                NumbersAndX numbersAndX2 = new NumbersAndX(numbersAndX.Symbols);
                numbersAndX2.Symbols.Insert(0, new Symbol(Symbol.SymbolKinds.minus));
                numbersAndX2.Symbols.Insert(1, new Symbol(Symbol.SymbolKinds.parenthesisStart));
                numbersAndX2.Symbols.Add(new Symbol(Symbol.SymbolKinds.parenthesisEnd));
            }
            return null;
        }

        public virtual List<Symbol> getSymbols(NumbersAndX numbersAndX)
        {
            if (numbersAndX.Type == NumbersAndXTypes.devide)
            {
                CalcExercise calc = new CalcExercise();
                Devide dev = (Devide)numbersAndX;
                List<Symbol> res = new List<Symbol>();
                res.Add(new Symbol(Symbol.SymbolKinds.parenthesisStart));
                res.AddRange(calc.getSymbolsFromSmallEqualination(dev.Counter));
                res.Add(new Symbol(Symbol.SymbolKinds.parenthesisEnd));
                res.Add(new Symbol(Symbol.SymbolKinds.divide));
                res.Add(new Symbol(Symbol.SymbolKinds.parenthesisStart));
                res.AddRange(calc.getSymbolsFromSmallEqualination(dev.Denominator));
                res.Add(new Symbol(Symbol.SymbolKinds.parenthesisEnd));
                return res;
            }
            return null;
        }

        public virtual NumbersAndX Clone()
        {
            if (Type == NumbersAndXTypes.regular)
            {
                return new NumbersAndX(new List<Symbol>(Symbols));
            }
            return new NumbersAndX();
        }

        public override string ToString()
        {
            if (Type == NumbersAndXTypes.regular)
            {
                return calc.getStringFromListOfSymbols(Symbols);
            }
            return "";
        }

        public enum NumbersAndXTypes { regular, devide}
        public NumbersAndXTypes Type;

        public List<Symbol> Symbols { get; set; }

    }

    public class Devide : NumbersAndX
    {
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
}