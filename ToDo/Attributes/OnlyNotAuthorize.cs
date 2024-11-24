using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ToDo.Attributes
{
    public class OnlyNotAuthorize : Attribute, IAuthorizationFilter
    {
        /// <summary>
        /// Метод для редиректа на Note, если пользователь авторизован
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToActionResult("index", "Note", null);
            }
        }
    }
}
