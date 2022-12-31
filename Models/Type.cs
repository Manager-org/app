namespace Manager.Models
{
    public class Type
    {
        string? _name;
        int _countHours;
        List<Person>? _persons;

        public Type(string name) => Name = name;

        public string? Name { get => _name; set => _name = value; }
        public int CountHours { get => _countHours; set => _countHours = value; }
        public List<Person>? Persons { get => _persons; set => _persons = value; }
    }
}
