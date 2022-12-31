namespace Manager.Utils.DB.Handlers
{
    public class Persons
    {
        RuleHandle _RuleHandle;

        public Persons(RuleHandle ruleHandle) => _RuleHandle = ruleHandle;

        public bool Add(Models.Person person)
        {
            MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand($"insert into `Persons` values(default, @idAccount, @idProject, @typeRequirements, @typeArchitecture, @typeImplementation, @typeTesting)", _RuleHandle.Connection);
            command.Parameters.AddWithValue("@idAccount", person.Account?.ID);
            command.Parameters.AddWithValue("@idProject", person.Project?.ID);
            command.Parameters.AddWithValue("@typeRequirements", person.TypeRequirements);
            command.Parameters.AddWithValue("@typeArchitecture", person.TypeArchitecture);
            command.Parameters.AddWithValue("@typeImplementation", person.TypeImplementation);
            command.Parameters.AddWithValue("@typeTesting", person.TypeTesting);

            var result = (command.ExecuteNonQuery() > 0) ? true : false;

            _RuleHandle.Connection.Close();

            return result;
        }

        public bool Remove(Models.Person person)
        {
            MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand($"delete from `Persons` where idAccount = @idAccount and idProject = @idProject", _RuleHandle.Connection);
            command.Parameters.AddWithValue("@idAccount", person.Account?.ID);
            command.Parameters.AddWithValue("@idProject", person.Project?.ID);

            var result = (command.ExecuteNonQuery() > 0) ? true : false;

            _RuleHandle.Connection.Close();

            return result;
        }

        public List<Models.Person> GetList()
        {
            MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand($"select * from `Persons`", _RuleHandle.Connection);
            MySql.Data.MySqlClient.MySqlDataReader reader = command.ExecuteReader();
            List<Models.Person> projects = new List<Models.Person>();

            while (reader.Read())
                projects.Add(new Models.Person(Convert.ToInt32(reader.GetString("id")), reader.GetInt32("TypeRequirements"), reader.GetInt32("TypeArchitecture"), reader.GetInt32("TypeImplementation"), reader.GetInt32("TypeTesting"), new Utils.DB.RuleHandle().Accounts.GetAccount(new Models.Account(reader.GetInt32("idAccount"))), new Utils.DB.RuleHandle().Projects.GetProject(new Models.Project(reader.GetInt32("idProject")))));

            var result = projects;

            _RuleHandle.Connection.Close();

            return result;
        }

        public List<Models.Person> GetList(Models.Project project)
        {
            MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand($"select * from `Persons` where idProject = @idProject", _RuleHandle.Connection);
            command.Parameters.AddWithValue("@idProject", project.ID);
            MySql.Data.MySqlClient.MySqlDataReader reader = command.ExecuteReader();
            List<Models.Person> projects = new List<Models.Person>();

            while (reader.Read())
                projects.Add(new Models.Person(Convert.ToInt32(reader.GetString("id")), reader.GetInt32("TypeRequirements"), reader.GetInt32("TypeArchitecture"), reader.GetInt32("TypeImplementation"), reader.GetInt32("TypeTesting"), new Utils.DB.RuleHandle().Accounts.GetAccount(new Models.Account(reader.GetInt32("idAccount"))), new Utils.DB.RuleHandle().Projects.GetProject(new Models.Project(reader.GetInt32("idProject")))));

            var result = projects;

            _RuleHandle.Connection.Close();

            return result;
        }

        public Models.Person GetPerson(Models.Account account)
        {
            MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand($"select * from `Persons` where idAccount=@idAccount", _RuleHandle.Connection);
            command.Parameters.AddWithValue("@idAccount", account.ID);
            MySql.Data.MySqlClient.MySqlDataReader reader = command.ExecuteReader();
            reader.Read();

            var result = new Models.Person(reader.GetInt32("id"), reader.GetInt32("TypeRequirements"), reader.GetInt32("TypeArchitecture"), reader.GetInt32("TypeImplementation"), reader.GetInt32("TypeTesting"), (new Utils.DB.RuleHandle()).Accounts.GetAccount(new Models.Account(reader.GetInt32("idAccount"))), (new Utils.DB.RuleHandle()).Projects.GetProject(new Models.Project(reader.GetInt32("idProject"))));

            _RuleHandle.Connection.Close();

            return result;
        }

        public bool ExistsPerson(Models.Account account)
        {
            MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand($"select exists(select * from `Persons` where idAccount=@idAccount)", _RuleHandle.Connection);
            command.Parameters.AddWithValue("@idAccount", account.ID);

            var result = ((int)command.ExecuteScalar() == 1) ? true : false;

            _RuleHandle.Connection.Close();

            return result;
        }
    }
}
