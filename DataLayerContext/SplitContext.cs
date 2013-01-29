using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity;
using System.Linq;
using DataInterface;
using Models;

namespace DataLayerContext
{
    public class SplitContext : DbContext, IUnitOfWork
    {
        public DbSet<Court> Courts { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Child> Children { get; set; }
        public DbSet<ChildForm> ChildForms { get; set; }
        public DbSet<Privacy> Privacy { get; set; }
        public DbSet<Information> Information { get; set; }
        public DbSet<Decisions> Decisions { get; set; }
        public DbSet<ExtraDecisions> ExtraDecisions { get; set; }
        public DbSet<Responsibility> Responsibilities { get; set; }
        public DbSet<Communication> Communications { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<ExtraHoliday> ExtraHolidays{ get; set; }
        public DbSet<County> Counties { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleForm> VehicleForms { get; set; }
        public DbSet<Debt> Debts { get; set; }
        public DbSet<Addendum> Addenda{ get; set; }
        public DbSet<Assets> Assets { get; set; }
        public DbSet<HealthInsurance> HealthInsurances{ get; set; }
        public DbSet<SpousalSupport> SpousalSupports { get; set; }
        public DbSet<Tax> Taxes{ get; set; }
        public DbSet<ChildSupport> ChildSupports{ get; set; }
        public DbSet<Income> Incomes{ get; set; }
        public DbSet<SocialSecurity> SocialSecurities{ get; set; }
        public DbSet<PreexistingSupportChild> PreexistingSupportChildren{ get; set; }
        public DbSet<PreexistingSupport> PreexistingSupports { get; set; }
        public DbSet<OtherChildren> OtherChildrens{ get; set; }
        public DbSet<SpecialCircumstances> SpecialCircumstanceses{ get; set; }
        public DbSet<OtherChild> OtherChild { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            SetupCourtMapping(modelBuilder);
            SetupParticipantMapping(modelBuilder);
            SetupUserEntity(modelBuilder);
            SetupClientEntity(modelBuilder);
            SetupChildEntity(modelBuilder);
            SetupChildFormEntity(modelBuilder);
            SetupPrivacyEntity(modelBuilder);
            SetupInformationEntity(modelBuilder);
            SetupDecisionEntity(modelBuilder);
            SetupExtraDecisionEntity(modelBuilder);
            SetupResponsibilityEntity(modelBuilder);
            SetupCommunicationEntity(modelBuilder);
            SetupScheduleEntity(modelBuilder);
            SetupHolidayEntity(modelBuilder);
            SetupExtraHolidayEntity(modelBuilder);
            SetupCountyEntity(modelBuilder);
            SetupHouseEntity(modelBuilder);
            SetupPropertyEntity(modelBuilder);
            SetupVehicleEntity(modelBuilder);
            SetupVehicleFormEntity(modelBuilder);
            SetupDebtEntity(modelBuilder);
            SetupAssetsEntity(modelBuilder);
            SetupHealthEntity(modelBuilder);
            SetupSpousalEntity(modelBuilder);
            SetupTaxEntity(modelBuilder);
            SetupChildSupportEntity(modelBuilder);
            SetupIncomeEntity(modelBuilder);
            SetupSocialSecurityEntity(modelBuilder);
            SetupPreexistingEntity(modelBuilder);
            SetupPreexistingChildEntity(modelBuilder);
            SetupOtherChildrenEntity(modelBuilder);
            SetupSpecialCircumstancesEntity(modelBuilder);
            SetupOtherChildEntity(modelBuilder);
            SetupParentingAddendumEntity(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void SetupClientEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasKey(t => new { t.Id });
            modelBuilder.Entity<Client>().Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);            
        }

        private void SetupParentingAddendumEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Addendum>().HasKey(t => new { t.Id });
            modelBuilder.Entity<Addendum>().Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }

        private void SetupChildFormEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChildForm>().HasKey(t => new { t.Id });
            modelBuilder.Entity<ChildForm>().Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }


        private void SetupOtherChildEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OtherChild>().HasKey(t => new { t.Id });
            modelBuilder.Entity<OtherChild>().Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<OtherChild>().Property(t => t.Name).HasMaxLength(100);
        }

        private void SetupSpecialCircumstancesEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SpecialCircumstances>().HasKey(t => new { t.Id });
            modelBuilder.Entity<SpecialCircumstances>().Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }

        private void SetupOtherChildrenEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OtherChildren>().HasKey(t => new { t.Id });
            modelBuilder.Entity<OtherChildren>().Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }

        private void SetupPreexistingChildEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PreexistingSupportChild>().HasKey(t => new { t.Id });
            modelBuilder.Entity<PreexistingSupportChild>().Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }

        private void SetupPreexistingEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PreexistingSupport>().HasKey(t => new { t.Id });
            modelBuilder.Entity<PreexistingSupport>().Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<PreexistingSupport>().Property(t => t.CourtName).HasMaxLength(100);
        }

        private void SetupSocialSecurityEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SocialSecurity>().HasKey(t => new { t.Id });
            modelBuilder.Entity<SocialSecurity>().Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }

        private void SetupIncomeEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Income>().HasKey(t => new { t.Id });
            modelBuilder.Entity<Income>().Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }

        private void SetupExtraHolidayEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExtraHoliday>().HasKey(t => new { t.Id });
            modelBuilder.Entity<ExtraHoliday>().Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<ExtraHoliday>().Property(t => t.HolidayName).HasMaxLength(100);
        }

        private void SetupChildSupportEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChildSupport>().HasKey(t => new { t.Id });
            modelBuilder.Entity<ChildSupport>().Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<ChildSupport>().Property(t => t.PaidBy).HasMaxLength(100);
            modelBuilder.Entity<ChildSupport>().Property(t => t.PaidBy).HasMaxLength(100);
        }

        private void SetupTaxEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tax>().HasKey(t => new { t.Id });
            modelBuilder.Entity<Tax>().Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }

        private void SetupSpousalEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SpousalSupport>().HasKey(t => new { t.Id });
            modelBuilder.Entity<SpousalSupport>().Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }

        private void SetupHealthEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HealthInsurance>().HasKey(t => new { t.Id });
            modelBuilder.Entity<HealthInsurance>().Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }

        private void SetupAssetsEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Assets>().HasKey(t => new { t.Id });
            modelBuilder.Entity<Assets>().Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }

        private void SetupDebtEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Debt>().HasKey(t => new { t.Id });
            modelBuilder.Entity<Debt>().Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        }

        private void SetupVehicleEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>().HasKey(t => new { t.Id });
            modelBuilder.Entity<Vehicle>().Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Vehicle>().Property(t => t.Make).HasMaxLength(100);
            modelBuilder.Entity<Vehicle>().Property(t => t.Model).HasMaxLength(100);
            modelBuilder.Entity<Vehicle>().Property(t => t.Owner).HasMaxLength(100);
        }
        private void SetupVehicleFormEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleForm>().HasKey(t => new { t.Id });
            modelBuilder.Entity<VehicleForm>().Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }

        private void SetupPropertyEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Property>().HasKey(t => new { t.Id });
            modelBuilder.Entity<Property>().Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }

        private void SetupHouseEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<House>().HasKey(t => new { t.Id });
            modelBuilder.Entity<House>().Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<House>().Property(t => t.Address).HasMaxLength(100);
            modelBuilder.Entity<House>().Property(t => t.MortgageOwner).HasMaxLength(100);
        }

        private void SetupCountyEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<County>().HasKey(t => new { t.Id });
            modelBuilder.Entity<County>().Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<County>().Property(t => t.CountyName).HasMaxLength(50);
        }

        private void SetupHolidayEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Holiday>().HasKey(t => new { t.Id });
            modelBuilder.Entity<Holiday>().Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);       
                 
        }

        private void SetupScheduleEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Schedule>().HasKey(t => new { t.Id });
            modelBuilder.Entity<Schedule>().Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);            
        }

        private void SetupCommunicationEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Communication>().HasKey(t => new { t.Id });
            modelBuilder.Entity<Communication>().Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Communication>().Property(t => t.OtherMethod).HasMaxLength(100);
        }

        private void SetupResponsibilityEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Responsibility>().HasKey(t => new { t.Id });
            modelBuilder.Entity<Responsibility>().Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }

        private void SetupExtraDecisionEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExtraDecisions>().HasKey(t => new { t.Id });
            modelBuilder.Entity<ExtraDecisions>().Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<ExtraDecisions>().Property(t => t.Description).HasMaxLength(100);
        }

        private void SetupDecisionEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Decisions>().HasKey(t => new { t.Id });
            modelBuilder.Entity<Decisions>().Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);            
        }

        private void SetupInformationEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Information>().HasKey(t => new { t.Id });
            modelBuilder.Entity<Information>().Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);           
        }

        private void SetupPrivacyEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Privacy>().HasKey(t => new { t.Id });
            modelBuilder.Entity<Privacy>().Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }

        private void SetupChildEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Child>().HasKey(t => new { t.Id });
            modelBuilder.Entity<Child>().Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Child>().Property(t => t.Name).HasMaxLength(100);
        }

        private void SetupParticipantMapping(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Participant>().HasKey(t => new { t.Id });
            modelBuilder.Entity<Participant>().Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Participant>().Property(t => t.DefendantsName).HasMaxLength(100);
            modelBuilder.Entity<Participant>().Property(t => t.PlaintiffsName).HasMaxLength(100);
        }

        private void SetupCourtMapping(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Court>().HasKey(t => new { t.Id});
            modelBuilder.Entity<Court>().Property(t => t.Id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        }

        private void SetupUserEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().Property(u => u.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<User>().Property(u => u.FirstName).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.FirstName).HasMaxLength(50);
            modelBuilder.Entity<User>().Property(u => u.LastName).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.LastName).HasMaxLength(50);
            modelBuilder.Entity<User>().Property(u => u.Email).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Email).HasMaxLength(100);
            modelBuilder.Entity<User>().Property(u => u.Password).HasMaxLength(100);
        }



        void IUnitOfWork.Commit()
        {
            base.SaveChanges();
        }


        /// <summary>
        /// Rollback tracked changes. 
        /// </summary>
        public void RollbackChanges()
        {
            // set all entities in change tracker 
            // as 'unchanged state'
            base.ChangeTracker.Entries()
                              .ToList()
                              .ForEach(entry => entry.State = EntityState.Unchanged);
        }

        /// <summary>
        /// Execute specific query with underliying persistence store
        /// </summary>
        /// <typeparam name="TEntity">Entity type to map query results</typeparam>
        /// <param name="sqlQuery">
        /// Dialect Query 
        /// <example>
        /// SELECT idCustomer,Name FROM dbo.[Customers] WHERE idCustomer > {0}
        /// </example>
        /// </param>
        /// <param name="parameters">A vector of parameters values</param>
        /// <returns>
        /// Enumerable results 
        /// </returns>
        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
        {
            return base.Database.SqlQuery<TEntity>(sqlQuery, parameters);
        }

        /// <summary>
        /// Execute arbitrary command into underliying persistence store
        /// </summary>
        /// <param name="sqlCommand">
        /// Command to execute
        /// <example>
        /// SELECT idCustomer,Name FROM dbo.[Customers] WHERE idCustomer > {0}
        /// </example>
        ///</param>
        /// <param name="parameters">A vector of parameters values</param>
        /// <returns>The number of affected records</returns>
        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            return base.Database.ExecuteSqlCommand(sqlCommand, parameters);
        }
    }
}
