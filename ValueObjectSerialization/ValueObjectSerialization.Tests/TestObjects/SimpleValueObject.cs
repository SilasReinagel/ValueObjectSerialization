namespace ValueObjectSerialization.Tests.TestObjects
{
    public class SimpleValueObject
    {
        public string Name { get; private set; }
        public int Year { get; private set; }

        public SimpleValueObject(string name, int year)
        {
            Name = name;
            Year = year;
        }
    }
}
