using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using TipezeNyumbaService.Models;
using TipezeNyumbaService.Models.UserClassesAndMethods;


namespace TipezeNyumbaRESTAPI.Controllers
{
    public class UsersController : ApiController
    {
        private readonly UserManagement _userManagement = new UserManagement();
        private readonly CaptureException _logException = new CaptureException();
        [HttpGet]
        public HttpResponseMessage GetUsers()
        {
            try
            {
                List<UserOutputModel> usersToDisplay = _userManagement.GetAllUsers();
                return Request.CreateResponse(HttpStatusCode.OK, usersToDisplay);
            }
            catch (System.Data.Entity.Core.EntityException ex)
            {
                _logException.LogException(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "System not available");
            }
            catch (Exception ex)
            {
                _logException.LogException(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unknown error, contact administrator");
            }
        }

        // GET api/values/5
        [HttpGet]
        public HttpResponseMessage GetUserById(string id)
        {
            try
            {
                bool checkIfIdIsInt = _userManagement.variableValidation.IsDigitsOnly(id);
                if (checkIfIdIsInt == false) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Incorrect id format, user id should only be numbers");
                UserInputModel getUser = _userManagement.GetUser(Convert.ToInt32(id));
                if (getUser == null) return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User not available in the system");
                return Request.CreateResponse(HttpStatusCode.OK, getUser);
            }
            catch (System.Data.Entity.Core.EntityException ex)
            {
                _logException.LogException(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "System not available");
            }
            catch (Exception ex)
            {
                _logException.LogException(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unknown error, contact administrator");
            }
        }

        [HttpGet]
        public HttpResponseMessage GetUsersByAccountState(string userAccountState)
        {
            try
            {
                List<UserOutputModel> getUsersBySystemRole = new List<UserOutputModel>();
                if (userAccountState != "deactivated" && userAccountState != "activated")
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "User state not specified. State must be activated or deactivated");
                }
                if (userAccountState == "deactivated")
                {
                    getUsersBySystemRole = _userManagement.GetDeactivatedUsers();
                }
                if (userAccountState == "activated")
                {
                    getUsersBySystemRole = _userManagement.GetActivatedUsers();
                }
                if (getUsersBySystemRole.Count == 0) return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Users with " + userAccountState + " state not available in the system");
                return Request.CreateResponse(HttpStatusCode.OK, getUsersBySystemRole);
            }
            catch (System.Data.Entity.Core.EntityException ex)
            {
                _logException.LogException(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "System not available");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unknown error, contact administrator");
            }
        }

        [HttpGet]
        public HttpResponseMessage GetUsersBySytemRole(string systemRole)
        {
            try
            {
                List<UserOutputModel> getUsersBySystemRole = _userManagement.GetUsers(systemRole);
                if (getUsersBySystemRole.Count == 0) return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Users not available in the system");
                return Request.CreateResponse(HttpStatusCode.OK, getUsersBySystemRole);
            }
            catch (System.Data.Entity.Core.EntityException ex)
            {
                _logException.LogException(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "System not available");
            }
            catch (Exception ex)
            {
                _logException.LogException(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unknown error, contact administrator");
            }
        }
        // POST api/values
        [HttpPost]
        public HttpResponseMessage AddUser([FromBody]UserInputModel userDetails)
        {
            if (userDetails == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NoContent, "No user details in the system.");
            }

            try
            {
                _userManagement.AddUser(userDetails);
                return Request.CreateResponse(HttpStatusCode.OK,"User created successfully");
            }
            catch (System.Data.Entity.Core.EntityException ex)
            {
                _logException.LogException(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "System not available");
            }
            catch (Exception ex)
            {
                _logException.LogException(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unknown error, contact administrator");
            }
        }

        // PUT api/values/5
        [HttpPut]
        public HttpResponseMessage UpdateUserDetails(string id, [FromBody]UserInputModel userDetails)
        {
            bool checkIfIdIsInt = _userManagement.variableValidation.IsDigitsOnly(id);
            if (checkIfIdIsInt == false) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Incorrect id format, house id should only be numbers");
            UserInputModel getUser = _userManagement.GetUser(Convert.ToInt32(id));
            if (getUser == null) return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User not available in the system");
            try
            {
                _userManagement.UpdateUserDetails(Convert.ToInt32(id),userDetails);
                return Request.CreateResponse(HttpStatusCode.OK,"User details updated");
            }
            catch (System.Data.Entity.Core.EntityException ex)
            {
                _logException.LogException(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "System not available");
            }
            catch (Exception ex)
            {
                _logException.LogException(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unknown error, contact administrator");
            }
        }

        // DELETE api/values/5
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            return Request.CreateErrorResponse(HttpStatusCode.NotImplemented, "Unavailable operation");
        }
    }
}
