﻿

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
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class HRMSContext : DbContext
{
    public HRMSContext()
        : base("name=HRMSContext")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Designation> Designations { get; set; }

    public virtual DbSet<EmployeeInfo> EmployeeInfoes { get; set; }

    public virtual DbSet<EmployeeSalary> EmployeeSalaries { get; set; }

    public virtual DbSet<LeaveForm> LeaveForms { get; set; }

    public virtual DbSet<Payroll> Payrolls { get; set; }

    public virtual DbSet<Payslip> Payslips { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

}

}

