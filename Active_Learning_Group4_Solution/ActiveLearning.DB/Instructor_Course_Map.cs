//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ActiveLearning.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Instructor_Course_Map
    {
        public int Sid { get; set; }
        public Nullable<int> InstructorSid { get; set; }
        public Nullable<int> CourseSid { get; set; }
        public Nullable<System.DateTime> CreateDT { get; set; }
        public Nullable<System.DateTime> UpdateDT { get; set; }
    
        public virtual Course Course { get; set; }
        public virtual Instructor Instructor { get; set; }
    }
}
