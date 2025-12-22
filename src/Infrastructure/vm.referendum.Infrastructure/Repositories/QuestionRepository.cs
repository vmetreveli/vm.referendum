using vm.referendum.Domain.Entities.Question;
using vm.referendum.Domain.Repository;
using vm.referendum.Infrastructure.Specifications;

namespace vm.referendum.Infrastructure.Repositories;

public sealed class QuestionRepository(DbContext context)
    : RepositoryBase<DbContext, Question, Guid>(context), IQuestionRepository
{
    public async Task<Question?> GetByIdWithAnswersAsync(Guid id, CancellationToken cancellationToken)
    {
        return await ApplySpecification(new QuestionByIdWithAnswersSpecification(id))
            .FirstOrDefaultAsync(cancellationToken);
    }
}