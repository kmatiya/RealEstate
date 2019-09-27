using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace TipezeNyumbaWebApplication.Controllers
{
    public class TipezeNyumbaController : Controller
    {
        TipezeNyumbaClientService.TipezeNyumbaServiceClient client = new TipezeNyumbaClientService.TipezeNyumbaServiceClient("BasicHttpBinding_ITipezeNyumbaService");
        
        // GET: TipezeNyumba
        public ActionResult Index()
        {
            List<TipezeNyumbaClientService.HouseDetails> GetHouses = client.GetAllHousesByDistrict("Lilongwe").ToList();
            return View(GetHouses);
        }
        // GET: TipezeNyumba/Details/5
        public ActionResult Details(int id)
        {
            TipezeNyumbaClientService.HouseDetails HouseRequired = client.GetSpecificHouse(id);
            return View(HouseRequired);
        }

        // GET: TipezeNyumba/Create
        public ActionResult Create()
        {
            LoadHouseDetailsDropdowns();
            return View();
        }

        private void LoadHouseDetailsDropdowns()
        {
            List<TipezeNyumbaClientService.HouseFenceType> GetFenceTypes = client.GetHouseFenceType().ToList();
            List<SelectListItem> fenceTypeListItems = new List<SelectListItem>();
            foreach (TipezeNyumbaClientService.HouseFenceType fenceType in GetFenceTypes)
            {
                fenceTypeListItems.Add(new SelectListItem { Text = fenceType.HouseFensetype, Value = fenceType.fenceTypeID.ToString() });
            }
            ViewBag.fenceType = fenceTypeListItems;
            List<TipezeNyumbaClientService.HousePaymentMode> GetHousePaymentModes = client.GetHousePaymentModes().ToList();
            List<SelectListItem> paymentModeListItems = new List<SelectListItem>();
            foreach (TipezeNyumbaClientService.HousePaymentMode housePaymentMode in GetHousePaymentModes)
            {
                paymentModeListItems.Add(new SelectListItem { Text = housePaymentMode.paymentMode, Value = housePaymentMode.modeOfPaymentID.ToString() });
            }
            ViewBag.modeOfPayment = paymentModeListItems;
            List<TipezeNyumbaClientService.HouseForRentState> GetForRentStates = client.GetHouseForRentStates().ToList();
            List<SelectListItem> forRentStatesListItems = new List<SelectListItem>();
            foreach (TipezeNyumbaClientService.HouseForRentState rentState in GetForRentStates)
            {
                forRentStatesListItems.Add(new SelectListItem { Text = rentState.HouseStatus, Value = rentState.houseStateID.ToString() });
            }
            ViewBag.houseState = forRentStatesListItems;
            List<TipezeNyumbaClientService.DistrictInMalawi> GetDistrictsInMalawi = client.GetDistrictsInMalawi().ToList();
            List<SelectListItem> districtListItems = new List<SelectListItem>();
            foreach (TipezeNyumbaClientService.DistrictInMalawi eachDistrict in GetDistrictsInMalawi)
            {
                districtListItems.Add(new SelectListItem { Text = eachDistrict.name, Value = eachDistrict.districtID.ToString() });
            }
            ViewBag.district = districtListItems;
        }

        // POST: TipezeNyumba/Create
        [HttpPost]
        public ActionResult Create(TipezeNyumbaClientService.HouseDetails newHouse)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add insert logic here
                    string messageFromService = client.AddAHouse(newHouse);
                    Session["messageFromService"] = messageFromService;
                    return RedirectToAction("Index");
                }
                catch
                {
                    Session["messageFromService"] = "Failed to add house";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                LoadHouseDetailsDropdowns();
                return View(newHouse);
            }
            
        }

        // GET: TipezeNyumba/Edit/5
        public ActionResult Edit(int id)
        {
            TipezeNyumbaClientService.HouseDetails EditHouse = client.GetSpecificHouse(id);
            LoadHouseDetailsDropdowns();
            ViewBag.district = null;
            List<TipezeNyumbaClientService.DistrictInMalawi> GetDistrictsInMalawi = client.GetDistrictsInMalawi().ToList();
            List<SelectListItem> districtListItems = new List<SelectListItem>();
            foreach (TipezeNyumbaClientService.DistrictInMalawi eachDistrict in GetDistrictsInMalawi)
            {
                if(eachDistrict.name == EditHouse.district)
                {
                    districtListItems.Add(new SelectListItem { Text = eachDistrict.name, Value = eachDistrict.districtID.ToString(), Selected = true });
                }
                else
                {
                    districtListItems.Add(new SelectListItem { Text = eachDistrict.name, Value = eachDistrict.districtID.ToString() });
                }                
            }
            ViewBag.district = districtListItems;
            return View(EditHouse);
        }

        // POST: TipezeNyumba/Edit/5
        [HttpPost]
        public ActionResult Edit(TipezeNyumbaClientService.HouseDetails houseToUpdate)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add insert logic here
                    string messageFromService = client.UpdateHouseDetails(houseToUpdate);
                    Session["messageFromService"] = messageFromService;
                    return RedirectToAction("Index");
                }
                catch
                {
                    Session["messageFromService"] = "Failed to add house";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                LoadHouseDetailsDropdowns();
                return View(houseToUpdate);
            }
        }

        // GET: TipezeNyumba/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TipezeNyumba/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
