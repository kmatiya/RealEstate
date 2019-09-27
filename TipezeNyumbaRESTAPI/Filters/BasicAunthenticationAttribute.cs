using System;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using TipezeNyumbaService.Models.UserClassesAndMethods.Aunthentication;

namespace TipezeNyumbaRESTAPI.Filters
{
    public class BasicAunthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        { 
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
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
                        if (login.VerifyCredentials(username, password))
                        {
                            Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), null);
                        }
                        else
                        {
                            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
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