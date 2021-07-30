using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        private readonly string _currentUserName;
        private readonly IUserResolverService _userResolverService;

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
            object x = null;
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
                            entityEntry.Property("UpdatedDate").CurrentValue = DateTime.UtcNow;
                            entityEntry.Property("CreatedBy").CurrentValue = currentUserName;
                            break;
                        case EntityState.Modified:
                            entityEntry.Property("UpdatedDate").CurrentValue = DateTime.UtcNow;
                            entityEntry.Property("UpdatedBy").CurrentValue = currentUserName;
                            break;
                        case EntityState.Deleted:
                            entityEntry.Property("DeletedBy").CurrentValue = currentUserName;
                            entityEntry.Property("DeletedDate").CurrentValue = DateTime.UtcNow;
                            break;

                    }
                }
                catch(Exception e)
                {
                    
                }
                
            }
        }

        public DbSet<HotelRoom> HotelRooms { get; set; }
        public DbSet<HotelRoomImage> HotelRoomImages { get; set; }
        public DbSet<HotelAmenity> HotelAmenities { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<AdminUser> AdminUser { get; set; }
        public DbSet<RoomOrderDetails> RoomOrderDetails { get; set; }
    }
}
