using vm.referendum.Domain.Entities.Question;

namespace vm.referendum.Domain.Repository;

public interface IQuestionRepository : IRepositoryBase<Question, Guid>
{
    public Task<Question?> GetByIdWithAnswersAsync(Guid id, CancellationToken cancellationToken);
}