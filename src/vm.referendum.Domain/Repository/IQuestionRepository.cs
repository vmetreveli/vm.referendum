using Framework.Abstractions.Repository;
using vm.referendum.Domain.Entities;

namespace vm.referendum.Domain.Repository;

public interface IQuestionRepository : IRepositoryBase<Question, Guid>
{
    public Task<Question?> GetByIdWithAnswersAsync(Guid id, CancellationToken cancellationToken);
}