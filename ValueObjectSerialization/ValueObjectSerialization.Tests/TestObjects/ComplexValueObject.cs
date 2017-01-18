namespace ValueObjectSerialization.Tests.TestObjects
{
    public class ComplexValueObject
    {
        public string Name { get; private set; }
        public SimpleValueObject Inner { get; private set; }

        public ComplexValueObject(string name, SimpleValueObject inner)
        {
            Name = name;
            Inner = inner;
        }
    }
}
