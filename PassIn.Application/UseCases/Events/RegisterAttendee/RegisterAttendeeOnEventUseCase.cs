﻿using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PassIn.Application.UseCases.Events.RegisterAttendee;
public class RegisterAttendeeOnEventUseCase
{
    private readonly PassInDbContext _dbContext;
    public RegisterAttendeeOnEventUseCase()
    {
        _dbContext = new PassInDbContext();
    }
    public ResponseRegisteredJson Execute(Guid eventId, RequestRegisterEventJson request) 
    {
        Validate(eventId, request);

        var entity = new Attendee(request.Name, request.Email, eventId);

        _dbContext.Attendees.Add(entity);
        _dbContext.SaveChanges();

        return new ResponseRegisteredJson(entity.Id);
    }
    private void Validate(Guid eventId, RequestRegisterEventJson request) 
    {
        var eventEntity = _dbContext.Events.Find(eventId);
        if (eventEntity is null)
            throw new ErrorOnValidationException("An event with this id does not exist.");
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ErrorOnValidationException("The name is invalid.");
        if (string.IsNullOrWhiteSpace(request.Email) || !EmailIsValid(request.Email))
            throw new ErrorOnValidationException("The email is invalid.");

        var attendeeAlreadyRegistered = _dbContext.Attendees.Any(a => a.Email.Equals(request.Email) && a.Event_Id == eventId);
        if (attendeeAlreadyRegistered)
            throw new ConflictException("you can not register twice on the same event.");

        var countAttendeesOnEvent = _dbContext.Attendees.Count(a => a.Event_Id == eventId);
        if (countAttendeesOnEvent >= eventEntity.Maximum_Attendees)
            throw new ConflictException("There is no room for this event.");
    }
    private bool EmailIsValid(string email) 
    {
        try
        {
        new MailAddress(email);
        return true;
        }
        catch
        {
            return false;
        }
    }
}
