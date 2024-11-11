using vm.referendum.Domain.Abstractions;
using vm.referendum.Domain.Repository;
using vm.referendum.Domain.ValueObjects;
using vm.referendum.Infrastructure.Authentication.Cryptography;

namespace vm.referendum.Application.Features.SeedData.Commands.AddDumpData;

public sealed class AddDumpDataCommandHandler(
    IUserRepository userRepository,
    IQuestionRepository questionRepository,
    IAnswerRepository answerRepository,
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork,
    IPasswordHasher passwordHasher)
    : ICommandHandler<AddDumpDataCommand>
{
    public async Task Handle(AddDumpDataCommand request, CancellationToken cancellationToken = default)
    {
        var resUserProfile = await userRepository.GetAllAsync(cancellationToken);


        if (!resUserProfile.Any())
        {
            var hash = passwordHasher.HashPassword("123");

            await userRepository
                .AddRangeAsync(new List<Domain.Entities.User>
                {
                    Domain.Entities.User.Create(
                        FirstName.Create("f"),
                        LastName.Create("l"),
                        Email.Create("test@mail.com"),
                        hash)
                }, cancellationToken);
        }


        var resQuestion = await questionRepository.GetAllAsync(cancellationToken);
        if (!resQuestion.Any())
            await questionRepository
                .AddRangeAsync(new List<Domain.Entities.Question>
                {
                    new(Guid.Parse("17dbd29b-0fca-444e-ba20-f3f5a35dc59d"), "BMW"),
                    new(Guid.Parse("f55f2fdf-23e3-43e9-a424-f6c7c71b5607"), "Audi"),
                    new(Guid.Parse("c91c303a-b5e5-47c1-b3d2-a57fe4d2f75e"), "Benz")
                }, cancellationToken);


        var resAnswer = await answerRepository.GetAllAsync(cancellationToken);
        if (!resAnswer.Any())
            await answerRepository
                .AddRangeAsync(new List<Domain.Entities.Answer>
                {
                    new("Yes", Guid.Parse("17dbd29b-0fca-444e-ba20-f3f5a35dc59d")),
                    new("No", Guid.Parse("17dbd29b-0fca-444e-ba20-f3f5a35dc59d"))
                }, cancellationToken);

        // var resUserProfile = await _unitOfWork.UserProfile.GetAll();
        // if (!resUserProfile.Any())
        //     await _unitOfWork.UserProfile
        //         .AddRangeAsync(new List<Domain.Entities.UserProfile>
        //         {
        //             Domain.Entities.UserProfile.CreateUserProfile(
        //                 BasicInfo.CreateBasicInfo("f", "l", "00000000000",
        //                     Email.Create("test@mail.com").Value,
        //                     "5555555555", DateTime.UtcNow, "Tb"))
        //         }, cancellationToken);


        var resCategory = await categoryRepository.GetAllAsync(cancellationToken);
        if (!resCategory.Any())
            await categoryRepository
                .AddRangeAsync(new List<Domain.Entities.Category>
                {
                    Domain.Entities.Category.CreateCategory(Name.Create("Category1")),
                    Domain.Entities.Category.CreateCategory(Name.Create("Category2")),
                    Domain.Entities.Category.CreateCategory(Name.Create("Category3"))
                }, cancellationToken);


        await unitOfWork.CompleteAsync(cancellationToken);

     }
}