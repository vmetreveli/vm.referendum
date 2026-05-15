using Meadow_Framework.Core.Abstractions.Repository;
using vm.referendum.Domain.Entities.Answer;

namespace vm.referendum.Domain.Repository;

public interface IAnswerRepository : IRepositoryBase<Answer, Guid>
{
}