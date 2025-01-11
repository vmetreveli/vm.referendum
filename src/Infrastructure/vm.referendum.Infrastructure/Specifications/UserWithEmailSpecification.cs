using Framework.Abstractions.Specifications;
using vm.referendum.Domain.Entities;
using vm.referendum.Domain.Entities.User;
using vm.referendum.Domain.ValueObjects;

namespace vm.referendum.Infrastructure.Specifications;

/// <summary>
///     Represents the specification for determining the user with email.
/// </summary>
public sealed class UserWithEmailSpecification(Email email)
    : Specification<User, Guid>(u => u.Email.Value!.Equals(email.Value));