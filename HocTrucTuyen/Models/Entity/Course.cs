//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HocTrucTuyen.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Course
    {
        public Course()
        {
            this.Course_Detail = new HashSet<Course_Detail>();
            this.MutipleChoices = new HashSet<MutipleChoice>();
            this.Registers = new HashSet<Register>();
            this.Tests = new HashSet<Test>();
            this.Reviews = new HashSet<Review>();
        }
    
        public long ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<long> Teacher_ID { get; set; }
        public string Images { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<bool> Status { get; set; }
    
        public virtual ICollection<Course_Detail> Course_Detail { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual ICollection<MutipleChoice> MutipleChoices { get; set; }
        public virtual ICollection<Register> Registers { get; set; }
        public virtual ICollection<Test> Tests { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
