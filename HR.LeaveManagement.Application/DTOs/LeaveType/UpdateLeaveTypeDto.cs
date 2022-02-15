using HR.LeaveManagement.Application.DTOs.Common;

namespace HR.LeaveManagement.Application.DTOs.LeaveType;

public class UpdateLeaveTypeDto : BaseDto, ILeaveTypeDto
{
    public string Name { get; set; }
    public int? DefaultDays { get; set; }
}
