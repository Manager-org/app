using System.Text.Json;
using Manager.Models;

namespace Manager.Utils
{
    class Cookies
    {
        HttpRequest? _Request;
        HttpResponse? _Response;

        public Cookies(HttpRequest request) => _Request = request;

        public Cookies(HttpResponse response) => _Response = response;

        public Cookies(HttpRequest request, HttpResponse response)
        {
            _Request = request;
            _Response = response;
        }

        public void Unset()
        {
            if (_Response != null)
                _Response.Cookies.Append("__Manager", "", new Microsoft.AspNetCore.Http.CookieOptions()
                {
                    Path = "/",
                    HttpOnly = true,
                    IsEssential = true,
                    Expires = DateTime.Now.AddMilliseconds(0),
                    Secure = false
                });
            else throw new Exception("Не инициализирован _Response");
        }

        public bool Find()
        {
            if (_Request != null)
                return _Request.Cookies["__Manager"] != null;
            else throw new Exception("Не инициализирован _Request");
        }

        public Account Get()
        {
            if (_Request != null)
            {
                string? temp1 = _Request.Cookies["__Manager"];

                if (temp1 != null)
                {
                    Models.Account? temp2 = JsonSerializer.Deserialize<Models.Account>(temp1);

                    if (temp2 != null)
                    {
                        return temp2;
                    }
                    else throw new Exception("Не найдено");
                }
                else throw new Exception("Не найдено");
            }
            else throw new Exception("Не инициализирован _Request");
        }

        public void Set(Account account)
        {
            if (_Response != null)
                _Response.Cookies.Append("__Manager", JsonSerializer.Serialize(account), new Microsoft.AspNetCore.Http.CookieOptions()
                {
                    Path = "/",
                    HttpOnly = true,
                    IsEssential = true,
                    Expires = DateTime.Now.AddMonths(1),
                    Secure = false
                });
            else throw new Exception("Не инициализирован _Response");
        }
    }
}
