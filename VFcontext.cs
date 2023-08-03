using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VF.Models;

namespace VF
{
    public class VFcontext : IdentityDbContext<Users, Roles, long>
    {
        public DbSet<Users>? Users { get; set; }
        public DbSet<Roles>? Roles { get; set; }
        public DbSet<UserRoles>? UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Users>(
                e =>
                {
                    e.ToTable("Users");
                    e.HasKey(e => e.Id);
                    e.Property(e => e.Email).IsRequired();
                    e.Property(e => e.UserName).IsRequired();
                    e.Property(e => e.PasswordHash).IsRequired();
                    e.Property(e => e.EmailConfirmed).HasDefaultValue(false);
                    e.Property(e => e.PhoneNumberConfirmed).HasDefaultValue(false);
                    e.Property(e => e.TwoFactorEnabled).HasDefaultValue(false);
                    e.Property(e => e.LockoutEnabled).HasDefaultValue(false);
                    e.Property(e => e.AccessFailedCount).HasDefaultValue(false);
                });
            modelBuilder.Entity<Roles>(
                e =>
                {
                    e.ToTable("Roles");
                    e.HasKey(e => e.Id);

                });
            modelBuilder.Entity<UserRoles>(
                e =>
                {
                    e.ToTable("UserRoles");
                    e.HasKey(e => e.UserId);
                    e.HasOne(e => e.Role).WithMany(e => e.UserRole).HasForeignKey(e => e.RoleId);
                    e.HasOne(e => e.User).WithOne(e => e.UserRole);
                });

        }

        public VFcontext(DbContextOptions<VFcontext> options) : base(options)
        {
           
        }       
        public void Save<T>(T entity) where T : class, IBaseEntity
        {
            T? existingEntity = Set<T>().Find(entity.Id);
             if (existingEntity != null)
            {
                existingEntity.UpdateAt = DateTime.Now;
                Entry(existingEntity).CurrentValues.SetValues(entity);
            }
            SaveChanges();
        }
        public async Task SaveAsync<T>(T entity) where T : class, IBaseEntity
        {
            T? existingEntity = await Set<T>().FindAsync(entity.Id);
            if (existingEntity != null)
            {
                existingEntity.UpdateAt = DateTime.Now;
                Entry(existingEntity).CurrentValues.SetValues(entity);
            }
            await SaveChangesAsync();

        }
        public void UpdateOrCreate<T>(T entity) where T : class, IBaseEntity
        {
            T? existingEntity = Set<T>().Find(entity.Id);
            if (existingEntity != null)
            {
                existingEntity.UpdateAt = DateTime.Now;
                Entry(existingEntity).CurrentValues.SetValues(entity);
            }
            else
            {
                entity.CreateAt = DateTime.Now;
                Set<T>().Add(entity);
            }
            
            SaveChanges();
        }
        public async Task UpdateOrCreateAsync<T>(T entity) where T : class, IBaseEntity
        {
            T? existingEntity = await Set<T>().FindAsync(entity.Id);
            if (existingEntity != null)
            {
                existingEntity.UpdateAt = DateTime.Now;
                Entry(existingEntity).CurrentValues.SetValues(entity);
            }
            else
            {
                entity.CreateAt = DateTime.Now;
                await Set<T>().AddAsync(entity);
            }

            await SaveChangesAsync();
        }
    }
}
