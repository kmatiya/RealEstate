﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FindAHouseEntities : DbContext
    {
        public FindAHouseEntities()
            : base("name=FindAHouseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ClientState> ClientStates { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<DurationType> DurationTypes { get; set; }
        public virtual DbSet<FenceType> FenceTypes { get; set; }
        public virtual DbSet<FieldState> FieldStates { get; set; }
        public virtual DbSet<HouseBooking> HouseBookings { get; set; }
        public virtual DbSet<HouseContactDetail> HouseContactDetails { get; set; }
        public virtual DbSet<HouseFavourite> HouseFavourites { get; set; }
        public virtual DbSet<HouseOwner> HouseOwners { get; set; }
        public virtual DbSet<House> Houses { get; set; }
        public virtual DbSet<HouseState> HouseStates { get; set; }
        public virtual DbSet<HouseUrl> HouseUrls { get; set; }
        public virtual DbSet<InterestedClient> InterestedClients { get; set; }
        public virtual DbSet<LocationsInDistrict> LocationsInDistricts { get; set; }
        public virtual DbSet<LoginSession> LoginSessions { get; set; }
        public virtual DbSet<PaymentMode> PaymentModes { get; set; }
        public virtual DbSet<SubscriptionType> SubscriptionTypes { get; set; }
        public virtual DbSet<UserPhoneNumber> UserPhoneNumbers { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserSubscription> UserSubscriptions { get; set; }
    }
}
