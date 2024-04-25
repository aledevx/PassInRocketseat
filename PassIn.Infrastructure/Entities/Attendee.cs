namespace PassIn.Infrastructure.Entities;
public class Attendee
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Guid Event_Id { get; set; }
    public DateTime Created_At { get; set; } = DateTime.UtcNow;
    public CheckIn? CheckIn { get; set; }
    public Attendee(string name, string email, Guid event_Id)
    {
        Name = name;
        Email = email;
        Event_Id = event_Id;
    }
}
