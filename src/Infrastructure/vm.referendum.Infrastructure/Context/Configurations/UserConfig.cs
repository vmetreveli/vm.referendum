using vm.referendum.Domain.Entities;

namespace vm.referendum.Infrastructure.Context.Configurations;

/// <summary>
///     Represents the configuration for the <see cref="User" /> entity.
/// </summary>
internal sealed class UserConfig : IEntityTypeConfiguration<User>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);

//        builder.HasOne(e => e.BasicInfo);


        builder.Property<string>("_passwordHash")
            .HasField("_passwordHash")
            .HasColumnName("password_hash")
            .IsRequired();


        builder.HasOne(e => e.Role)
            .WithMany()
            .HasForeignKey("RoleId");


        builder.OwnsOne(e => e.Email, modelNameBuilder =>
            modelNameBuilder
                .Property(l => l.Value)
                .HasColumnName("email")
                .IsRequired());

        builder.Navigation(c => c.Email);

        builder.OwnsOne(e => e.FirstName, modelNameBuilder =>
            modelNameBuilder
                .Property(l => l.Value)
                .HasColumnName("first_name")
                .HasMaxLength(100)
                .IsRequired());

        builder.Navigation(c => c.FirstName);

        builder.OwnsOne(e => e.LastName, modelNameBuilder =>
            modelNameBuilder
                .Property(l => l.Value)
                .HasColumnName("last_name")
                .HasMaxLength(100)
                .IsRequired());

        builder.Navigation(c => c.LastName);


        builder.Property(c => c.CreatedOn).IsRequired();
        builder.Property(c => c.ModifiedOn);
        builder.Property(c => c.DeletedOn);
        builder.Property(c => c.IsDeleted).IsRequired();

        builder.HasQueryFilter(user => !user.IsDeleted);

        builder.Ignore(user => user.FullName);
    }
}