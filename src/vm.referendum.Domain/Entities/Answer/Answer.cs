using Framework.Abstractions.Exceptions;
using vm.referendum.Domain.Abstractions;
using vm.referendum.Domain.ValueObjects;

namespace vm.referendum.Domain.Entities;

public sealed class Answer : AggregateRoot<Guid>, IAuditableEntity, IDeletableEntity
{
    private Answer() : base(Guid.NewGuid())
    {
    }

    public Answer(string text, Guid questionId) : base(Guid.NewGuid())
    {
        Text = text;
        QuestionId = questionId;
    }

    public bool IsSelected { get; private set; }
    public Guid QuestionId { get; private set; }
    public Question? Question { get; }

    public Statistic? Statistic { get; private set; }
    public string Text { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime CreatedOn { get; }
    public DateTime ModifiedOn { get; }
    public bool IsDeleted { get; }
    public DateTime? DeletedOn { get; }


    public void SetAnswer(Question question)
    {
        IsSelected = true;
        var res = Statistic.Create(this, question);
        Statistic = res ?? throw new InflowException("The answer has already been selected.");
       
    }

    public static Answer CreateAnswer(Guid questionId, string text, Guid userId)
    {
        return new Answer
        {
            QuestionId = questionId,
            Text = text,
            UserId = userId
        };
    }


    public void UpdateAnswerText(string newText)
    {
        Text = newText;
    }
}