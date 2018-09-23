namespace math
{
    public class FuncDomainDefinition
    {
        public FuncDomainDefinition(DomainDefinitionKinds kind, decimal x)
        {
            Kind = kind;
            X = x;
        }

        public override string ToString()
        {
            CalcExercise calc = new CalcExercise();
            if (Kind == DomainDefinitionKinds.xNotEquals)
            {
                return "x != " + calc.numberToString(X);
            }
            return "";
        }

        public enum DomainDefinitionKinds { xNotEquals }
        public DomainDefinitionKinds Kind { get; set; }

        public decimal X { get; set; }
    }
}