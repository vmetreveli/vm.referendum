using vm.referendum.Domain.Abstractions;
using vm.referendum.Domain.Entities;

namespace vm.referendum.Domain.ValueObjects;

public sealed class Vote : ValueObject
{
    public Vote(int value)
    {
        Value = value;
        Count = 1;
    }

    private Vote(Answer answer, Question question)
    {
        if (answer.Statistic is not null)
        {
            Value += answer.Statistic!.Value;
            Percentage = Value / question.Answers.Count() * 100;
        }
    }

    public int Value { get; set; }
    public int Count { get; set; }
    public double Percentage { get; set; }

    public static Vote Create(Answer answer, Question question)
    {
        return new Vote(answer, question);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
        yield return Percentage;
    }
}