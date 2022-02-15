using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Persistence;

public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
{
    Task<LeaveRequest> GetWithDetailsAsync(int id);
    Task<List<LeaveRequest>> GetAllWithDetailsAsync();
    Task ChangeApprovalStatus(LeaveRequest leaveRequest, bool? approved);
}
