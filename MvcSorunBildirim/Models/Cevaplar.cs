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
    
    public partial class Cevaplar
    {
        public int Cevap_Id { get; set; }
        public Nullable<System.DateTime> Cevap_Zamani { get; set; }
        public string Cevap_Metni { get; set; }
        public Nullable<int> Basvuru_Id { get; set; }
        public bool İs_Active { get; set; }
    
        public virtual Basvuru Basvuru { get; set; }
    }
}
