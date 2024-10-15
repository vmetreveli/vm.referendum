using vm.referendum.Domain.Entities;

namespace vm.referendum.Infrastructure.Context.Configurations;

internal sealed class UserRoleConfig : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasKey(x => new { x.RoleId, x.UserId });

        builder.Property(c => c.CreatedOn).IsRequired();
        builder.Property(c => c.ModifiedOn);
        builder.Property(c => c.DeletedOn);
        builder.Property(c => c.IsDeleted).IsRequired();

        builder.HasQueryFilter(user => !user.IsDeleted);
    }
}