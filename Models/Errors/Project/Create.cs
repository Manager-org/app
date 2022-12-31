namespace Manager.Models.Errors.Project
{
    public class Create
    {
        bool _name, _priceHour, _isError;

        public bool Name { get => _name; set => _name = value; }
        public bool PriceHour { get => _priceHour; set => _priceHour = value; }
        public bool IsError { get => _isError; set => _isError = value; }
    }
}
