﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PensionModel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class VPDPensionEntities : DbContext
    {
        public VPDPensionEntities()
            : base("name=VPDPensionEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<AllowanceMaster> AllowanceMasters { get; set; }
        public DbSet<CompWiseAllowance> CompWiseAllowances { get; set; }
        public DbSet<EmployerMaster> EmployerMasters { get; set; }
        public DbSet<ErrorMsgMaster> ErrorMsgMasters { get; set; }
        public DbSet<GradeMaster> GradeMasters { get; set; }
        public DbSet<InsuranceMaster> InsuranceMasters { get; set; }
        public DbSet<PlanMaster> PlanMasters { get; set; }
        public DbSet<RetirementType_Master> RetirementType_Master { get; set; }
        public DbSet<LoginMaster> LoginMasters { get; set; }
        public DbSet<RoleMaster> RoleMasters { get; set; }
        public DbSet<LifeCertificate> LifeCertificates { get; set; }
        public DbSet<NomineeMaster> NomineeMasters { get; set; }
        public DbSet<PensionMaster> PensionMasters { get; set; }
        public DbSet<GradeChange> GradeChanges { get; set; }
        public DbSet<PensionType> PensionTypes { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<MergingCompany> MergingCompanies { get; set; }
        public DbSet<PensionLimit> PensionLimits { get; set; }
        public DbSet<SalaryMaster> SalaryMasters { get; set; }
        public DbSet<TaxMaster> TaxMasters { get; set; }
        public DbSet<TaxValue> TaxValues { get; set; }
        public DbSet<Incentive> Incentives { get; set; }
        public DbSet<EmployeePlan> EmployeePlans { get; set; }
        public DbSet<ColunmMapMaster> ColunmMapMasters { get; set; }
        public DbSet<CompanyMaster> CompanyMasters { get; set; }
        public DbSet<EmployeeMaster> EmployeeMasters { get; set; }
        public DbSet<SAContribution> SAContributions { get; set; }
        public DbSet<SAEmpDetail> SAEmpDetails { get; set; }
        public DbSet<SAInterest> SAInterests { get; set; }
        public DbSet<Annuity> Annuities { get; set; }
    }
}
