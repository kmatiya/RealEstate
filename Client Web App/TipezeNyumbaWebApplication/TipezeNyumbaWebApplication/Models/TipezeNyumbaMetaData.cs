using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TipezeNyumbaWebApplication.TipezeNyumbaClientService
{
    [MetadataType(typeof(HouseDetailsMetaData))]
    public partial class HouseDetails
    {
    }

    public class HouseDetailsMetaData
    {
        public int houseID { get; set; }
        [Display(Name = "Bed rooms")]
        [Required]
        public int bedrooms { get; set; }
        [Display(Name = "Master Bedroom Ensuite")]
        public bool masterBedroomEnsuite { get; set; }
        [Display(Name = "Self Contained")]
        public bool selfContained { get; set; }
        [Display(Name = "Number of Garages")]
        public Nullable<int> numberOfGarages { get; set; }
        [Display(Name = "Date house will be Available")]
        [Required]
        public System.DateTime dateHouseWillBeAvailable { get; set; }
        [Display(Name = "Price")]
        [Required]
        public decimal price { get; set; }
        [Display(Name = "Date Uploaded")]
        [Required]
        public System.DateTime dateUploaded { get; set; }
        [Display(Name = "Description")]
        [Required]
        public string description { get; set; }
        [Display(Name = "Mode of Payment")]
        [Required]
        public string modeOfPayment { get; set; }
        [Display(Name = "House State")]
        public string houseState { get; set; }
        [Display(Name = "Type of Fence")]
        [Required]
        public string fenceType { get; set; }
        [Display(Name = "District")]
        [Required]
        public string district { get; set; }
        [Display(Name = "Phone Number 1(mandatory)")]
        [Required]
        public string phoneNumber1 { get; set; }
        [Display(Name = "Phone Number 1(Optional)")]
        public string phoneNumber2 { get; set; }
        [Display(Name = "WhatsApp Contact (Optional)")]
        public string whatsAppContactNumber { get; set; }
        [Display(Name = "Email (Optional)")]
        public string email { get; set; }
    }
}