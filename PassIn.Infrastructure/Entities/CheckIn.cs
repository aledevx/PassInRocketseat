using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassIn.Infrastructure.Entities;
public class CheckIn
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime Created_at{ get; set; } = DateTime.UtcNow;
    public Guid Attendee_Id { get; set; }
    [ForeignKey("Attendee_Id")]
    public Attendee Attendee { get; set; } = default!;
    public CheckIn(Guid attendee_Id)
    {
        Attendee_Id = attendee_Id;
    }
}
