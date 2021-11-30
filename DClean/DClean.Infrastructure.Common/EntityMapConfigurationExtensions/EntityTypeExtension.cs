using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DClean.Domain.Common.BaseEntities;
using DClean.Domain.Interfaces;

namespace DClean.Infrastructure.Common.EntityMapConfigurationExtensions
{
    public static class EntityTypeExtension
    {
        public static class EntityTypeOrder
        {

        }
        public static void ConfigureBaseTypes<TUser>(this EntityTypeBuilder builder)
        {
            var entiyType = builder.GetType().GetGenericArguments().FirstOrDefault(t => t.IsAssignableFrom(typeof(EntityBase)));
            if (entiyType == null) return;
            ConfigureBaseTypes<TUser, Guid>(builder, entiyType);
        }
        public static void ConfigureBaseTypes<TUser, TUserPK>(this EntityTypeBuilder builder, Type entityType)
        {
            throw new NotImplementedException();
        }
        public static void ConfigureBaseTypes<T, TUser, TUserPK>(this EntityTypeBuilder<T> builder)
            where T : class
            where TUser : class, IEntity
            where TUserPK : struct
        {
            var entityType = typeof(T);
            if (typeof(ISoftDeleteAuditedEntity<TUserPK, TUser>).IsAssignableFrom(entityType))
            {
                builder.HasOne(t => ((ISoftDeleteAuditedEntity<TUserPK, TUser>)t).DeletedBy)
                    .WithMany()
                    .HasForeignKey(t => ((ISoftDeleteAuditedEntity<TUserPK, TUser>)t).DeletedById);
            }
            else if (typeof(ISoftDeleteAuditedEntity<TUserPK>).IsAssignableFrom(entityType))
            {
                builder.HasOne<TUser>()
                    .WithMany()
                    .HasForeignKey(t => ((ISoftDeleteAuditedEntity<TUserPK>)t).DeletedById);
            }

            if (typeof(IUpdateAuditedEntity<TUserPK, TUser>).IsAssignableFrom(entityType))
            {
                builder.HasOne(t => ((IUpdateAuditedEntity<TUserPK, TUser>)t).UpdatedBy)
                    .WithMany()
                    .HasForeignKey(t => ((IUpdateAuditedEntity<TUserPK, TUser>)t).UpdatedById);
            }
            else if (typeof(IUpdateAuditedEntity<TUserPK>).IsAssignableFrom(entityType))
            {
                builder.HasOne<TUser>()
                    .WithMany()
                    .HasForeignKey(t => ((IUpdateAuditedEntity<TUserPK>)t).UpdatedById);
            }

            if (typeof(ICreateAuditedEntity<TUserPK, TUser>).IsAssignableFrom(entityType))
            {
                builder.HasOne(t => ((ICreateAuditedEntity<TUserPK, TUser>)t).CreatedBy)
                    .WithMany()
                    .HasForeignKey((t) => ((ICreateAuditedEntity<TUserPK, TUser>)t).CreatedById);
            }
            else if (typeof(ICreateAuditedEntity<TUserPK>).IsAssignableFrom(entityType))
            {
                builder.HasOne<TUser>()
                    .WithMany()
                    .HasForeignKey((t) => ((ICreateAuditedEntity<TUserPK>)t).CreatedById);
            }
            if (typeof(IEntity<Guid>).IsAssignableFrom(entityType))
            {
                builder.HasKey(t => ((IEntity<Guid>)t).Id);
                builder.Property(t => ((IEntity<Guid>)t).Id).ValueGeneratedOnAdd();
            }

            if (typeof(IMayHaveTenant<Guid>).IsAssignableFrom(entityType))
            {

            }
            if (typeof(IMayHaveTenant<Guid>).IsAssignableFrom(entityType))
            {

            }
        }

        public static void ConfigureBaseTypes<T, TUser>(this EntityTypeBuilder<T> builder)
            where T : class
            where TUser : class, IEntity
        {
            ConfigureBaseTypes<T, TUser, Guid>(builder);
        }
    }
}

