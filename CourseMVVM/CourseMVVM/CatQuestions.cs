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
    
    public partial class CatQuestions
    {
        public CatQuestions()
        {
            this.CatAnswers = new HashSet<CatAnswers>();
        }
    
        public int CatQuestionID { get; set; }
        public string QDesc { get; set; }
        public Nullable<int> QCat_id { get; set; }
        public string QAnswer { get; set; }
        public int QWeight { get; set; }
    
        public virtual ICollection<CatAnswers> CatAnswers { get; set; }
        public virtual Categories CategoryQt { get; set; }
    }
}
