using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassIn.Application.UseCases.Attendees.GetAll;
public class GetAllAttendeesByEventIdUseCase
{
    private readonly PassInDbContext _dbContext;
    public GetAllAttendeesByEventIdUseCase()
    {
        _dbContext = new PassInDbContext();
    }
    public ResponseAllAttendeesJson Execute(Guid eventId)
    {
        var eventEntity = _dbContext.Events.Include(e => e.Attendees).ThenInclude(e => e.CheckIn).FirstOrDefault(e => e.Id == eventId);

        if (eventEntity is null)
            throw new NotFoundException("An event with this id does not exist.");

        if (eventEntity.Attendees.Count() <= 0)
            throw new NotFoundException("Não existem pessoas registradas nesse evento.");

        var response = new ResponseAllAttendeesJson(eventEntity.Attendees.Select(a => new ResponseAttendeeJson(
            a.Id,
            a.Name,
            a.Email,
            a.Created_At,
            a.CheckIn?.Created_at)).ToList());
        
        return response;

    }
}
