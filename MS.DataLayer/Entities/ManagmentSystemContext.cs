using System.Data.Entity;

namespace MS.DataLayer.Entities
{
    public class ManagmentSystemContext : DbContext
    {
        public ManagmentSystemContext():base("ManagmentSystemConnection")
        {
            Database.SetInitializer<ManagmentSystemContext>(null);
        }
        public DbSet<Role> UserRoles { get; set; }
        public DbSet<User> Users { get; set; }       
        public DbSet<Gender> Genders { get; set; }
        public DbSet<SubscriptionType> SubscriptionTypes { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<ClubCard> ClubCards { get; set; }
        public DbSet<Client> Clients { get; set; }      
        public DbSet<Payment> Payments { get; set; }
        public DbSet<RecordForTraining> RecordsForTraining { get; set; }
    }
}
