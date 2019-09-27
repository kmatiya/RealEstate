using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TipezeNyumbaWebApplication.TipezeNyumbaClientService
{
    [MetadataType(typeof(UserDetailsMetaData))]
    public partial class UserDetails
    {
    }

    public class UserDetailsMetaData
    {
        [Required(AllowEmptyStrings = true)]
        [Display(Name = "User ID")]
        public int userID { get; }
        [Display(Name = "First Name")]
        [Required]
        public string firstName { set; get; }
        [Required]
        [Display(Name = "Last Name")]
        public string lastName { set; get; }
        [Display(Name = "Email")]
        public string email { set; get; }
        [Display(Name = "Phone Number")]
        [Required]
        public string phoneNumber { get; set; }
        [Display(Name = "Date Created")]
        [Required]
        public System.DateTime dateTimeCreated { set; get; }
   
        [Display(Name = "Account State")]
        [Required]
        public string accountState { set; get; }
        [Display(Name = "User Role")]
        [Required]
        public string userRoleForUser { get; set; }
        [Display(Name = "User Subscription")]
        [Required]
        public string userSubscriptionType { get; set; }
    }
}