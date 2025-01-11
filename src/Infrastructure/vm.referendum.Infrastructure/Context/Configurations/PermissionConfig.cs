using vm.referendum.Domain.Entities;
using vm.referendum.Domain.Entities.Permission;

namespace vm.referendum.Infrastructure.Context.Configurations;

internal sealed class PermissionConfig : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();

        // // Explicitly provide Ids for seed data
        // var permissions = Enum.GetValues<Domain.Enums.Permission>()
        //     .Select((p, index) =>
        //         new Permission(index + 1, p.ToString())); // Ensure unique Ids starting from 1
        //
        //
        // builder.HasData(permissions);

        builder.Property(p => p.Name).IsRequired();
        builder.Property(c => c.CreatedOn).IsRequired();
        builder.Property(c => c.ModifiedOn);
        builder.Property(c => c.DeletedOn);
        builder.Property(c => c.IsDeleted).IsRequired();
    }
}