﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LGSA.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MainDatabaseEntities : DbContext
    {
        public MainDatabaseEntities()
            : base("name=MainDatabaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<buy_Offer> buy_Offer { get; set; }
        public virtual DbSet<dic_condition> dic_condition { get; set; }
        public virtual DbSet<dic_Genre> dic_Genre { get; set; }
        public virtual DbSet<dic_Offer_status> dic_Offer_status { get; set; }
        public virtual DbSet<dic_Product_type> dic_Product_type { get; set; }
        public virtual DbSet<dic_Transaction_status> dic_Transaction_status { get; set; }
        public virtual DbSet<product> product { get; set; }
        public virtual DbSet<sell_Offer> sell_Offer { get; set; }
        public virtual DbSet<transactions> transactions { get; set; }
        public virtual DbSet<users> users { get; set; }
        public virtual DbSet<users_Authetication> users_Authetication { get; set; }
    }
}
