using vm.referendum.Application.Contracts.Common.Mappings;
using vm.referendum.Domain.Entities;
using vm.referendum.Domain.Entities.Question;

namespace vm.referendum.Application.Contracts;

public sealed class QuestionResponse : IMap
{
    public Guid Id { get; init; }
    public Guid UserProfileId { get; init; }
    public string? TextContent { get; init; }
    public IEnumerable<AnswerResponse>? Answers { get; init; }
    public DateTime CreatedDate { get; init; }
    public DateTime LastModified { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Question, QuestionResponse>()
            .ForMember(i => i.TextContent,
                opt =>
                    opt.MapFrom(src => src.TextContent))
            .ReverseMap();
    }
}