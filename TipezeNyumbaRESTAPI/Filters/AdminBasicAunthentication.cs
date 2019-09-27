using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using TipezeNyumbaService.Models.UserClassesAndMethods.Aunthentication;

namespace TipezeNyumbaRESTAPI.Filters
{
    public class AdminBasicAunthentication : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "No authorization credentials");
            }
            else
            {
                try
                {
                    LoginManagement login = new LoginManagement();
                    string username = "";
                    string password = "";
                    string aunthenticationToken = actionContext.Request.Headers.Authorization.Parameter;
                    string decodedAunthenticationToken =
                        Encoding.UTF8.GetString(Convert.FromBase64String(aunthenticationToken));
                    string[] usernamePasswordArray = decodedAunthenticationToken.Split(':');
                    username = usernamePasswordArray[0];
                    password = usernamePasswordArray[1];
                    if (login.VerifyAdminCredentials(username, password))
                    {
                        Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), null);
                    }
                    else
                    {
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized,"User not an admin");
                    }
                }
                catch (FormatException)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Format of input string not to base64");
                }
                catch (Exception)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
            }
        }
    }
}