using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VF.Models;

namespace VF
{
    public class VFcontext : IdentityDbContext<Users, Roles, long>
    {
        private readonly TimeZoneInfo timeZoneInfo;
        public DbSet<Users>? Users { get; set; }
        public DbSet<Roles>? Roles { get; set; }
        public DbSet<UserRoles>? UserRoles { get; set; }

        public VFcontext(DbContextOptions<VFcontext> options, TimeZoneInfo dt) : base(options)
        {
            timeZoneInfo = dt;
        }
        public void Save<T>(T entity) where T : class, IBaseEntity
        {
            T? existingEntity = Set<T>().Find(entity.Id);
            DateTime? dt = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, timeZoneInfo);
            if (existingEntity != null)
            {
                existingEntity.UpdateAt = dt;
                Entry(existingEntity).CurrentValues.SetValues(entity);
            }
            SaveChanges();
        }
        public async Task SaveAsync<T>(T entity) where T : class, IBaseEntity
        {
            T? existingEntity = await Set<T>().FindAsync(entity.Id);
            DateTime? dt = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, timeZoneInfo);
            if (existingEntity != null)
            {
                existingEntity.UpdateAt =dt ;
                Entry(existingEntity).CurrentValues.SetValues(entity);
            }
            await SaveChangesAsync();

        }
        public void UpdateOrCreate<T>(T entity) where T : class, IBaseEntity
        {
            T? existingEntity = Set<T>().Find(entity.Id);
            DateTime? dt = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, timeZoneInfo);
            if (existingEntity != null)
            {
                existingEntity.UpdateAt =dt;
                Entry(existingEntity).CurrentValues.SetValues(entity);
            }
            else
            {
                entity.CreateAt = dt;
                Set<T>().Add(entity);
            }
            
            SaveChanges();
        }
        public async Task UpdateOrCreateAsync<T>(T entity) where T : class, IBaseEntity
        {
            T? existingEntity = await Set<T>().FindAsync(entity.Id);
            DateTime? dt = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, timeZoneInfo);
            if (existingEntity != null)
            {
                existingEntity.UpdateAt = dt;
                Entry(existingEntity).CurrentValues.SetValues(entity);
            }
            else
            {
                entity.CreateAt = dt;
                await Set<T>().AddAsync(entity);
            }

            await SaveChangesAsync();
        }
    }
}
