using Framework.Abstractions.Repository;
using vm.referendum.Domain.Entities;

namespace vm.referendum.Domain.Repository;

public interface IAnswerRepository : IRepositoryBase<Answer, Guid>
{
}