﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MaimApp.DataModels
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MaimfEntities : DbContext
    {
        public MaimfEntities()
            : base("name=MaimfEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Approval> Approval { get; set; }
        public virtual DbSet<Approval_request> Approval_request { get; set; }
        public virtual DbSet<BascetLine> BascetLine { get; set; }
        public virtual DbSet<Basket> Basket { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<County> County { get; set; }
        public virtual DbSet<Link> Link { get; set; }
        public virtual DbSet<PersonDataInRequest> PersonDataInRequest { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<ProductComment> ProductComment { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<Request> Request { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserFavProduct> UserFavProduct { get; set; }
        public virtual DbSet<UserPrData> UserPrData { get; set; }
    }
}
