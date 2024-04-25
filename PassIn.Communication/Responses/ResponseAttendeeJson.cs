namespace PassIn.Communication.Responses;
public class ResponseAttendeeJson
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? CheckedInAt { get; set; }
    public ResponseAttendeeJson(Guid id, string name, string email, DateTime createdAt, DateTime? checkedInAt)
    {
        Id = id;
        Name = name;
        Email = email;
        CreatedAt = createdAt;
        CheckedInAt = checkedInAt;
    }
}
