using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Infrastructure;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Models;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, int>
{
    private readonly ILeaveRequestRepository leaveRequestRepository;
    private readonly IMapper mapper;
    private readonly IEmailSender emailSender;

    public CreateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper, 
        IEmailSender emailSender)
    {
        this.leaveRequestRepository = leaveRequestRepository;
        this.mapper = mapper;
        this.emailSender = emailSender;
    }
    public async Task<int> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = mapper.Map<LeaveRequest>(request.CreateLeaveRequestDto);
        leaveRequest = await leaveRequestRepository.AddAsync(leaveRequest);

        var email = new Email
        {
            To = "employee@org.com",
            Body = $"Your leave request for {request.CreateLeaveRequestDto.StartDate:D} " +
            $"to {request.CreateLeaveRequestDto.EndDate:D} has been submitted successfully.",
            Subject = "Leave Request Submitted"
        };
        try
        {
            await emailSender.SendEmail(email);
        }
        catch
        {
            // log error ...
        }

        return leaveRequest.Id;
    }
}
