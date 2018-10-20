namespace math
{
    public class Symbol
    {
        CalcExercise calc = new CalcExercise();
        public Symbol(SymbolKinds kind)
        {
            Kind = kind;
            Num = 0;
            Pow = 1;
        }

        public Symbol(SymbolKinds kind, decimal num)
        {
            Kind = kind;
            Num = num;
            Pow = 1;
        }

        public Symbol(SymbolKinds kind, decimal num, int pow)
        {
            Kind = kind;
            Num = num;
            Pow = pow;
        }

        public override string ToString()
        {
            switch (Kind)
            {
                case SymbolKinds.error:
                    return "error";
                case SymbolKinds.multiplay:
                    return "*";
                case SymbolKinds.divide:
                    return "/";
                case SymbolKinds.plus:
                    return "+";
                case SymbolKinds.minus:
                    return "-";
                case SymbolKinds.power:
                    return "^";
                case SymbolKinds.parenthesisStart:
                    return "(";
                case SymbolKinds.parenthesisEnd:
                    return ")";
                case SymbolKinds.comma:
                    return ",";
                case SymbolKinds.number:
                    return Num.ToString();
                case SymbolKinds.root:
                    return "root(" + calc.numberToString(Num) + ", " + Pow.ToString() + ")";
                case SymbolKinds.x:
                    string res = "";
                    if (Num == 0 || Pow == 0)
                    {
                        return "";
                    }
                    else if (Num == -1)
                    {
                        res += "-";
                    }
                    else if (Num != 1 && Num != -1)
                    {
                        res += calc.numberToString(Num);
                    }
                    res += "x";
                    if (Pow > 1 || Pow < 0)
                    {
                        res += "^" + Pow.ToString();
                    }
                    return res;
                default:
                    return "";
            }
        }

        public enum SymbolKinds { error, number, multiplay, divide, plus, minus , parenthesisStart, parenthesisEnd, power, x, root, comma }

        public SymbolKinds Kind { get; set; }
        public decimal Num { get; set; }
        public int Pow { get; set; }
    }
}