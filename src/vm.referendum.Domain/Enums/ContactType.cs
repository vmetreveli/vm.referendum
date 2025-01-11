using System.ComponentModel;

namespace vm.referendum.Domain.Enums;

public enum ContactType
{
    [Description("Tel")] Tel = 1,
    [Description("Mobile")] Mobile = 2,
    [Description("Mail")] Mail = 3,
    [Description("Fax")] Fax = 4
}