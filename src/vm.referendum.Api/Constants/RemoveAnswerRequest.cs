namespace vm.referendum.Api.Constants;

public class RemoveAnswerRequest
{
    public Guid UserId { get; set; }
    public Guid QuestionId { get; set; }
    public Guid AnswerId { get; set; }
}