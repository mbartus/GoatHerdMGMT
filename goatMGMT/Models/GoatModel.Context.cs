﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace goatMGMT.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class goatDBEntities : DbContext
    {
        public goatDBEntities()
            : base("name=goatDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Animal> Animals { get; set; }
        public virtual DbSet<AnimalTreatment> AnimalTreatments { get; set; }
        public virtual DbSet<Associate> Associates { get; set; }
        public virtual DbSet<Birth> Births { get; set; }
        public virtual DbSet<Breeding> Breedings { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Treatment> Treatments { get; set; }
    }
}
