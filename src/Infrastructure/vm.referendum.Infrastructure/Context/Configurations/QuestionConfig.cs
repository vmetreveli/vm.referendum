using vm.referendum.Domain.Entities;
using vm.referendum.Domain.Entities.Answer;
using vm.referendum.Domain.Entities.Category;
using vm.referendum.Domain.Entities.Question;

namespace vm.referendum.Infrastructure.Context.Configurations;

public sealed class QuestionConfig : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.HasKey(a => a.Id);


        builder.Property(c => c.CreatedOn).IsRequired();
        builder.Property(c => c.ModifiedOn);
        builder.Property(c => c.DeletedOn);
        builder.Property(c => c.IsDeleted).IsRequired();

        builder.HasOne<Answer>()
            .WithMany()
            .HasForeignKey("AnswerId")
            .OnDelete(DeleteBehavior.Cascade);


        builder.HasOne<Category>()
            .WithMany()
            .HasForeignKey("CategoryId");


        builder.HasQueryFilter(c => !c.IsDeleted);
    }
}