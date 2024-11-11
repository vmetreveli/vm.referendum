using Framework.Abstractions.Exceptions;
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


    public void UpdateQuestionText(string newText, Guid userId)
    {
        if (UserId != userId)
            throw new InflowException("You cannot change the question text because users are not the same.");
        TextContent = newText;
    }

    public void AddAnswer(Answer newAnswer)
    {
        _answers.Add(newAnswer);
    }

    public void RemoveAnswer(Answer toRemove)
    {
        if (toRemove.UserId != UserId)
            throw new InflowException("You cannot remove the answer because users are not the same.");

        _answers.Remove(toRemove);
    }

    public void UpdateAnswer(Guid answerId, string updatedAnswer)
    {
        // var answer = Answers.FirstOrDefault(c => c.Id == answerId);
        // if (answer != null && !string.IsNullOrWhiteSpace(updatedAnswer))
        //     answer.UpdateAnswerText(updatedAnswer);
    }
}