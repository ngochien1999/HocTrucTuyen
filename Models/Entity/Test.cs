//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HocTrucTuyen.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Test
    {
        public long ID { get; set; }
        public Nullable<long> Course_ID { get; set; }
        public Nullable<long> User_ID { get; set; }
        public Nullable<int> Category { get; set; }
        public string Result { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    
        public virtual Course Course { get; set; }
        public virtual User User { get; set; }
    }
}
