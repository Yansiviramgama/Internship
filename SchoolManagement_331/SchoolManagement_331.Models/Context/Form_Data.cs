//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SchoolManagement_331.Models.Context
{
    using System;
    using System.Collections.Generic;
    
    public partial class Form_Data
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public System.DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string ADDRESS { get; set; }
        public int UserCountry { get; set; }
        public int UserState { get; set; }
        public int UserCity { get; set; }
        public Nullable<int> PostalCode { get; set; }
    
        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
        public virtual State State { get; set; }
    }
}