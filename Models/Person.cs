namespace Manager.Models
{
    public class Person
    {
        int _id, _typeRequirements, _typeArchitecture, _typeImplementation, _typeTesting;
        Models.Account? _account;
        Models.Project? _project;

        public Person(int id) => this.ID = id;

        public Person(Models.Account account, Models.Project project)
        {
            this.Account = account;
            this.Project = project;
        }

        public Person(int typeRequirements, int typeArchitecture, int typeImplementation, int typeTesting, Models.Account account, Models.Project project)
        {
            this.TypeRequirements = typeRequirements;
            this.TypeArchitecture = typeArchitecture;
            this.TypeImplementation = typeImplementation;
            this.TypeTesting = typeTesting;
            this.Account = account;
            this.Project = project;
        }

        public Person(int id, int typeRequirements, int typeArchitecture, int typeImplementation, int typeTesting, Models.Account account, Models.Project project)
        {
            this.ID = id;
            this.TypeRequirements = typeRequirements;
            this.TypeArchitecture = typeArchitecture;
            this.TypeImplementation = typeImplementation;
            this.TypeTesting = typeTesting;
            this.Account = account;
            this.Project = project;
        }

        public int ID { get => _id; set => _id = value; }
        public int TypeRequirements { get => _typeRequirements; set => _typeRequirements = value; }
        public int TypeArchitecture { get => _typeArchitecture; set => _typeArchitecture = value; }
        public int TypeImplementation { get => _typeImplementation; set => _typeImplementation = value; }
        public int TypeTesting { get => _typeTesting; set => _typeTesting = value; }
        public Account? Account { get => _account; set => _account = value; }
        public Project? Project { get => _project; set => _project = value; }
    }
}
