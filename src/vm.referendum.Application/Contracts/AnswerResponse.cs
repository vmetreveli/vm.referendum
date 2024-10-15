using vm.referendum.Application.Contracts.Common.Mappings;
using vm.referendum.Domain.Entities;

namespace vm.referendum.Application.Contracts;

public sealed class AnswerResponse : IMap
{
    public Guid Id { get; init; }
    public string Text { get; init; }
    public StatisticDto? Statistic { get; init; }
    public string UserProfileId { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<AnswerResponse, Answer>()
            .ReverseMap();
    }
}