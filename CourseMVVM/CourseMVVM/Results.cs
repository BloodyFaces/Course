//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CourseMVVM
{
    using System;
    using System.Collections.Generic;
    
    public partial class Results
    {
        public int Results_id { get; set; }
        public string Owner { get; set; }
        public Nullable<int> Points { get; set; }
        public Nullable<int> TotalPoints { get; set; }
        public Nullable<int> Mark { get; set; }
        public Nullable<int> RCat_id { get; set; }
    
        public virtual Account AccountRs { get; set; }
        public virtual Categories CategoryRs { get; set; }
    }
}
