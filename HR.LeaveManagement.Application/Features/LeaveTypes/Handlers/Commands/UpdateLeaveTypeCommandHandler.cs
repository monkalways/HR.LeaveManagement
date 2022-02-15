using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;

public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
{
    private readonly ILeaveTypeRepository leaveTypeRepository;
    private readonly IMapper mapper;

    public UpdateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
    {
        this.leaveTypeRepository = leaveTypeRepository;
        this.mapper = mapper;
    }


    public async Task<Unit> Handle(UpdateLeaveTypeCommand command, CancellationToken cancellationToken)
    {
        var leaveType = await leaveTypeRepository.GetAsync(command.LeaveTypeDto.Id);

        if(leaveType == null)
        {
            throw new NotFoundException(nameof(LeaveType), command.LeaveTypeDto.Id);
        }

        mapper.Map(command.LeaveTypeDto, leaveType);

        await leaveTypeRepository.UpdateAsync(leaveType); 

        return Unit.Value;
    }
}
