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
    
    public partial class Categories
    {
        public Categories()
        {
            this.Results = new HashSet<Results>();
            this.CatQuestions = new HashSet<CatQuestions>();
        }
    
        public int Cat_id { get; set; }
        public string Cat_name { get; set; }
    
        public virtual ICollection<Results> Results { get; set; }
        public virtual ICollection<CatQuestions> CatQuestions { get; set; }
    }
}
