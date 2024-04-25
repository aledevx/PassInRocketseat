using Microsoft.EntityFrameworkCore;
using PassIn.Application.UseCases.Events.Register;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PassIn.Application.UseCases.Events.GetById;
public class GetEventByIdUseCase
{
    public ResponseEventJson Execute(Guid id)
    {

        var dbContext = new PassInDbContext();

        var entity = dbContext.Events
            .Include(e => e.Attendees)
            .FirstOrDefault(e=> e.Id == id) ?? throw new NotFoundException("An event with this id dont exits.");

        var response = new ResponseEventJson()
        {
            Id = entity.Id,
            Title = entity.Title,
            Details = entity.Details,
            AttendeesAmount = entity.Attendees.Count(),
            MaximumAttendees = entity.Maximum_Attendees,
        };

        return response;

    }
}
