using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VentasProyect.Controllers
{
    public class FiltroSeguridadController : ActionFilterAttribute
    {
        LoginController _login = new LoginController();
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {

                var email = ThereEmailLogin();
                if (string.IsNullOrEmpty(email))
                {
                    // Redirige al usuario a la página de inicio de sesión
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary
                        {
                            { "controller", "Login/Index" }
                        });
                }
            }
            catch (Exception e)
            {
                //Si hay una excepción debe seguir el flujo normal sin interferir con el funcionamiento
            }

            base.OnActionExecuting(context);

        }

        public string ThereEmailLogin()
        {
            try
            {
                if (HttpContext.Current != null && HttpContext.Current.Session != null)
                {
                    return HttpContext.Current.Session["Email"] as string ?? string.Empty;
                }
                else
                {
                    // HttpContext o HttpContext.Session es nulo
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                // Capturar cualquier excepción
                return string.Empty;
            }
        }
    }
}