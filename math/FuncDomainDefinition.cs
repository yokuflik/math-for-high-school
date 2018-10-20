using System;
namespace math
{
    public class FuncDomainDefinition
    {
        public FuncDomainDefinition(DomainDefinitionKinds kind, decimal x)
        {
            Kind = kind;
            X = x;
            EqualsToX = false;
        }
        
        public FuncDomainDefinition(DomainDefinitionKinds kind, bool equals)
        {
            Kind = kind;
            EqualsToX = equals;
        }

        public FuncDomainDefinition(DomainDefinitionKinds kind, decimal x, bool equals)
        {
            Kind = kind;
            X = x;
            EqualsToX = equals;
        }

        public FuncDomainDefinition(DomainDefinitionKinds kind, XBetweenKinds xBetweenKind, decimal smallX, decimal bigX)
        {
            Kind = kind;
            XBetweenKind = xBetweenKind;
            SmallerX = Math.Min(smallX, bigX);
            BiggerX = Math.Max(smallX, bigX);
        }

        public override string ToString()
        {
            CalcExercise calc = new CalcExercise();
            if (Kind == DomainDefinitionKinds.xNotEquals)
            {
                return "x != " + calc.numberToString(X);
            }
            else if (Kind == DomainDefinitionKinds.allX)
            {
                if (EqualsToX)
                {
                    return "Every x";
                }
                else
                {
                    return "None x";
                }
            }
            else if (Kind == DomainDefinitionKinds.xBigThen)
            {
                string res = "x >";
                if (EqualsToX)
                {
                    res += '=';
                }
                res += " "+calc.numberToString(X);
                return res;
            }
            else if (Kind == DomainDefinitionKinds.xSmallThen)
            {
                string res = "x <";
                if (EqualsToX)
                {
                    res += '=';
                }
                res += " " + calc.numberToString(X);
                return res;
            }
            else if (Kind == DomainDefinitionKinds.xBetweenThen)
            {
                if (XBetweenKind == XBetweenKinds.equalsToAll)
                {
                    return calc.numberToString(SmallerX) + " <= x <= " + calc.numberToString(BiggerX);
                }
                else if (XBetweenKind == XBetweenKinds.equalsToBigger)
                {
                    return calc.numberToString(SmallerX) + " < x <= " + calc.numberToString(BiggerX);
                }
                else if (XBetweenKind == XBetweenKinds.equalsToSmaller)
                {
                    return calc.numberToString(SmallerX) + " <= x < " + calc.numberToString(BiggerX);
                }
                else if (XBetweenKind == XBetweenKinds.notEquals)
                {
                    return calc.numberToString(SmallerX) + " < x < " + calc.numberToString(BiggerX);
                }
            }
            return "";
        }

        public decimal getSmallX()
        {
            if (Kind == DomainDefinitionKinds.xBetweenThen)
            {
                return SmallerX;
            }
            return X;
        }

        public override bool Equals(object obj)
        {
            try
            {
                FuncDomainDefinition fdd = (FuncDomainDefinition)obj;
                if (fdd.Kind == Kind && fdd.X == X)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public enum DomainDefinitionKinds { xNotEquals, xBigThen, xSmallThen, xBetweenThen, allX}

        public bool EqualsToX { get; set; }

        public enum XBetweenKinds { notEquals, equalsToAll, equalsToSmaller, equalsToBigger}

        public XBetweenKinds XBetweenKind { get; set; }

        public DomainDefinitionKinds Kind { get; set; }

        public decimal X { get; set; }

        public decimal SmallerX { get; set; }
        public decimal BiggerX { get; set; }
    }
}