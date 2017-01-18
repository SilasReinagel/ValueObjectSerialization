namespace ValueObjectSerialization.Tests.TestObjects
{
    public class InheritedValueObject : SimpleValueObject
    {
        public string Weapon { get; private set; }

        public InheritedValueObject(string name, int year, string weapon) : base(name, year)
        {
            Weapon = weapon;
        }
    }
}
