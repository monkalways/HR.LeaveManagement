using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Persistence;

public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
{
    Task<LeaveAllocation> GetWithDetailsAsync(int id);
    Task<List<LeaveAllocation>> GetAllWithDetailsAsync();
}
