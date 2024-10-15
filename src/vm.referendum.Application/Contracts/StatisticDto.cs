using vm.referendum.Application.Contracts.Common.Mappings;
using vm.referendum.Domain.ValueObjects;

namespace vm.referendum.Application.Contracts;

public sealed class StatisticDto : IMap
{
    public int Percentage { get; init; }
    public int Value { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<StatisticDto, Statistic>()
            .ReverseMap();
    }
}