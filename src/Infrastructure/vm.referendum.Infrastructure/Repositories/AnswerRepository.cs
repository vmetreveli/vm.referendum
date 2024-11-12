using Framework.Infrastructure.Repository;
using vm.referendum.Domain.Entities;
using vm.referendum.Domain.Entities.Answer;
using vm.referendum.Domain.Repository;

namespace vm.referendum.Infrastructure.Repositories;

public sealed class AnswerRepository(DbContext context)
    : RepositoryBase<DbContext, Answer, Guid>(context), IAnswerRepository;