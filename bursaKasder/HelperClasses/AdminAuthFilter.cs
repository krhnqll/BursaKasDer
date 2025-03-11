using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using bursaKasder.HelperClasses;

namespace bursaKasder.Filters
{
    public class AdminAuthFilter : IActionFilter
    {
        private readonly SessionClass _session;

        public AdminAuthFilter(SessionClass session)
        {
            _session = session;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (string.IsNullOrEmpty(_session.Admin_name))
            {
                context.Result = new RedirectToActionResult("Index", "Login", null);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
