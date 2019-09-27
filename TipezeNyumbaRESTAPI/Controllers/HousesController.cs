using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TipezeNyumbaRESTAPI.Filters;
using TipezeNyumbaService.Models;
using TipezeNyumbaService.Models.HouseClassesAndMethods.GeneralHouses;

namespace TipezeNyumbaRESTAPI.Controllers
{
    [BasicAunthentication]
    public class HousesController : ApiController
    {
        private readonly GeneralHouseManagement _houseManagement = new GeneralHouseManagement();
        private readonly CaptureException _logException = new CaptureException();
        // GET: api/Houses
        [HttpGet]
        public HttpResponseMessage GetAllHouses()
        {
            try
            {
                List<HouseModel> allHouses = _houseManagement.GetAllHouses();
                if (allHouses.Count == 0)
                {
                    return Request.CreateResponse((HttpStatusCode.NoContent), "Houses not available in the system");
                }
                return Request.CreateResponse(HttpStatusCode.OK, allHouses);
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
        public HttpResponseMessage GetHousesByDistrict(string district)
        {
            try
            {
                List<HouseModel> allHouses = _houseManagement.GetAllHousesByDistrict(district);
                if (allHouses.Count == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent,
                        "Houses not found with district " + district + " in Malawi");
                }
                return Request.CreateResponse(HttpStatusCode.OK, allHouses);
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
        public HttpResponseMessage GetHousesByPriceRange(string district, decimal minimumPrice, decimal maximumPrice,
            string location = null)
        {
            try
            {
                List<HouseModel> allHouses = location == null ? _houseManagement.GetHousesByPriceRange(district, minimumPrice,maximumPrice) : _houseManagement.GetHousesByPriceRange(district, minimumPrice, maximumPrice,location);
                if (allHouses == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Houses not available for the specified price ranges and location");
                }
                return Request.CreateResponse(HttpStatusCode.OK, allHouses);
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
        public HttpResponseMessage GetAllHousesByDateHouseWillBeAvailable(string district, DateTime date,
            string location = null)
        {
            try
            {
                List<HouseModel> allHouses = location == null ? _houseManagement.GetAllHousesByDateHouseWillBeAvailable(district,date) : _houseManagement.GetAllHousesByDateHouseWillBeAvailable(district,date, location);
                if (allHouses == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Houses not available on the specified date");
                }
                return Request.CreateResponse(HttpStatusCode.OK, allHouses);
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
        public HttpResponseMessage GetAllAvailableHousesByDateRange(string district, DateTime startDate,
            DateTime endDate, string location = null)
        {
            try
            {
                List<HouseModel> allHouses = location == null ? _houseManagement.GetAllAvailableHousesByDateRange(district, startDate.Date, endDate.Date) : _houseManagement.GetAllAvailableHousesByDateRange(district, startDate, endDate, location);
                if (allHouses == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Houses not available on the specified dates");
                }
                return Request.CreateResponse(HttpStatusCode.OK, allHouses);
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
       
        // GET: api/Houses/5
        [HttpGet]
        public HttpResponseMessage GetHouseById(string id)
        {
            try
            {
                bool checkIfIdIsInt = _houseManagement.variableValidation.IsDigitsOnly(id);
                if (checkIfIdIsInt == false) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Incorrect id format, house id should only be numbers");
                HouseModel gethouse = _houseManagement.GetHouseById(Convert.ToInt32(id));
                if (gethouse == null) return Request.CreateErrorResponse(HttpStatusCode.NotFound, "House not available in the system");
                return Request.CreateResponse(HttpStatusCode.OK, gethouse);
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
        [HttpPost]
        // POST: api/Houses
        public HttpResponseMessage Post([FromBody]HouseModel newHouseDetails)
        {
            if (newHouseDetails == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NoContent, "No house details in the system.");
            }

            try
            {
               bool checkIfHouseIsCreated = _houseManagement.AddHouse(newHouseDetails);
                if (checkIfHouseIsCreated)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, "House created successfully");
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please try again later");

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

        [HttpPut]
        public HttpResponseMessage UpdateHouseStatus(int houseId, [FromBody]string houseStatus)
        {
            if (houseStatus == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NoContent, "No house details in the system.");
            }
            try
            {
                bool checkHouseIsUpdated = _houseManagement.UpdateHouseStatus(houseId,houseStatus);
                if (checkHouseIsUpdated)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, "House updated successfully");
                }
                return Request.CreateErrorResponse(HttpStatusCode.Forbidden, "House status not updated, check house status sent");
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
        // PUT: api/Houses/5
        [HttpPut]
        public HttpResponseMessage UpdateHouseDetails(string id, [FromBody]HouseModel updatedHouseModel)
        {
            bool checkIfIdIsInt = _houseManagement.variableValidation.IsDigitsOnly(id);
            if (checkIfIdIsInt == false) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Incorrect id format, house id should only be numbers");
            if (updatedHouseModel == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NoContent, "No house details in the system.");
            }
            try
            {
                _houseManagement.UpdateHouseDetails(Convert.ToInt32(id), updatedHouseModel);
                return Request.CreateResponse(HttpStatusCode.Created, "House updated successfully");
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
        // DELETE: api/Houses/5
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            return Request.CreateErrorResponse(HttpStatusCode.NotImplemented, "Unavailable operation");
        }
    }
}
