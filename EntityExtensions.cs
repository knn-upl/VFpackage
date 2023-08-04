using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VF.Models;

namespace VF
{
    public static class EntityExtensions
    {
       
        public static T Create<T>(this DbSet<T> dbSet, T entity) where T :  IBaseEntity
        {
            entity.CreatedAt = DateTimeExtensions.GetCurrentTime();
            entity.UpdatedAt = DateTimeExtensions.GetCurrentTime();
            dbSet.Add(entity);          
            return entity;
        }
        public static async Task<T> CreateAsync<T>(this DbSet<T> dbSet, T entity) where T : IBaseEntity
        {
            entity.CreatedAt = DateTimeExtensions.GetCurrentTime();
            entity.UpdatedAt = DateTimeExtensions.GetCurrentTime();
            await dbSet.AddAsync(entity);
            return entity;
        }
        public static T Update<T>(this DbSet<T> dbSet, T entity) where T : IBaseEntity
        {
            var existingEntity = dbSet.AsNoTracking().SingleOrDefault(p => p.Id == entity.Id);

            entity.UpdatedAt = DateTimeExtensions.GetCurrentTime();
            entity.CreatedAt = existingEntity?.CreatedAt;
            dbSet.Update(entity);
            return entity;
            
        }
        public static async Task<T> UpdateAsync<T>(this DbSet<T> dbSet, T entity) where T : IBaseEntity
        {
            var existingEntity = await dbSet.AsNoTracking().SingleOrDefaultAsync(p => p.Id == entity.Id);

            entity.UpdatedAt = DateTimeExtensions.GetCurrentTime();
            entity.CreatedAt = existingEntity?.CreatedAt;
            dbSet.Update(entity);
            await Task.CompletedTask;
            return entity;
        }

        public static T UpdateOrCreate<T>(this DbSet<T> dbSet, T entity) where T : IBaseEntity
        {
            var existingEntity = dbSet.AsNoTracking().SingleOrDefault(p => p.Id == entity.Id);

            if (existingEntity == null)
            {
                entity.Id = null;
                entity.CreatedAt = DateTimeExtensions.GetCurrentTime();
                entity.UpdatedAt = DateTimeExtensions.GetCurrentTime();
                dbSet.Add(entity);
            }
            else
            {              
                entity.UpdatedAt = DateTimeExtensions.GetCurrentTime();
                entity.CreatedAt = existingEntity.CreatedAt;
                dbSet.Update(entity);
            }
            return entity;
        }

        public static async Task<T> UpdateOrCreateAsync<T>(this DbSet<T> dbSet, T entity) where T : IBaseEntity
        {
            var existingEntity = await dbSet.AsNoTracking().SingleOrDefaultAsync(p=>p.Id == entity.Id);
            if (existingEntity == null)
            {
                entity.Id = null;
                entity.CreatedAt = DateTimeExtensions.GetCurrentTime();
                entity.UpdatedAt = DateTimeExtensions.GetCurrentTime();
                await dbSet.AddAsync(entity);
            }
            else
            {               
                entity.UpdatedAt = DateTimeExtensions.GetCurrentTime();              
                entity.CreatedAt = existingEntity.CreatedAt;
                dbSet.Update(entity);
            }
            return entity;
        }           
    }
}
