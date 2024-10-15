using vm.referendum.Domain.Abstractions;
using vm.referendum.Domain.Errors;

namespace vm.referendum.Domain.Entities;

public sealed class Question : AggregateRoot<Guid>, IAuditableEntity, IDeletableEntity
{
    private readonly List<Answer> _answers = new();

    private Question(Guid id) : base(id)
    {
    }

    public Question(Guid id, string textContent) : base(id)
    {
        TextContent = textContent;
    }

    public IEnumerable<Answer> Answers => _answers;
    public string TextContent { get; private set; }

    public Guid? CategoryId { get; }
    // public Category? Category { get; private set; }

    public User User { get; }
    public Guid UserId { get; private set; }

    public DateTime CreatedOn { get; }
    public DateTime ModifiedOn { get; }
    public bool IsDeleted { get; }
    public DateTime? DeletedOn { get; }

    public static Question CreateQuestion(Guid userId, Guid? categoryId, string textContent)
    {
        return new Question(Guid.NewGuid())
        {
            UserId = userId,
            TextContent = textContent
            //    CategoryId = categoryId
        };
    }


    public Result UpdateQuestionText(string newText, Guid userId)
    {
        if (UserId != userId) return Result.Failure(UserErrors.InvalidPermissions);
        TextContent = newText;
        return Result.Success();
    }

    public void AddAnswer(Answer newAnswer)
    {
        _answers.Add(newAnswer);
    }

    public Result RemoveAnswer(Answer toRemove)
    {
        if (toRemove.UserId != UserId)
            return Result.Failure(
                new Error("Question.Remove",
                    "Cannot remove answer from question as you are not its author"));


        _answers.Remove(toRemove);
        return Result.Success();
    }

    public void UpdateAnswer(Guid answerId, string updatedAnswer)
    {
        // var answer = Answers.FirstOrDefault(c => c.Id == answerId);
        // if (answer != null && !string.IsNullOrWhiteSpace(updatedAnswer))
        //     answer.UpdateAnswerText(updatedAnswer);
    }
}