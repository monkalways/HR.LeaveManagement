using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories;

public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
{
    private readonly LeaveManagementDbContext dbContext;

    public LeaveRequestRepository(LeaveManagementDbContext dbContext) : base(dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task ChangeApprovalStatus(LeaveRequest leaveRequest, bool? approved)
    {
        leaveRequest.Approved = approved;
        dbContext.Entry(leaveRequest).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<LeaveRequest>> GetAllWithDetailsAsync()
    {
        var leaveRequests = await dbContext.LeaveRequests
            .Include(lr => lr.LeaveType)
            .ToListAsync();
        return leaveRequests;
    }

    public async Task<LeaveRequest> GetWithDetailsAsync(int id)
    {
        var leaveRequest = await dbContext.LeaveRequests
            .Include(lr => lr.LeaveType)
            .FirstOrDefaultAsync(lr => lr.Id == id);
        return leaveRequest;
    }
}
