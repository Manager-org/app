namespace Manager.Models.Errors.Account
{
    public class Registration
    {
        bool _username, _password, _name, _surname, _middleName, _isError;

        public bool Username { get => _username; set => _username = value; }
        public bool Password { get => _password; set => _password = value; }
        public bool Name { get => _name; set => _name = value; }
        public bool Surname { get => _surname; set => _surname = value; }
        public bool MiddleName { get => _middleName; set => _middleName = value; }
        public bool IsError { get => _isError; set => _isError = value; }
    }
}
