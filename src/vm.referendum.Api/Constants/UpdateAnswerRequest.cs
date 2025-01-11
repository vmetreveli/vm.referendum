namespace vm.referendum.Api.Constants;

public sealed class UpdateAnswerRequest
{
    public Guid QuestionId { get; set; }
    public Guid AnswerId { get; set; }
    public string Text { get; set; }
}