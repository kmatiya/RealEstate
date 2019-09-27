using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TipezeNyumbaRESTAPI.Models;
using TipezeNyumbaService.Models.HouseClassesAndMethods;
using TipezeNyumbaService.Models.HouseClassesAndMethods.InterestedHouseClients;
using TipezeNyumbaService.Models.UserClassesAndMethods;

namespace TipezeNyumbaRESTAPI.Controllers
{
    public class InterestedHouseClientsController : ApiController
    {
        private readonly InterestedHouseClientsManagement _interestedHouseClientsManagement = new InterestedHouseClientsManagement();
        // GET: api/InterestedHouseClients
        public HttpResponseMessage GetInterestedClients(string houseId)
        {
            bool checkIfIdIsInt = _interestedHouseClientsManagement.variableValidation.IsDigitsOnly(houseId);
            if (checkIfIdIsInt == false) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Incorrect id format, house id should only be numbers");
            List<UserOutputModel> getClients = _interestedHouseClientsManagement.GetInterestedHouseClients(Convert.ToInt32(Convert.ToInt32(houseId)));
            if (getClients == null) return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Clients not found for the specified house");
            return Request.CreateResponse(HttpStatusCode.OK, getClients);
        }

        // GET: api/InterestedHouseClients/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/InterestedHouseClients
        [HttpPost]
        public HttpResponseMessage AddInterestedHouseClient([FromBody]UserHouseModel interestedClientModel)
        {
            if (interestedClientModel.houseId == null || interestedClientModel.userId == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid input variables");
            }
            bool checkIfHouseIdIsInt = _interestedHouseClientsManagement.variableValidation.IsDigitsOnly(interestedClientModel.houseId);
            bool checkIfUserIdIsInt = _interestedHouseClientsManagement.variableValidation.IsDigitsOnly(interestedClientModel.userId);
            if (checkIfHouseIdIsInt == false)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "House Id must be integer.");
            }
            if (checkIfUserIdIsInt == false)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "User Id must be integer.");
            }
            try
            {
                bool checkIfInterestedClientIsCreated = _interestedHouseClientsManagement.AddInterestedClient(Convert.ToInt32(interestedClientModel.houseId), Convert.ToInt32(interestedClientModel.userId), "Interested");
                if (checkIfInterestedClientIsCreated)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "User added to interested house clients list");
                }
                return Request.CreateResponse(HttpStatusCode.NoContent, "User not added to interested house clients list");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Please try again later");
            }
        }

        // PUT: api/InterestedHouseClients/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/InterestedHouseClients/5
        public void Delete(int id)
        {
        }
    }
}
