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
    
    public partial class Birim
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Birim()
        {
            this.Birim_Yetkili = new HashSet<Birim_Yetkili>();
            this.Sevk = new HashSet<Sevk>();
        }
    
        public int Birim_Id { get; set; }
        public string Birim_Adi { get; set; }
        public Nullable<int> Kurum_Id { get; set; }
        public Nullable<bool> Is_Active { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Birim_Yetkili> Birim_Yetkili { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sevk> Sevk { get; set; }
    }
}