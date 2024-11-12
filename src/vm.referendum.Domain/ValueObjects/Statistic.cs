using vm.referendum.Domain.Abstractions;
using vm.referendum.Domain.Entities;
using vm.referendum.Domain.Entities.Answer;
using vm.referendum.Domain.Entities.Question;

namespace vm.referendum.Domain.ValueObjects;

public sealed class Statistic : ValueObject
{
    private Statistic()
    {
    }

    private Statistic(Answer answer, Question question)
    {
        if (answer.Statistic is not null)
        {
            question.Answers
                .ToList()
                .ForEach(i => { Value += i.Statistic!.Value; });

            Percentage = Value / question.Answers.Count() * 100;
        }
    }

    public int Percentage { get; }
    public int Value { get; private set; } = 1;


    public static Statistic Create(Answer answer, Question question)
    {
        return new Statistic(answer, question);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
        yield return Percentage;
    }
}