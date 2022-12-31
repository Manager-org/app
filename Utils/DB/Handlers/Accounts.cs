namespace Manager.Utils.DB.Handlers
{
    public class Accounts
    {
        RuleHandle _RuleHandle;

        public Accounts(RuleHandle ruleHandle) => _RuleHandle = ruleHandle;

        public bool Add(Models.Account account)
        {
            MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand($"insert into `Accounts` values(default, @username, @password, @name, @middleSurname, @surname, @isAdmin)", _RuleHandle.Connection);
            command.Parameters.AddWithValue("@username", account.Username);
            command.Parameters.AddWithValue("@password", account.Password);
            command.Parameters.AddWithValue("@name", account.Name);
            command.Parameters.AddWithValue("@middleSurname", account.MiddleName);
            command.Parameters.AddWithValue("@surname", account.Surname);
            command.Parameters.AddWithValue("@isAdmin", account.IsAdmin);

            var result = (command.ExecuteNonQuery() > 0) ? true : false;

            _RuleHandle.Connection.Close();

            return result;
        }

        public bool Remove(Models.Account account)
        {
            MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand($"delete from `Accounts` where id = @id", _RuleHandle.Connection);
            command.Parameters.AddWithValue("@id", account.ID);

            var result = (command.ExecuteNonQuery() > 0) ? true : false;

            _RuleHandle.Connection.Close();

            return result;
        }

        public bool Exists(Models.Account account)
        {
            if (
                account.Username != null
                && account.Password != null
            )
            {
                MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand($"select exists(select id from `Accounts` where username=@username and password=@password)", _RuleHandle.Connection);
                command.Parameters.AddWithValue("@username", account.Username);
                command.Parameters.AddWithValue("@password", account.Password);

                var result = ((int)command.ExecuteScalar() == 1) ? true : false;

                _RuleHandle.Connection.Close();

                return result;
            }
            else
            {
                MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand($"select exists(select id from `Accounts` where username=@username)", _RuleHandle.Connection);
                command.Parameters.AddWithValue("@username", account.Username);

                var result = ((int)command.ExecuteScalar() == 1) ? true : false;

                _RuleHandle.Connection.Close();

                return result;
            }
        }

        public List<Models.Account> GetList()
        {
            MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand($"select * from `Accounts`", _RuleHandle.Connection);
            MySql.Data.MySqlClient.MySqlDataReader reader = command.ExecuteReader();
            List<Models.Account> accounts = new List<Models.Account>();

            while (reader.Read())
                accounts.Add(new Models.Account(Convert.ToInt32(reader.GetString("id")), reader.GetString("Username"), reader.GetString("Name"), reader.GetString("MiddleName"), reader.GetString("Surname"), reader.GetBoolean("IsAdmin")));

            var result = accounts;

            _RuleHandle.Connection.Close();

            return result;
        }

        public Models.Account GetAccount(Models.Account account)
        {
            if (account.ID != 0)
            {
                MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand($"select Username, Name, MiddleName, Surname, IsAdmin from `Accounts` where id=@id", _RuleHandle.Connection);
                command.Parameters.AddWithValue("@id", account.ID);
                MySql.Data.MySqlClient.MySqlDataReader reader = command.ExecuteReader();
                reader.Read();

                var result = new Models.Account(account.ID, reader.GetString("Username"), reader.GetString("Name"), reader.GetString("MiddleName"), reader.GetString("Surname"), reader.GetBoolean("IsAdmin"));

                _RuleHandle.Connection.Close();

                return result;
            }
            else
            {
                MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand($"select id, Username, Name, MiddleName, Surname, IsAdmin from `Accounts` where Username=@username and Password=@password", _RuleHandle.Connection);
                command.Parameters.AddWithValue("@username", account.Username);
                command.Parameters.AddWithValue("@password", account.Password);
                MySql.Data.MySqlClient.MySqlDataReader reader = command.ExecuteReader();
                reader.Read();

                var result = new Models.Account(reader.GetInt32("id"), reader.GetString("Username"), reader.GetString("Name"), reader.GetString("MiddleName"), reader.GetString("Surname"), reader.GetBoolean("IsAdmin"));

                _RuleHandle.Connection.Close();

                return result;
            }
        }

        public bool IsAdmin(Models.Account account)
        {
            MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand($"select IsAdmin from `Accounts` where Username=@username and Password=@password", _RuleHandle.Connection);
            command.Parameters.AddWithValue("@id", account.ID);
            command.Parameters.AddWithValue("@username", account.Username);
            command.Parameters.AddWithValue("@password", account.Password);
            MySql.Data.MySqlClient.MySqlDataReader reader = command.ExecuteReader();
            reader.Read();

            var result = reader.GetBoolean("IsAdmin");

            _RuleHandle.Connection.Close();

            return result;
        }
    }
}
