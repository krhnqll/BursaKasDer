using System;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace bursaKasder.HelperClasses
{
    public class SessionClass
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionClass(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        //Adimn Id for session Class
        public int? Admin_id
        {
            get
            {
                return _httpContextAccessor.HttpContext?.Session.GetInt32("Admin_Id");
            }

            set
            {
                if (value.HasValue)
                    _httpContextAccessor.HttpContext?.Session.SetInt32("AdminId", value.Value);
                else
                    _httpContextAccessor.HttpContext?.Session.Remove("AdminId");
            }
        }
        public string? Admin_name
        {
            get
            {
                return _httpContextAccessor.HttpContext?.Session.GetString("Admin_name");
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                    _httpContextAccessor.HttpContext?.Session.SetString("AdminName", value);
                else
                    _httpContextAccessor.HttpContext?.Session.Remove("AdminName");
            }
        }
        public string? Admin_surname
        {
            get
            {
                return _httpContextAccessor.HttpContext?.Session.GetString("Admin_surname");
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                    _httpContextAccessor.HttpContext?.Session.SetString("AdminSurname", value);
                else
                    _httpContextAccessor.HttpContext?.Session.Remove("AdminSurname");
            }
        }
    }
}
