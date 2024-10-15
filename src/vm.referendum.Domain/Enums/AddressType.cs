using vm.referendum.Domain.Primitives;

namespace vm.referendum.Domain.Enums;

public sealed class AddressType : Enumeration<AddressType>
{
    public static readonly AddressType _actualAddress = new(Guid.NewGuid(), "ActualAddress");

    public static readonly AddressType _registrationAddress = new(Guid.NewGuid(), "RegistrationAddress");

    // [Description("ActualAddress")] Actual = 1,
    // [Description("RegistrationAddress")] Registration = 2
    private AddressType(Guid value, string name)
        : base(value, name)
    {
    }
}