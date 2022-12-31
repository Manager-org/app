namespace Manager.Utils.DB.Handlers
{
    public class Projects
    {
        RuleHandle _RuleHandle;

        public Projects(RuleHandle ruleHandle)
        {
            _RuleHandle = ruleHandle;
        }

        public bool Add(Models.Project project)
        {
            MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand($"insert into `Projects` values(default, @name, @priceHour, @typeRequirementsHours, @typeArchitectureHours, @typeImplementationHours, @typeTestingHours)", _RuleHandle.Connection);
            command.Parameters.AddWithValue("@name", project.Name);
            command.Parameters.AddWithValue("@priceHour", project.PriceHour);
            command.Parameters.AddWithValue("@typeRequirementsHours", project.TypeRequirementsHours);
            command.Parameters.AddWithValue("@typeArchitectureHours", project.TypeArchitectureHours);
            command.Parameters.AddWithValue("@typeImplementationHours", project.TypeImplementationHours);
            command.Parameters.AddWithValue("@typeTestingHours", project.TypeTestingHours);

            var result = (command.ExecuteNonQuery() > 0) ? true : false;

            _RuleHandle.Connection.Close();

            return result;
        }

        public bool Update(Models.Project projectOld, Models.Project projectNew)
        {
            MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand($"update `Projects` set Name=@newProjectName, PriceHour=@newProjectPriceHour, TypeRequirementsHours=@newProjectTypeRequirementsHours, TypeArchitectureHours=@newProjectTypeArchitectureHours, TypeImplementationHours=@newProjectTypeImplementationHours, TypeTestingHours=@newProjectTypeTestingHours where id=@oldProjectID", _RuleHandle.Connection);
            command.Parameters.AddWithValue("@oldProjectID", projectOld.ID);
            command.Parameters.AddWithValue("@newProjectName", projectNew.Name);
            command.Parameters.AddWithValue("@newProjectPriceHour", projectNew.PriceHour);
            command.Parameters.AddWithValue("@newProjectTypeRequirementsHours", projectNew.TypeRequirementsHours);
            command.Parameters.AddWithValue("@newProjectTypeArchitectureHours", projectNew.TypeArchitectureHours);
            command.Parameters.AddWithValue("@newProjectTypeImplementationHours", projectNew.TypeImplementationHours);
            command.Parameters.AddWithValue("@newProjectTypeTestingHours", projectNew.TypeTestingHours);

            var result = (command.ExecuteNonQuery() > 0) ? true : false;

            _RuleHandle.Connection.Close();

            return result;
        }

        public bool Remove(Models.Project project)
        {
            MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand($"delete from `Projects` where id = @id", _RuleHandle.Connection);
            command.Parameters.AddWithValue("@id", project.ID);

            var result = (command.ExecuteNonQuery() > 0) ? true : false;

            _RuleHandle.Connection.Close();

            return result;
        }

        public List<Models.Project> GetListShort()
        {
            MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand($"select * from `Projects`", _RuleHandle.Connection);
            MySql.Data.MySqlClient.MySqlDataReader reader = command.ExecuteReader();
            List<Models.Project> projects = new List<Models.Project>();

            while (reader.Read())
                projects.Add(new Models.Project(Convert.ToInt32(reader.GetString("id")), reader.GetString("Name"), reader.GetInt32("PriceHour"), reader.GetInt32("TypeRequirementsHours"), reader.GetInt32("TypeArchitectureHours"), reader.GetInt32("TypeImplementationHours"), reader.GetInt32("TypeTestingHours")));

            var result = projects;

            _RuleHandle.Connection.Close();

            return result;
        }

        public Models.Project GetProject(Models.Project project)
        {
            MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand($"select * from `Projects` where id=@id limit 1", _RuleHandle.Connection);
            command.Parameters.AddWithValue("@id", project.ID);
            MySql.Data.MySqlClient.MySqlDataReader reader = command.ExecuteReader();
            reader.Read();

            var result = new Models.Project(project.ID, reader.GetString("Name"), reader.GetInt32("PriceHour"), reader.GetInt32("TypeRequirementsHours"), reader.GetInt32("TypeArchitectureHours"), reader.GetInt32("TypeImplementationHours"), reader.GetInt32("TypeTestingHours"));

            _RuleHandle.Connection.Close();

            return result;
        }
    }
}
