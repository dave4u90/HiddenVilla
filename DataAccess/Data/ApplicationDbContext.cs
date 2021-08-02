using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;
using DataAccess.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

namespace DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        private readonly string _currentUserName;
        private readonly IUserResolverService _userResolverService;

        public static readonly ILoggerFactory loggerFactory = new LoggerFactory(new[] {
              new DebugLoggerProvider()
        });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(loggerFactory)
                .EnableSensitiveDataLogging();
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IUserResolverService userResolverService) : base(options)
        {
            _userResolverService = userResolverService;
            _currentUserName = _userResolverService.GetCurrentUserName();   
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving(_currentUserName);
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving(_currentUserName);
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public void OnBeforeSaving(string currentUserName)
        {
            string userName = String.IsNullOrEmpty(_currentUserName) ? SD.Role_Admin : _currentUserName;
            var entries = ChangeTracker
            .Entries()
            .Where(e =>
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified || e.State == EntityState.Deleted);

            foreach (var entityEntry in entries)
            {
                try
                {
                    switch (entityEntry.State)
                    {
                        case EntityState.Added:
                            entityEntry.Property("CreatedDate").CurrentValue = DateTime.UtcNow;
                            entityEntry.Property("CreatedBy").CurrentValue = userName;
                            break;
                        case EntityState.Modified:
                            entityEntry.Property("UpdatedDate").CurrentValue = DateTime.UtcNow;
                            entityEntry.Property("UpdatedBy").CurrentValue = userName;
                            break;
                        case EntityState.Deleted:
                            //entityEntry.State = EntityState.Modified;
                            entityEntry.Property("DeletedBy").CurrentValue = userName;
                            entityEntry.Property("DeletedDate").CurrentValue = DateTime.UtcNow;
                            entityEntry.State = EntityState.Modified;
                            break;

                    }
                }
                catch(Exception e)
                {
                    
                }
                
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<HotelRoom>().HasQueryFilter(x => String.IsNullOrEmpty(x.DeletedBy));
            builder.Entity<HotelRoomImage>().HasQueryFilter(x => String.IsNullOrEmpty(x.DeletedBy));
            builder.Entity<HotelAmenity>().HasQueryFilter(x => String.IsNullOrEmpty(x.DeletedBy));
            builder.Entity<RoomOrderDetails>().HasQueryFilter(x => String.IsNullOrEmpty(x.DeletedBy));
        }

        public DbSet<HotelRoom> HotelRooms { get; set; }
        public DbSet<HotelRoomImage> HotelRoomImages { get; set; }
        public DbSet<HotelAmenity> HotelAmenities { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<AdminUser> AdminUser { get; set; }
        public DbSet<RoomOrderDetails> RoomOrderDetails { get; set; }
    }
}
