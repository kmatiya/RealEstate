using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Web.Http;
using TipezeNyumbaService.Models;
using TipezeNyumbaService.Models.HouseClassesAndMethods;
using TipezeNyumbaService.Models.HouseClassesAndMethods.FavouriteHouses;
using TipezeNyumbaService.Models.HouseClassesAndMethods.GeneralHouses;

namespace TipezeNyumbaRESTAPI.Controllers
{
    public class FavouriteHousesController : ApiController
    {
        private readonly HouseFavouritesManagement _houseFavouritesManagement = new HouseFavouritesManagement();
        private readonly CaptureException _logException = new CaptureException();
        // GET api/FavouriteHousesController
        [HttpGet]
        public HttpResponseMessage GetFavouriteHouses(string userId)
        {
            try
            {
                bool checkIfIdIsInt = _houseFavouritesManagement.variableValidation.IsDigitsOnly(userId);
                if (checkIfIdIsInt == false) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Incorrect id format, user id should only be numbers");
                List<HouseModel> allFavouriteHouses = _houseFavouritesManagement.GetHousesAsFavourites(Convert.ToInt32(userId));
                return Request.CreateResponse(HttpStatusCode.OK, allFavouriteHouses);
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

        // GET api/FavouriteHousesController/5
        [HttpGet]
        public HttpResponseMessage GetFavourite(string houseId,string userId)
        {
            try
            {
                if (houseId == null || userId == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid input variables");
                }
                bool checkIfHouseIdIsInt = _houseFavouritesManagement.variableValidation.IsDigitsOnly(houseId);
                bool checkIfUserIdIsInt = _houseFavouritesManagement.variableValidation.IsDigitsOnly(userId);
                if (checkIfHouseIdIsInt == false)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "House Id must be integer.");
                }
                if (checkIfUserIdIsInt == false)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "User Id must be integer.");
                }
                HouseModel getFavouriteHouse = _houseFavouritesManagement.GetHouseFavouriteById(Convert.ToInt32(houseId), Convert.ToInt32(userId));
                if (getFavouriteHouse == null) return Request.CreateErrorResponse(HttpStatusCode.NotFound, "House not available in the system");
                return Request.CreateResponse(HttpStatusCode.OK, getFavouriteHouse);
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

        // POST api/FavouriteHousesController
        [HttpPost]
        public HttpResponseMessage AddHouseToFavourites([FromBody]UserHouseModel newHouseFavourite)
        {
            if (newHouseFavourite.houseId == null || newHouseFavourite.userId == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid input variables");
            }
            bool checkIfHouseIdIsInt = _houseFavouritesManagement.variableValidation.IsDigitsOnly(newHouseFavourite.houseId);
            bool checkIfUserIdIsInt = _houseFavouritesManagement.variableValidation.IsDigitsOnly(newHouseFavourite.userId);
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
                bool checkIfHouseFavouriteIsCreated = _houseFavouritesManagement.AddHouseToFavourites(Convert.ToInt32(newHouseFavourite.houseId),Convert.ToInt32(newHouseFavourite.userId));
                if (checkIfHouseFavouriteIsCreated)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "House added to your favourites");
                }
                return Request.CreateResponse(HttpStatusCode.NoContent, "House does not exist");
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

        // PUT api/FavouriteHousesController/5
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody]string value)
        {
            return Request.CreateErrorResponse(HttpStatusCode.NotImplemented, "Unavailable operation");
        }

        // DELETE api/FavouriteHousesController/5
        [HttpDelete]
        public HttpResponseMessage RemoveHouseFavourite(string houseId,string userId)
        {
            if (houseId == null || userId == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid input variables");
            }
            bool checkIfHouseIdIsInt = _houseFavouritesManagement.variableValidation.IsDigitsOnly(houseId);
            bool checkIfUserIdIsInt = _houseFavouritesManagement.variableValidation.IsDigitsOnly(userId);
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
                bool removeHouseFromFavourites =
                    _houseFavouritesManagement.RemoveHouseFromFavourites(Convert.ToInt32(houseId), Convert.ToInt32(userId));
                if (removeHouseFromFavourites)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "House removed as favourite");
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotImplemented, "House not found for the user id given");
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
    }
}
