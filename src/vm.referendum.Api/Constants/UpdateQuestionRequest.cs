namespace vm.referendum.Api.Constants;

public class UpdateQuestionRequest
{
    public string TextContent { get; set; }
    public Guid CategoryId { get; set; }
    public Guid QuestionId { get; set; }
}