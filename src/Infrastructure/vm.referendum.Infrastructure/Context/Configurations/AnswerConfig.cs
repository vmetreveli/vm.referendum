using vm.referendum.Domain.Entities;

namespace vm.referendum.Infrastructure.Context.Configurations;

internal sealed class AnswerConfig : IEntityTypeConfiguration<Answer>
{
    public void Configure(EntityTypeBuilder<Answer> builder)
    {
        builder.HasKey(a => a.Id);

        builder.HasOne(a => a.Question)
            .WithMany(q => q.Answers)
            .HasForeignKey(a =>
                a.QuestionId); // Assuming there's a foreign key property in the Answer entity, e.g., QuestionId


        builder.OwnsOne(e => e.Statistic,
            modelNameBuilder =>
                modelNameBuilder
                    .Property(l => l.Value)
                    .IsRequired());

        builder.OwnsOne(e => e.Statistic, modelNameBuilder =>
            modelNameBuilder
                .Property(l => l.Percentage)
                .HasColumnName("percentage"));


        builder.Navigation(e => e.Statistic);

        builder.Property(c => c.CreatedOn).IsRequired();
        builder.Property(c => c.ModifiedOn);
        builder.Property(c => c.DeletedOn);
        builder.Property(c => c.IsDeleted).IsRequired();

        builder.HasQueryFilter(c => !c.IsDeleted);
    }
}