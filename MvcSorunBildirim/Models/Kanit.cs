//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Kanit
    {
        public int Kanit_Id { get; set; }
        public string Kanit1 { get; set; }
        public Nullable<System.DateTime> Kanit_Zamani { get; set; }
        public Nullable<int> Dokuman_Turu_Id { get; set; }
        public Nullable<int> Basvuru_Id { get; set; }
        public bool Is_Active { get; set; }
    
        public virtual Basvuru Basvuru { get; set; }
        public virtual Dokuman_Turu Dokuman_Turu { get; set; }
    }
}