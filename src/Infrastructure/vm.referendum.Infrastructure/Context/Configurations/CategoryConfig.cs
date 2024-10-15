using vm.referendum.Domain.Entities;

namespace vm.referendum.Infrastructure.Context.Configurations;

public sealed class CategoryConfig : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(a => a.Id);

        builder.OwnsOne(e => e.Name, modelNameBuilder =>
            modelNameBuilder
                .Property(l => l.Value)
                .HasColumnName("name")
                .IsRequired());


        builder.HasMany<Question>()
            .WithOne()
            .HasForeignKey("CategoryId");


        builder.Property(c => c.CreatedOn).IsRequired();
        builder.Property(c => c.ModifiedOn);
        builder.Property(c => c.DeletedOn);
        builder.Property(c => c.IsDeleted).IsRequired();

        builder.HasQueryFilter(c => !c.IsDeleted);
    }
}