namespace Manager.Models
{
    public class Account
    {
        int _id;
        string? _username, _password, _name, _middleName, _surname;
        bool _isAdmin = false;

        public Account() { }

        public Account(int? id)
        {
            if (id != null)
                this.ID = id.Value;
            else throw new Exception("Ошибка при получении id");
        }

        public Account(string username) => this.Username = username;

        public Account(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public Account(int id, string username, string name, string middleName, string surname)
        {
            this.ID = id;
            this.Username = username;
            this.Name = name;
            this.Surname = surname;
        }

        public Account(int id, string username, string name, string middleName, string surname, bool isAdmin)
        {
            this.ID = id;
            this.Username = username;
            this.Name = name;
            this.Surname = surname;
            this.IsAdmin = isAdmin;
        }

        public int ID { get => _id; set => _id = value; }
        public string? Username { get => _username; set => _username = value; }
        public string? Password { get => _password; set => _password = value; }
        public string? Name { get => _name; set => _name = value; }
        public string? MiddleName { get => _middleName; set => _middleName = value; }
        public string? Surname { get => _surname; set => _surname = value; }
        public bool IsAdmin { get => _isAdmin; set => _isAdmin = value; }

        public override string ToString() => $"ID = {ID}, Username = {Username}, Password = {Password}, Name = {Name}, MiddleName = {MiddleName}, Surname = {Surname}, IsAdmin = {IsAdmin}";
    }
}
