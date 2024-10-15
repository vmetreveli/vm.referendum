using vm.referendum.Application.Contracts.Common.Mappings;
using vm.referendum.Domain.Entities;

namespace vm.referendum.Application.Contracts;

/// <summary>
///     Represents the user response.
/// </summary>
public sealed class UserResponse : IMap
{
    public Guid? Id { get; set; }
    public string? FullName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserResponse, User>()
            .ReverseMap();
    }
}