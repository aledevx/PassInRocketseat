using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PassIn.Application.UseCases.Events.Register;
public class RegisterEventUseCase
{
    public ResponseRegisteredJson Execute(RequestEventJson request)
    {
        Validate(request);

        var dbContext = new PassInDbContext();

        var entity = new Event(request.Title, request.Details, request.Title.ToLower().Replace(" ", "-"),request.MaximumAttendees);

        dbContext.Events.Add(entity);
        dbContext.SaveChanges();

        return new ResponseRegisteredJson(entity.Id);
    }

    private void Validate(RequestEventJson request)
    {
        if (request.MaximumAttendees <= 0)
        {
            throw new ErrorOnValidationException("The maximum attendes is invalid");
        }
        if (string.IsNullOrWhiteSpace(request.Title)) 
        {
            throw new ErrorOnValidationException("The title is invalid.");
        }
        if (string.IsNullOrWhiteSpace(request.Details))
        {
            throw new ErrorOnValidationException("The details is invalid.");
        }
    }
}
