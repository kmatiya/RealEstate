//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TipezeNyumbaService.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserSubscription
    {
        public int userSubscriptionID { get; set; }
        public int userID { get; set; }
        public int subcriptionType { get; set; }
        public System.DateTime startDate { get; set; }
        public System.DateTime endDate { get; set; }
        public int state { get; set; }
        public System.DateTime dateCreated { get; set; }
    
        public virtual FieldState FieldState { get; set; }
        public virtual SubscriptionType SubscriptionType { get; set; }
        public virtual User User { get; set; }
    }
}
