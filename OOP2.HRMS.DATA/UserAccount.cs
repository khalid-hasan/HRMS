//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OOP2.HRMS.DATA
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserAccount
    {
        public int UserID { get; set; }
        public string Password { get; set; }
        public int UserType { get; set; }
    
        public virtual EmployeeInfo EmployeeInfo { get; set; }
    }
}