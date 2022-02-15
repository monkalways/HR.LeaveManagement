using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands;

public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
{
    private readonly ILeaveRequestRepository LeaveRequestRepository;
    private readonly IMapper mapper;

    public UpdateLeaveRequestCommandHandler(ILeaveRequestRepository LeaveRequestRepository, IMapper mapper)
    {
        this.LeaveRequestRepository = LeaveRequestRepository;
        this.mapper = mapper;
    }
    public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var LeaveRequest = await LeaveRequestRepository.GetAsync(request.UpdateLeaveRequestDto.Id);

        mapper.Map(request.UpdateLeaveRequestDto, LeaveRequest);

        await LeaveRequestRepository.UpdateAsync(LeaveRequest);

        return Unit.Value;
    }
}
