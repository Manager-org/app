namespace Manager.Models
{
    public class Project
    {
        int _id, _priceHour, _typeRequirementsHours, _typeArchitectureHours, _typeImplementationHours, _typeTestingHours;
        string? _name;
        List<Person>? _persons;

        public Project(int? id)
        {
            if (id != null)
                this.ID = id.Value;
            else throw new Exception("Ошибка при получении id");
        }

        public Project(string name, int priceHour)
        {
            Name = name;
            PriceHour = priceHour;
        }

        public Project(string? name, int priceHour, int typeRequirementsHours, int typeArchitectureHours, int typeImplementationHours, int typeTestingHours)
        {
            if (name != null)
                this.Name = name;
            else throw new Exception("Ошибка при получении name");

            Name = name;
            PriceHour = priceHour;
            TypeRequirementsHours = typeRequirementsHours;
            TypeArchitectureHours = typeArchitectureHours;
            TypeImplementationHours = typeImplementationHours;
            TypeTestingHours = typeTestingHours;
        }

        public Project(int id, string name, int priceHour, int typeRequirementsHours, int typeArchitectureHours, int typeImplementationHours, int typeTestingHours)
        {
            ID = id;
            Name = name;
            PriceHour = priceHour;
            TypeRequirementsHours = typeRequirementsHours;
            TypeArchitectureHours = typeArchitectureHours;
            TypeImplementationHours = typeImplementationHours;
            TypeTestingHours = typeTestingHours;
        }

        public Project(int id, string name, int priceHour, int typeRequirementsHours, int typeArchitectureHours, int typeImplementationHours, int typeTestingHours, List<Person> persons)
        {
            ID = id;
            Name = name;
            PriceHour = priceHour;
            TypeRequirementsHours = typeRequirementsHours;
            TypeArchitectureHours = typeArchitectureHours;
            TypeImplementationHours = typeImplementationHours;
            TypeTestingHours = typeTestingHours;
            Persons = persons;
        }

        public int ID { get => _id; set => _id = value; }
        public string? Name { get => _name; set => _name = value; }
        public int PriceHour { get => _priceHour; set => _priceHour = value; }
        public int TypeRequirementsHours { get => _typeRequirementsHours; set => _typeRequirementsHours = value; }
        public int TypeArchitectureHours { get => _typeArchitectureHours; set => _typeArchitectureHours = value; }
        public int TypeImplementationHours { get => _typeImplementationHours; set => _typeImplementationHours = value; }
        public int TypeTestingHours { get => _typeTestingHours; set => _typeTestingHours = value; }
        public List<Person>? Persons { get => _persons; set => _persons = value; }
        public double FinalPrice() => PriceHour * (TypeRequirementsHours + TypeArchitectureHours + TypeImplementationHours + TypeTestingHours);
    }
}
