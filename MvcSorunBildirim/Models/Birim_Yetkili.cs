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
    
    public partial class Birim_Yetkili
    {
        public int Birim_Yetkili_Id { get; set; }
        public Nullable<int> Birim_Id { get; set; }
        public Nullable<int> Yetkili_Id { get; set; }
        public Nullable<bool> Is_Active { get; set; }
    
        public virtual Birim Birim { get; set; }
    }
}