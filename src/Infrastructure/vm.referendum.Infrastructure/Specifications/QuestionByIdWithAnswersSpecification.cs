using vm.referendum.Domain.Entities.Question;

namespace vm.referendum.Infrastructure.Specifications;

public sealed class QuestionByIdWithAnswersSpecification : Specification<Question, Guid>
{
    public QuestionByIdWithAnswersSpecification(Guid questionId)
        : base(question => question.Id == questionId)
    {
        AddInclude(question => question.Answers);
    }
}