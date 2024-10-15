using vm.referendum.Domain.Exception.UserErrors;
using vm.referendum.Domain.Primitives;
using vm.referendum.Domain.Services;
using vm.referendum.Domain.ValueObjects;

namespace vm.referendum.Domain.Entities;

public sealed class User : AggregateRoot<Guid>, IAuditableEntity, IDeletableEntity
{
    private string _passwordHash;


    private User(FirstName firstName, LastName lastName, Email email, string passwordHash)
        // : base(Guid.NewGuid())
        : base(Guid.NewGuid())
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        _passwordHash = passwordHash;
        RoleId = Role.Basic.Value;

        //AddDomainEvent(new UserCreatedDomainEvent(this));
    }

    private User() : base(Guid.NewGuid())
    {
    }

    public FirstName FirstName { get; private set; }

    public LastName LastName { get; private set; }


    public string FullName => $"{FirstName} {LastName}";

    public Email Email { get; }

    public Role? Role { get; }
    public Guid RoleId { get; private set; }

    // public void AddAccount(Account account)
    // {
    //     if (account is null)
    //         throw new ArgumentNullException(nameof(account));
    //
    //     if (_accounts.Contains(account.Id))
    //         return;
    //
    //     this.Append(new CustomerEvents.AccountAdded(this, account.Id));
    // }

    // protected override void When(IDomainEvent @event)
    // {
    //     switch (@event)
    //     {
    //         case UserEvents.UserCreated c:
    //             //Id = c.AggregateId;
    //             FirstName = c.Firstname;
    //             LastName = c.Lastname;
    //             Email = c.Email;
    //             break;
    //         // case UserEvents.AccountAdded aa:
    //         //     _accounts.Add(aa.AccountId);
    //         //     break;
    //     }
    // }
    public DateTime CreatedOn { get; }
    public DateTime ModifiedOn { get; }
    public bool IsDeleted { get; }
    public DateTime? DeletedOn { get; }

    // public static User Create(string firstName, string lastName, string email, Role role, string passwordHash)
    //     => new(firstName, lastName, email, role, passwordHash);
    public static User Create(FirstName firstName, LastName lastName, Email email, string passwordHash)
    {
        return new User(firstName, lastName, email, passwordHash);
    }


    public bool VerifyPasswordHash(string password, IPasswordHashChecker passwordHashChecker)
    {
        return !string.IsNullOrWhiteSpace(password) && passwordHashChecker.HashesMatch(_passwordHash, password);
    }


    public void ChangePassword(string password, string passwordHash)
    {
        if (passwordHash == _passwordHash)
            throw new CannotChangePasswordException();

        _passwordHash = passwordHash;

        var sendEmail = new SendEmailDto
        {
            Subject = "Referendum recovery",
            Html =
                $"Your template password is {password} Please change for security reason",
            To = Email
        };
        //AddDomainEvent(new PasswordResetDomainEvent(sendEmail));
        
    }

    public void ChangeName(FirstName firstName, LastName lastName)
    {
        FirstName = firstName;
        LastName = lastName;
//  AddDomainEvent(new UserNameChangedDomainEvent(this));
    }
}