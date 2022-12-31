namespace Manager.Models.Errors.Account
{
    public class Login
    {
        bool _username, _password, _isError;

        public bool Username { get => _username; set => _username = value; }
        public bool Password { get => _password; set => _password = value; }
        public bool IsError { get => _isError; set => _isError = value; }
    }
}
