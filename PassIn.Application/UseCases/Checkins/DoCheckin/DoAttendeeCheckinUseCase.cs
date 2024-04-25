using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassIn.Application.UseCases.Checkins.DoCheckin;
public class DoAttendeeCheckinUseCase
{
    private readonly PassInDbContext _dbContext;
    public DoAttendeeCheckinUseCase()
    {
        _dbContext = new PassInDbContext();
    }
    public ResponseRegisteredJson Execute(Guid attendeeId) 
    {
        Validate(attendeeId);

        var entity = new CheckIn(attendeeId);

        _dbContext.CheckIns.Add(entity);
        _dbContext.SaveChanges();

        return new ResponseRegisteredJson(entity.Id);
    }
    public void Validate(Guid attendeeId) 
    {
        var existAttendee = _dbContext.Attendees.Any(a => a.Id == attendeeId);

        if (!existAttendee)
            throw new NotFoundException("The attendee with this Id was not found.");

        var existCheckin = _dbContext.CheckIns.Any(c => c.Attendee_Id == attendeeId);

        if (existCheckin)
            throw new ConflictException("Attendee can not do checking twice in the same event.");
        
    }
}
