using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using WINConnect.Models;

namespace WINConnect.Data.Configuration.EntityFramework
{
    public class WINContext : DbContext
    {
        public WINContext()
            : base("WINContext")
        {
        }

        static WINContext()
        {
            //Database.SetInitializer(new DbInitializer());
        }

        private void SetDateProperties()
        {
            var utcNow = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                entry.Property("UpdatedOn").CurrentValue = utcNow;
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedOn").CurrentValue = utcNow;
                }
            }
        }

        public override int SaveChanges()
        {
            SetDateProperties();
            return base.SaveChanges();
        }

        public override System.Threading.Tasks.Task<int> SaveChangesAsync()
        {
            SetDateProperties();
            return base.SaveChangesAsync();
        }

        /* Master data */
        public DbSet<Country> Countries { get; set; }
        public DbSet<UNLocode> UNLocodes { get; set; }
        public DbSet<ListValues> ListValues { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<SeaCarrier> SeaCarriers { get; set; }

        public DbSet<Agent> Agents { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        /* Quotes */
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Quote> Quotes { get; set; }

        /* Sea booking */
        public DbSet<SeaBooking> SeaBookings { get; set; }
        public DbSet<SeaBookingStatusEvent> SeaBookingStatusEvents { get; set; }

        /* Air Freight */
        public DbSet<AirBooking> AirBookings { get; set; }
        public DbSet<ViewMAWB> ViewMAWBs { get; set; }
        public DbSet<HAWB> HAWBs { get; set; }

        public DbSet<FSU> FSUs { get; set; }
        public DbSet<PortalToXmlLog> FWBs { get; set; }
        public DbSet<FWBLog> FWBLog { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //modelBuilder.Entity<Article>().Property(a => a.CreatedDate).HasColumnType("datetime2").HasPrecision(0);
            //modelBuilder.Entity<Article>().Property(a => a.ModifiedDate).HasColumnType("datetime2").HasPrecision(0);
            //modelBuilder.Entity<Article>().Property(a => a.PublishedDate).HasColumnType("datetime2").HasPrecision(0);

            base.OnModelCreating(modelBuilder);
        }
    }
}