﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcSorunBildirim.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Sorun_BildirimEntities : DbContext
    {
        public Sorun_BildirimEntities()
            : base("name=Sorun_BildirimEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Basvuru> Basvuru { get; set; }
        public virtual DbSet<Birim> Birim { get; set; }
        public virtual DbSet<Birim_Yetkili> Birim_Yetkili { get; set; }
        public virtual DbSet<Cevaplar> Cevaplar { get; set; }
        public virtual DbSet<Dokuman_Turu> Dokuman_Turu { get; set; }
        public virtual DbSet<Dokumanlar> Dokumanlar { get; set; }
        public virtual DbSet<Kanit> Kanit { get; set; }
        public virtual DbSet<Kurum> Kurum { get; set; }
        public virtual DbSet<Kurum_Yetkili> Kurum_Yetkili { get; set; }
        public virtual DbSet<Loglar> Loglar { get; set; }
        public virtual DbSet<Sevk> Sevk { get; set; }
        public virtual DbSet<Yetkililer> Yetkililer { get; set; }
    }
}