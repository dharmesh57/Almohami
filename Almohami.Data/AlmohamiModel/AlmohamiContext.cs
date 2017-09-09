namespace Almohami.Data.AlmohamiModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AlmohamiContext : DbContext
    {
        public AlmohamiContext()
            : base("name=AlmohamiContext")
        {
        }

        public virtual DbSet<ApplicationModule> ApplicationModules { get; set; }
        public virtual DbSet<ApplicationParentModule> ApplicationParentModules { get; set; }
        public virtual DbSet<AppointmentCategory> AppointmentCategories { get; set; }
        public virtual DbSet<Case> Cases { get; set; }
        public virtual DbSet<CaseAppealStatusMst> CaseAppealStatusMsts { get; set; }
        public virtual DbSet<CaseAppointment> CaseAppointments { get; set; }
        public virtual DbSet<CaseDiscriminationStatusMst> CaseDiscriminationStatusMsts { get; set; }
        public virtual DbSet<CasePaymentDetail> CasePaymentDetails { get; set; }
        public virtual DbSet<CasePrimaryStatusMst> CasePrimaryStatusMsts { get; set; }
        public virtual DbSet<CaseType> CaseTypes { get; set; }
        public virtual DbSet<CaseUpdate> CaseUpdates { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Court> Courts { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<menu_mst> menu_mst { get; set; }
        public virtual DbSet<SecurityRole> SecurityRoles { get; set; }
        public virtual DbSet<SecurityRolePermission> SecurityRolePermissions { get; set; }
        public virtual DbSet<submenu_mst> submenu_mst { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserLog> UserLogs { get; set; }
        public virtual DbSet<vRolePermission> vRolePermissions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CasePaymentDetail>()
                .Property(e => e.CaseContractAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<CasePaymentDetail>()
                .Property(e => e.CasePaidAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<CasePaymentDetail>()
                .Property(e => e.CaseCreditAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Cases)
                .WithOptional(e => e.Client)
                .HasForeignKey(e => e.CaseClient);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.CasePaymentDetails)
                .WithOptional(e => e.Client)
                .HasForeignKey(e => e.CaseClientId);

            modelBuilder.Entity<Court>()
                .HasMany(e => e.Cases)
                .WithOptional(e => e.Court)
                .HasForeignKey(e => e.CaseCourtID);

            modelBuilder.Entity<Language>()
                .HasMany(e => e.SecurityRoles)
                .WithRequired(e => e.Language)
                .HasForeignKey(e => e.LanguageId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Language>()
                .HasMany(e => e.SecurityRolePermissions)
                .WithRequired(e => e.Language)
                .HasForeignKey(e => e.LanguageId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<menu_mst>()
                .Property(e => e.menu_name)
                .IsUnicode(false);

            modelBuilder.Entity<menu_mst>()
                .Property(e => e.menu_url)
                .IsUnicode(false);

            modelBuilder.Entity<menu_mst>()
                .Property(e => e.menu_icon)
                .IsUnicode(false);

            modelBuilder.Entity<menu_mst>()
                .Property(e => e.menu_createddate)
                .IsUnicode(false);

            modelBuilder.Entity<menu_mst>()
                .Property(e => e.menu_createdtime)
                .IsUnicode(false);

            modelBuilder.Entity<menu_mst>()
                .HasMany(e => e.submenu_mst)
                .WithOptional(e => e.menu_mst)
                .HasForeignKey(e => e.submenu_menu_id);

            modelBuilder.Entity<submenu_mst>()
                .Property(e => e.submenu_name)
                .IsUnicode(false);

            modelBuilder.Entity<submenu_mst>()
                .Property(e => e.submenu_url)
                .IsUnicode(false);

            modelBuilder.Entity<submenu_mst>()
                .Property(e => e.submenu_icon)
                .IsUnicode(false);

            modelBuilder.Entity<submenu_mst>()
                .Property(e => e.submenu_createddate)
                .IsUnicode(false);

            modelBuilder.Entity<submenu_mst>()
                .Property(e => e.submenu_createdtime)
                .IsUnicode(false);

            modelBuilder.Entity<submenu_mst>()
                .HasMany(e => e.submenu_mst1)
                .WithOptional(e => e.submenu_mst2)
                .HasForeignKey(e => e.submenu_parental_id);

            modelBuilder.Entity<User>()
                .Property(e => e.UserAvtar)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserPassword)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .HasMany(e => e.Cases)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.CaseUserID);
        }
    }
}
