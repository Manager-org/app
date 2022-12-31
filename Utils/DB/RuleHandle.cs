using MySql.Data.MySqlClient;

namespace Manager.Utils.DB
{
    public class RuleHandle
    {
        public MySqlConnection Connection;
        public Handlers.Accounts Accounts;
        public Handlers.Projects Projects;
        public Handlers.Persons Persons;

        public RuleHandle()
        {
            Connection = new MySqlConnection(new AppSettings().Configuration.GetValue<string>("ConnectionString"));
            Accounts = new Handlers.Accounts(this);
            Projects = new Handlers.Projects(this);
            Persons = new Handlers.Persons(this);

            Connection.Open();
        }
    }
}
