//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StoreFront2.DATA.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employees
    {
        public int EmpID { get; set; }
        public int DepID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ReportsTo { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public string Position { get; set; }
        public System.DateTime HireDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
    
        public virtual Departments Department { get; set; }
    }
}
